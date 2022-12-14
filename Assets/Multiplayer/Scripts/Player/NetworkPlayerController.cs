/*  Filename:           NetworkPlayerController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

using Cinemachine;
using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetworkPlayerController : BaseNetworkController<NetworkPlayerController>
{
    public enum eNetworkPlayerState
    {
        NONE = 0,
        IDLE = 1,
        USE_ITEM = 2,
        DASH_STATE = 3,
    }

    // Scripts
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public NetworkEquipSlot EquipedItem { get; private set; }
    [field: SerializeField] public Rigidbody Rb { get; private set; }
    [field: SerializeField] public HealthSystem Health { get; private set; }
    [field: SerializeField] public Inventory Inventory { get; private set; }
    [field: SerializeField] public ActionTimerPool Timers { get; private set; }

    [Header("Animation Constraints")]
    [field: SerializeField] private Transform LookLocation;
    public Vector3 lastDirectionFaced;
    [field: SerializeField] private Transform FacingLocation;


    // States
    public NetworkPlayerIdleState idleState;
    public NetworkPlayerUseItemState useItemState;
    public NetworkPlayerDashState dashState;

    // Editor Accessable
    public PlayerContext playerContext;

    // Player Input
    public PlayerInputActions Input { get; private set; }
    public GameEnums.EquipableInput AttackInput { get { return networkAttackInput.Value; } }
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

    // Network
    private NetworkVariable<Vector3> networkLookAtPosition = new NetworkVariable<Vector3>();
    private NetworkVariable<Vector3> networkFacingLocalPosition = new NetworkVariable<Vector3>();
    private NetworkVariable<eNetworkPlayerState> networkPlayerActiveState = new NetworkVariable<eNetworkPlayerState>();
    private NetworkVariable<GameEnums.EquipableInput> networkAttackInput = new NetworkVariable<GameEnums.EquipableInput>();
    private eNetworkPlayerState networkPlayerState;

    private void Awake()
    {
        // Hopefully temporary
        Inventory.OnAddItem += (ItemData data) => { CurrentScore += data.score; };

        // SetUp Health
        Health = GetComponent<HealthSystem>();
        Health.ReceiveDamage += ReceiveDamage;

        // Reset current health
        CurrentHealth = MaxHealth;

        // Reset score
        CurrentScore = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(FacingLocation.localPosition, 1f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Rb.velocity.normalized, 1f);
        Gizmos.color = Color.blue;
    }

    private void Update()
    {
        if (IsClient)
        {
            LookLocation.position = networkLookAtPosition.Value;
            FacingLocation.localPosition = networkFacingLocalPosition.Value;

            if (!networkPlayerState.Equals(networkPlayerActiveState.Value))
            {
                networkPlayerState = networkPlayerActiveState.Value;
                SwitchState();                
            }
        }
    }

    private void SwitchState()
    {
        switch (networkPlayerActiveState.Value)
        {
            case eNetworkPlayerState.NONE:
                break;
            case eNetworkPlayerState.IDLE:
                ChangeState(idleState);
                break;
            case eNetworkPlayerState.USE_ITEM:
                ChangeState(useItemState);
                break;
            case eNetworkPlayerState.DASH_STATE:
                ChangeState(dashState);
                break;
            default:
                Debug.LogWarning($"NetworkPlayerController ERROR. eNetworkPlayerState {networkPlayerActiveState.Value} did not handled in ChangeState. Please update the method.");
                break;
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsHost)
        {
            GameObject hostSpawnPoint = GameObject.FindGameObjectWithTag("HostSpawnPoint");
            if (hostSpawnPoint != null)
            {
                transform.position = hostSpawnPoint.transform.position;
            }
        }
        else if (IsClient)
        {
            GameObject clientSpawnPoint = GameObject.FindGameObjectWithTag("ClientSpawnPoint");
            if (clientSpawnPoint != null)
            {
                transform.position = clientSpawnPoint.transform.position;
            }
        }

        if (IsClient)
        {
            idleState = new NetworkPlayerIdleState(this);
            useItemState = new NetworkPlayerUseItemState(this);
            dashState = new NetworkPlayerDashState(this);
        }

        playerContext._currentSpeed = BaseSpeed; // TODO, it is weird

        if (IsClient && IsOwner)
        {
            // Create and enable input
            Input = new PlayerInputActions();
            Input.Player.Enable();
            AddInputActions();

            // Update the follow variable of cinemachine
            CinemachineVirtualCamera virtualPlayerFollowCamera = FindObjectOfType<CinemachineVirtualCamera>();            
            virtualPlayerFollowCamera.Follow = transform;

            // Update intial player state
            UpdatePlayerActiveStateServerRpc(eNetworkPlayerState.IDLE);

            WireUpUI();
        }

        SwapWeapon();
    }

    private void WireUpUI()
    {
        // Set up the health bar
        NetworkPlayerHealthBar healthBar = FindObjectOfType<NetworkPlayerHealthBar>();
        healthBar.SetUp(this);

        // Set up the dash bar
        NetworkPlayerDashBar dashBar = FindObjectOfType<NetworkPlayerDashBar>();
        dashBar.SetUp(this);

        // Set up the notification controller
        NetworkNotificationController notificationController = FindObjectOfType<NetworkNotificationController>();
        notificationController.SetUp(Inventory);

        // Set up the inventory screen
        NetworkInventoryScreen inventoryScreen = FindObjectOfType<NetworkInventoryScreen>(true);
        inventoryScreen.SetUp(Inventory);

        // Set up the score counter
        NetworkScoreCounter scoreCounter = FindObjectOfType<NetworkScoreCounter>();
        scoreCounter.SetUp(this);

        // Set up the final score counter
        NetworkFinalScoreCounter finalScoreCounter = FindObjectOfType<NetworkFinalScoreCounter>(true);
        finalScoreCounter.SetUp(this);

        // Set up the inventory item details display
        NetworkInventoryItemDetailsDisplay inventoryItemDetailsDisplay = FindObjectOfType<NetworkInventoryItemDetailsDisplay>(true);
        inventoryItemDetailsDisplay.SetUp(this);
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

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        // avoid null reference upon stopping the game or return to the start scene which results from OnAimInput
        RemoveInputActions();
    }

    #region server rpc
    [ServerRpc]
    private void UpdateLookAtPositionServerRpc(Vector3 lookAtPosition)
    {
        networkLookAtPosition.Value = lookAtPosition;
    }

    [ServerRpc]
    private void UpdateFacingLocalPositionServerRpc(Vector3 facingLocalPosition)
    {
        networkFacingLocalPosition.Value = facingLocalPosition;
    }

    [ServerRpc]
    public void UpdatePlayerActiveStateServerRpc(eNetworkPlayerState playerState)
    {
        networkPlayerActiveState.Value = playerState;
    }

    [ServerRpc]
    private void UpdateAttackInputServerRpc(GameEnums.EquipableInput attackInput)
    {
        networkAttackInput.Value = attackInput;
    }
    #endregion server rpc

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

    public void NetworkUpgrade<TUpgradeData>(NetworkUpgrade<TUpgradeData> upgrade) where TUpgradeData : ScriptableObject
    {
        upgrade.DoUpgrade(this);
    }

    private void ReceiveDamage(float damage)
    {
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

                if (IsOwner)
                {
                    FindObjectOfType<GameEndScreen>(true).Open(false);
                }
            }
        }
    }

    private void OnEnemyDestroyed(int scoreIncrement)
    {
        if (IsOwner)
        {
            CurrentScore += scoreIncrement;
        }
    }

    private void SetLookAtPosition(Vector3 lookAtPosition)
    {        
        // Update NetworkVariable
        UpdateLookAtPositionServerRpc(lookAtPosition);
    }

    public void SetFacingLocalPosition(Vector3 facingLocalPosition)
    {
        // Update NetworkVariable
        UpdateFacingLocalPositionServerRpc(facingLocalPosition);
    }

    public Vector3 GetFacingLocalPosition()
    {
        return FacingLocation.localPosition;
    }

    public Vector3 GetFacingPosition()
    {
        return FacingLocation.position;
    }

    public void BoostSpeed(float boostTime)
    {
        // Create Action Timer to reset player Speed
        Timers.CreateTimer(boostTime / 1000f, () =>
        {
            CurrentSpeed = BaseSpeed;
        });
    }

    #region Input actions check IsOwner before performing
    public void OnMovementInput(InputAction.CallbackContext context)
    {
        if (IsOwner)
        {
            MovementInput = context.ReadValue<Vector2>();
            if (MovementInput.magnitude < 0.01f)
            {
                return;
            }
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (IsOwner)
        {
            GameEnums.EquipableInput attackValue = (GameEnums.EquipableInput)(int)context.ReadValue<float>();

            // Update NetworkVariable
            UpdateAttackInputServerRpc(attackValue);
        }
    }

    public void OnSwapWeaponInput(InputAction.CallbackContext context)
    {
        SwapWeapon();
    }

    private void SwapWeapon()
    {
        if (IsOwner)
        {
            EquipedItem.LoadWeapon(OnEnemyDestroyed, Inventory);
        }
    }

    public void OnAimInput(InputAction.CallbackContext context)
    {
        if (IsOwner)
        {
            Vector3 mousePos = Input.Player.Aim.ReadValue<Vector2>();

            Ray ray = Camera.main.ScreenPointToRay(mousePos);


            Vector3 cameraAtPlayerheight = Camera.main.transform.position;
            cameraAtPlayerheight.y = transform.position.y;

            // remove unrealted Axis from ray.
            Vector3 zVector = new Vector3(0.0f, ray.direction.y, ray.direction.z);
            Vector3 xVector = new Vector3(ray.direction.x, ray.direction.y, 0.0f);

            // Calculate the angle of the raycast for the X axis and the Z axis
            float zAngle = Vector3.Angle(zVector, Vector3.down) * Mathf.Deg2Rad;
            float xAngle = (Vector3.Angle(xVector, Vector3.left) - 90f) * Mathf.Deg2Rad;

            // The y position simply matchs the player
            float yheight = Vector3.Distance(Camera.main.transform.position, cameraAtPlayerheight);

            // Caculate the z and x position for where the player needs to look
            float zPos = Camera.main.transform.position.z + (Mathf.Tan(zAngle) * yheight);
            float xPos = Camera.main.transform.position.x + (Mathf.Tan(xAngle) * yheight);

            // Set the Target for the AimConstrant
            Vector3 newLookAtPosition = transform.position + Vector3.Normalize(new Vector3(xPos, transform.position.y, zPos) - transform.position);

            SetLookAtPosition(newLookAtPosition);
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (IsOwner)
        {
            DashInput = context.ReadValue<float>() > 0.5f;
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (IsOwner)
        {
            OnCrateInteracted?.Invoke(Inventory);
        }
    }

    public void OnInputOne(InputAction.CallbackContext context)
    {
        if (IsOwner)
        {
            OnCrateQuestAnswered?.Invoke(Inventory, 1);
        }
    }

    public void OnInputTwo(InputAction.CallbackContext context)
    {
        if (IsOwner)
        {
            OnCrateQuestAnswered?.Invoke(Inventory, 2);
        }
    }

    public void OnInputThree(InputAction.CallbackContext context)
    {
        if (IsOwner)
        {
            OnCrateQuestAnswered?.Invoke(Inventory, 3);
        }
    }

    public void OnInputFour(InputAction.CallbackContext context)
    {
        if (IsOwner)
        {
            OnCrateQuestAnswered?.Invoke(Inventory, 4);
        }
    }
    #endregion Input actions


}
