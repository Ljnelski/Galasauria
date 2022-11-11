/*  Filename:           TestNavMeshAgent.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 9th, 2022
 *  Description:        Spider Controller
 *  Revision History:   November 9th, 2022 (Liam Nelski): Initial script.
 *                      Novemeber 10th 2022 (Liam Nelski): Added Chase and Idle State
 */
using UnityEngine;
using UnityEngine.AI;

public class SpiderController : BaseController<SpiderController>
{
    public Transform playerTransform;    
    public NavMeshAgent Agent { get; private set; }
    public SpiderChaseState ChaseState { get; private set; }
    public SpiderIdleState IdleState { get; private set; }

    public float detectionRange = 8f;
    // Start is called before the first frame update
    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();

        ChaseState = new SpiderChaseState(this);
        IdleState = new SpiderIdleState(this);

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        activeState = IdleState;
        activeState.OnStateEnter();
    }
    public bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= detectionRange;
    }

    private void OnDrawGizmos()
    {
        // Target
        if (playerTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerTransform.position, 3f);
        }

        // View Range
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
