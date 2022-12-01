/*  Filename:           PlayerController.cs
 *  Author:             Liam Nelski (301064116), Yuk Yee Wong (301234795)
 *  Last Update:        October 10th, 2022
 *  Description:        Controls the player
 *  Revision History:   October 10th (Liam Nelski): Inital Script.
 *                      October 16th (Liam Nelski): Added Use of Equipable Items
 *                      November 3th (Liam Nelski): Moved Equipable from interface to script
 *                      November 3th (Liam Nelski): Made Player Point to the mouse.
 *                      November 12th (Liam Nelski): Moved Values to Scriptable Object.
 *                      November 13th (Liam Nelski): Added Event for value changes For UI Accessiblity
 *                      November 25th (Yuk Yee Wong): Added OnEnemyDestroyMethod; passed as an argument in LoadWeapon, implemented health system to receive damage; reset current health and current score in awake.
 *                      November 26th (Yuk Yee Wong): Added input actions for crate answer and interaction
 */
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController<PlayerController>
{
    // Scripts
    //private Camera mainCamera;
    public EquipSlot EquipedItem { get; private set; }
    public Rigidbody Rb { get; private set; }
    public HealthSystem Health { get; private set; }
    public Inventory Inventory { get; private set; }
    public ActionTimerPool Timers { get; private set; }

    // States
    public PlayerIdleState idleState;
    public PlayerUseItemState useItemState;
    public PlayerDashState dashState;

    // Editor Accessable
    public PlayerContext playerContext;
    public List<GameObject> equipables;

    // Player Input
    public PlayerInputActions Input { get; private set; }
    public GameEnums.EquipableInput AttackInput { get; private set; }
    public Vector3 LookAtPosition { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public bool DashInput { get; private set; }

    // Values that control player behviour
    // ReadOnly
    public float MaxHealth { get => playerContext._maxHealth; }
    public float BaseSpeed { get => playerContext._baseSpeed; }
    public float TurnSpeed { get => playerContext._turnSpeed; }
    public float Acceleration { get => playerContext._acceleration; }
    public float DashSpeed { get => playerContext._dashSpeed; }
    public float DashDurationMiliseconds { get => playerContext._dashDurationMiliseconds; }
    public float DashCoolDownMiliseconds { get => playerContext._dashCoolDownMiliseconds; }

    // Writable
    public float CurrentHealth
    {
        get => playerContext._currentHealth;
        set
        {
            playerContext._currentHealth = value;
            OnHealthUpdated?.Invoke();
        }
    }

    public float CurrentDashCoolDown
    {
        get => playerContext._currentDashCoolDown;
        set
        {
            playerContext._currentDashCoolDown = Mathf.Max(value, 0f);
            OnDashCoolDownUpdated?.Invoke();
        }
    }

    public int CurrentScore
    {
        get => playerContext._score;
        set
        {
            playerContext._score = value;
            OnScoreIncremented?.Invoke(value);
        }
    }

    public float CurrentSpeed { get => playerContext._currentSpeed; set => playerContext._currentSpeed = value; }
    public bool CanDash { get => playerContext._canDash; set => playerContext._canDash = value; }

    // Events
    public Action OnHealthUpdated;
    public Action OnDashCoolDownUpdated;
    public Action<int> OnScoreIncremented;

    public Action<Inventory> OnCrateInteracted;
    public Action<Inventory, int> OnCrateQuestAnswered;

    // private varibles
    private int equipablesIndex = -1;

    private void Awake()
    {
        Input = new PlayerInputActions();
        Input.Player.Enable();

        idleState = new PlayerIdleState(this);
        useItemState = new PlayerUseItemState(this);
        dashState = new PlayerDashState(this);

        Rb = GetComponent<Rigidbody>();
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        EquipedItem = GetComponentInChildren<EquipSlot>();
        Timers = GetComponent<ActionTimerPool>();
        Inventory = GetComponent<Inventory>();

        AddInputActions();

        // Hopefully temporary
        Inventory.OnAddItem += (ItemData data) => { CurrentScore += data.score; };

        // SetUp Health
        Health = GetComponent<HealthSystem>();
        Health.ReceiveDamage += ReceiveDamage;

        // Reset current health
        CurrentHealth = MaxHealth;

        // Reset score
        CurrentScore = 0;

        activeState = idleState;
        activeState.OnStateEnter();
    }

    private void OnDestroy()
    {
        // avoid null reference upon stopping the game or return to the start scene which results from OnAimInput
        RemoveInputActions();
    }

    private void AddInputActions()
    {
        Input.Player.Movement.started += OnMovementInput;
        Input.Player.Movement.performed += OnMovementInput;
        Input.Player.Movement.canceled += OnMovementInput;

        Input.Player.Attack.started += OnAttackInput;
        Input.Player.Attack.canceled += OnAttackInput;

        Input.Player.SwapWeapon.performed += OnSwapWeaponInput;

        Input.Player.Aim.started += OnAimInput;
        Input.Player.Aim.performed += OnAimInput;
        Input.Player.Aim.canceled += OnAimInput;

        Input.Player.Dash.started += OnDashInput;
        Input.Player.Dash.canceled += OnDashInput;

        Input.Player.Interact.started += OnInteractInput;

        Input.Player.One.started += OnInputOne;
        Input.Player.Two.started += OnInputTwo;
        Input.Player.Three.started += OnInputThree;
        Input.Player.Four.started += OnInputFour;
    }

    private void RemoveInputActions()
    {
        Input.Player.Movement.started -= OnMovementInput;
        Input.Player.Movement.performed -= OnMovementInput;
        Input.Player.Movement.canceled -= OnMovementInput;

        Input.Player.Attack.started -= OnAttackInput;
        Input.Player.Attack.canceled -= OnAttackInput;

        Input.Player.SwapWeapon.performed -= OnSwapWeaponInput;

        Input.Player.Aim.started -= OnAimInput;
        Input.Player.Aim.performed -= OnAimInput;
        Input.Player.Aim.canceled -= OnAimInput;

        Input.Player.Dash.started -= OnDashInput;
        Input.Player.Dash.canceled -= OnDashInput;

        Input.Player.Interact.started -= OnInteractInput;
    }

    private void ReceiveDamage(float damage)
    {
        // Debug.Log($"{CurrentHealth} - {damage}");
        if (CurrentHealth > 0)
        {
            if (CurrentHealth - damage > 0)
            {
                CurrentHealth -= damage;
            }
            else
            {
                Health.ReceiveDamage -= ReceiveDamage;
                CurrentHealth = 0;
                FindObjectOfType<GameEndScreen>(true).Open(false);
            }
        }
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        GameEnums.EquipableInput attackValue = (GameEnums.EquipableInput)(int)context.ReadValue<float>();
        AttackInput = attackValue;
    }

    public void OnSwapWeaponInput(InputAction.CallbackContext context)
    {
        if (equipables.Count == 0)
        {
            equipablesIndex = -1;
        }
        else if (equipables.Count == 1)
        {
            equipablesIndex = 0;
        }
        else
        {
            equipablesIndex = (equipablesIndex + 1) % Mathf.Max(equipables.Count, 0);
        }

        // No weapon avalible, Clear Weapon (Should Not Happen In Gameplay)
        if (equipablesIndex == -1)
        {
            EquipedItem.LoadWeapon(null, null);
        }
        else
        {
            EquipedItem.LoadWeapon(equipables[equipablesIndex], OnEnemyDestroyed);
        }
    }

    private void OnEnemyDestroyed(int scoreIncrement)
    {
        CurrentScore += scoreIncrement;
    }

    public void OnAimInput(InputAction.CallbackContext context)
    {
        Vector3 mousePos = Input.Player.Aim.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform)
            {
                Vector3 worldPos = hit.point;
                Vector3 rawLookAtPosition = Vector3.ClampMagnitude(worldPos - transform.position, 5f);
                rawLookAtPosition.y = 0;
                LookAtPosition = rawLookAtPosition;
            }
        }

        /*
        Vector3 cameraAtPlayerheight = mainCamera.transform.position;
        cameraAtPlayerheight.y = transform.position.y;

        // remove unrealted Axis from ray.
        Vector3 zVector = new Vector3(0.0f, ray.direction.y, ray.direction.z);
        Vector3 xVector = new Vector3(ray.direction.x, ray.direction.y, 0.0f);

        // Calculate the angle of the raycast for the X axis and the Z axis
        float zAngle = Vector3.Angle(zVector, Vector3.down) * Mathf.Deg2Rad;
        float xAngle = (Vector3.Angle(xVector, Vector3.left) - 90f) * Mathf.Deg2Rad;

        // The y position simply matchs the player
        float yheight = Vector3.Distance(mainCamera.transform.position, cameraAtPlayerheight);

        // Caculate the z and x position for where the player needs to look
        float zPos = mainCamera.transform.position.z + (Mathf.Tan(zAngle) * yheight);
        float xPos = mainCamera.transform.position.x + (Mathf.Tan(xAngle) * yheight);

        // Set Y pos
        LookAtPosition = Vector3.ClampMagnitude(new Vector3(xPos, transform.position.y, zPos) - transform.position, 5f);
        */
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        DashInput = context.ReadValue<float>() > 0.5f;
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        OnCrateInteracted?.Invoke(Inventory);
    }

    public void OnInputOne(InputAction.CallbackContext context)
    {
        OnCrateQuestAnswered?.Invoke(Inventory, 1);
    }

    public void OnInputTwo(InputAction.CallbackContext context)
    {
        OnCrateQuestAnswered?.Invoke(Inventory, 2);
    }

    public void OnInputThree(InputAction.CallbackContext context)
    {
        OnCrateQuestAnswered?.Invoke(Inventory, 3);
    }

    public void OnInputFour(InputAction.CallbackContext context)
    {
        OnCrateQuestAnswered?.Invoke(Inventory, 4);
    }
}

