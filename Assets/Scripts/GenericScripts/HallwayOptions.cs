using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayOptions : MonoBehaviour
{
    [SerializeField] private GameObject _hallway1;
    [SerializeField] private GameObject _hallway2;
    [SerializeField] private GameObject _hallway3;
    [SerializeField] private GameObject _hallway4;

    [SerializeField] private bool _showWall1;
    [SerializeField] private bool _showWall2;
    [SerializeField] private bool _showWall3;
    [SerializeField] private bool _showWall4;

    public void UpdateWalls()
    {
        _hallway1.SetActive(_showWall1);
        _hallway2.SetActive(_showWall2);
        _hallway3.SetActive(_showWall3);
        _hallway4.SetActive(_showWall4);
    }
}
