/*  Filename:           TestNavMeshAgent.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 9th, 2022
 *  Description:        TestNavMesh to learn how it works
 *  Revision History:   November 9th, 2022 (Liam Nelski): Initial script.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavMeshAgent : MonoBehaviour
{
    public Transform movePostionTransform;

    private NavMeshAgent agent;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = movePostionTransform.position;
    }
}
