/*  Filename:           RayCastFromCamera.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 3, 2022
 *  Description:        File For Testing how to Get Point for Character to look at
 *  Revision History:   Novemeber 3, 2022 (Liam Nelski): Initial script. *                     
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePositionInWorld : MonoBehaviour
{
    public float rayLength = 1f;

    private PlayerInputActions Input { get; set; }
    private Camera playCamera;
    private RayDrawer rayDrawer;
    private WireSphereDrawer wireSphereDrawer;

    // Start is called before the first frame update

    private void Awake()
    {
        playCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        rayDrawer = GetComponent<RayDrawer>();
        rayDrawer.active = true;

        wireSphereDrawer = GetComponent<WireSphereDrawer>();
        wireSphereDrawer.active = true;

        Input = new PlayerInputActions();
        Input.Player.Enable();
    }

    private void Start()
    {
        rayDrawer.startPos = playCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.Player.Aim.ReadValue<Vector2>();

        Ray ray = playCamera.ScreenPointToRay(mousePos);

        rayDrawer.rayDir = ray.direction;
        rayDrawer.RayLength = rayLength;

        float playerY = GameObject.FindGameObjectWithTag("Player").transform.position.y;
        Vector3 ground = playCamera.transform.position;
        ground.y = playerY;


        // remove unrealted Axis from ray.
        Vector3 zVector = new Vector3(0.0f, ray.direction.y, ray.direction.z);
        Vector3 xVector = new Vector3(ray.direction.x, ray.direction.y, 0.0f);

        // Calculate the angle of the raycast for the X axis and the Z axis
        float zAngle = Vector3.Angle(zVector, Vector3.down) * Mathf.Deg2Rad;
        float xAngle = (Vector3.Angle(xVector, Vector3.left) - 90f) * Mathf.Deg2Rad;

        // The y position simply matchs the player
        float yPos = Vector3.Distance(playCamera.transform.position, ground);

        // Caculate the z and x position for where the player needs to look
        float zPos = playCamera.transform.position.z + (Mathf.Tan(zAngle) * yPos);
        float xPos = playCamera.transform.position.x + (Mathf.Tan(xAngle) * yPos);

        // TODO Clamp the values around the player
        //xPos = Mathf.Clamp(xPos, -5, 5);
        //zPos = Mathf.Clamp(xPos, -5, 5);


        wireSphereDrawer.ClearSpheres();

        wireSphereDrawer.AddSphere(new Vector3(xPos, ground.y, zPos), 5.0f, Color.white);
        wireSphereDrawer.AddSphere(new Vector3(xPos, ground.y, 0.0f), 5.0f, Color.red);
        wireSphereDrawer.AddSphere(new Vector3(0.0f, ground.y, zPos), 5.0f, Color.blue);
        wireSphereDrawer.AddSphere(ground, 5.0f, Color.green);
    }

    private void OnAimInput(InputAction.CallbackContext context)
    {

    }
}
