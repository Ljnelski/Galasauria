/*  Filename:           PlayerController.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 10th, 2022
 *  Description:        Controls the player
 *  Revision History:   October 10th (Liam Nelski): Inital Script.
 *                      October 16th (Liam Nelski): Added Use of Equipable Items
 *                      November 3th (Liam Nelski): Moved Equipable from interface to script
 *                      November 3th (Liam Nelski): Made Player Point to the mouse
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputActions Input { get; private set; }
    public EquipSlot EquipedItem { get; private set; }
    public Rigidbody Rb { get; private set; }
        
    // Monobehaviours
    private PlayerState activeState;
    private Camera mainCamera;

    // State switches
    
    // States
    public PlayerIdleState idleState;
    public PlayerUseItemState useItemState;
    public PlayerDashState dashState;


    // TODO => Move to Scriptable Object to be able to move across levels


    // Player Input
    public GameEnums.EquipableInput AttackInput { get; private set; }
    public Vector3 LookAtPosition { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public bool DashInput { get; private set; }
    // Values that control player behviour
    [Header("Movement")]
    public float baseSpeed = 5f;
    public float turnSpeed = 90f;
    [Header("Dash")]
    public float dashSpeed = 10f;
    public float dashDurationMiliseconds = 1000f;
    public float dashCoolDownMiliseconds = 10000f;
    public float DashCoolDownTimer { get; private set; } = 0f;
   

    public List<GameObject> equipables;
    public int equipablesIndex { get; private set; } = -1;


    private void Awake()
    {
        Input = new PlayerInputActions();
        Input.Player.Enable();

        idleState = new PlayerIdleState(this);
        useItemState = new PlayerUseItemState(this);
        dashState = new PlayerDashState(this);  

        Rb = GetComponent<Rigidbody>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        EquipedItem = GetComponentInChildren<EquipSlot>();

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

        activeState = idleState;
        activeState.OnStateEnter();
    }

    private void FixedUpdate()
    {
        activeState.OnStateRun();
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
        Debug.Log("equipable.Count: " + equipables.Count);
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
        if(equipablesIndex == -1)
        {
            EquipedItem.LoadWeapon(null);
        }
        else
        {
            EquipedItem.LoadWeapon(equipables[equipablesIndex]);
        }
    }

    public void OnAimInput(InputAction.CallbackContext context) 
    {
        Vector3 mousePos = Input.Player.Aim.ReadValue<Vector2>();

        Ray ray = mainCamera.ScreenPointToRay(mousePos);

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

    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        DashInput = context.ReadValue<float>() > 0.5f;
    }

    public void ChangeState(PlayerState newState)
    {
        Debug.Log("Changing State");

        activeState.OnStateExit();

        activeState = newState;

        activeState.OnStateEnter();
    }
}

/*  Author:             Liam Nelski (301064116)
 *  Last Update:        October 10th, 2022
 *  Description:        Basic playerState that all other can inherit from to reducee duplicate code
 */
public abstract class PlayerState : BaseState<PlayerController>
{
    public PlayerState(PlayerController playerController) : base(playerController)
    {
        ;
    }

    protected void MovePlayer()
    {
        context.Rb.velocity = Vector3.Slerp(context.Rb.velocity, new Vector3(context.MovementInput.x, 0, context.MovementInput.y) * context.baseSpeed, 0.1f);
    }

    protected void RotatePlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(context.LookAtPosition);
        context.Rb.MoveRotation(Quaternion.RotateTowards(context.transform.rotation, rotation, context.turnSpeed));
    }
}


public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController playerController) : base(playerController)
    {

    }
    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {
        ;
    }

    public override void OnStateRun()
    {
        MovePlayer();
  
        if(context.EquipedItem.ItemEquiped && context.AttackInput != GameEnums.EquipableInput.NONE && !context.EquipedItem.InUse)
        {
            context.ChangeState(context.useItemState);
        }

        if(context.DashInput && context.DashCoolDownTimer <= 0)
        {
            context.ChangeState(context.dashState);
        }

        // Look At Mouse Position
        RotatePlayer();
    }
}

public class PlayerUseItemState : PlayerState
{
    public PlayerUseItemState(PlayerController playerController) : base(playerController)
    {
    }

    public override void OnStateEnter()
    {
        context.EquipedItem.UseItem(context.AttackInput);
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateRun()
    {
        MovePlayer();
        RotatePlayer();

        if(!context.EquipedItem.InUse)
        {
            context.ChangeState(context.idleState);
        }
    }
}

public class PlayerDashState : PlayerState
{
    private float currentTime = 0f;
    private Vector3 dashDirection;

    public PlayerDashState(PlayerController playerController) : base(playerController)
    {
    }

    public override void OnStateEnter()
    {
        dashDirection = new Vector3(context.MovementInput.x, 0.0f, context.MovementInput.y);
        currentTime = 0f;
    }

    public override void OnStateExit()
    {
        context.Rb.velocity = Vector3.zero;
    }

    public override void OnStateRun()
    {
        currentTime += Time.fixedDeltaTime * 1000;
        Debug.Log("currentTime: " + currentTime);
        if (currentTime >= context.dashDurationMiliseconds)
        {
            context.ChangeState(context.idleState);
        }
        else
        {
            context.Rb.velocity = dashDirection * context.dashSpeed;
        }
    }
}

