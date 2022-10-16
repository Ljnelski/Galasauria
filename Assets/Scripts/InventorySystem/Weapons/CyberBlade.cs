using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberBlade : MonoBehaviour
{
    private Transform blade;
    private Transform hilt;
    private Transform handle;

    private void Awake()
    {
        handle = transform.GetChild(0).GetChild(0).GetChild(0);
        hilt = handle.GetChild(0);
        blade = hilt.GetChild(0);

        ScaleHandle(2);
    }

    private void ScaleHandle(float factor)
    {
        handle.localScale = new Vector3(handle.localScale.x, handle.localScale.y * factor, handle.localScale.z);
        hilt.localScale = new Vector3(handle.localScale.x, handle.localScale.y / factor, handle.localScale.z);
        blade.localScale = new Vector3(blade.localScale.x, blade.localScale.y / factor, blade.localScale.z);
    }
}
