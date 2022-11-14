using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingMainCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation; // Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
