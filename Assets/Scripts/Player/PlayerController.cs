using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody rigidBody;
    private PlayerInputActions playerInputActions;

    // Start is called before the first frame update

    private void Awake()
    {        
        rigidBody = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();


        playerInputActions.Player.Enable();        
        playerInputActions.Player.Attack1.performed += AttackPerformed;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        
        rigidBody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    public void AttackPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("HyiYA");
    }
}
