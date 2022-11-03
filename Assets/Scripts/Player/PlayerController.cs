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
    public EquipedItem EquipedItem { get; private set; }
    public Rigidbody Rb { get; private set; }
        
    private PlayerState activeState;
    private int equipablesIndex = -1;
    private Camera mainCamera;

    // States
    public PlayerIdleState idleState;


    // TODO => Move to Scriptable Object to be able to move across levels
    // Player Values
    public Transform weaponSlot;
    public GameEnums.EquipableInput attackInput;
    public Vector3 lookAtPosition;
    public Vector2 movementInput;
    public float speed = 5f;
    public float turnSpeed = 90f;
    public List<GameObject> equipables;

    private void Awake()
    {
        Input = new PlayerInputActions();
        Input.Player.Enable();

        idleState = new PlayerIdleState(this);       

        Rb = GetComponent<Rigidbody>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        EquipedItem = weaponSlot.GetComponentInChildren<EquipedItem>();

        Input.Player.Movement.started += OnMovementInput;
        Input.Player.Movement.performed += OnMovementInput;
        Input.Player.Movement.canceled += OnMovementInput;

        Input.Player.Attack.started += OnAttackInput;
        Input.Player.Attack.canceled += OnAttackInput;

        Input.Player.SwapWeapon.performed += OnSwapWeapon;

        Input.Player.Aim.started += OnAim;
        Input.Player.Aim.performed += OnAim;
        Input.Player.Aim.canceled += OnAim;

        activeState = idleState;
        activeState.OnStateEnter();
    }

    private void FixedUpdate()
    {
        activeState.OnStateRun();
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }    

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        GameEnums.EquipableInput attackValue = (GameEnums.EquipableInput)(int)context.ReadValue<float>();
        attackInput = attackValue;
    }

    public void OnSwapWeapon(InputAction.CallbackContext context)
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

    public void OnAim(InputAction.CallbackContext context) 
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
        lookAtPosition = Vector3.ClampMagnitude(new Vector3(xPos, transform.position.y, zPos) - transform.position, 5f);

    }

    public void ChangeState(PlayerState newState)
    { 
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
        controller.Rb.AddForce(new Vector3(controller.movementInput.x, 0, controller.movementInput.y) * controller.speed, ForceMode.Force);
  
        if(controller.EquipedItem != null && controller.attackInput != GameEnums.EquipableInput.NONE && !controller.EquipedItem.InUse)
        {
            controller.EquipedItem.UseItem(controller.attackInput);
        }

        // Look At Mouse Position
        Quaternion rotation = Quaternion.LookRotation(controller.lookAtPosition);
        controller.Rb.MoveRotation(Quaternion.RotateTowards(controller.transform.rotation, rotation, controller.turnSpeed));
    }
}

