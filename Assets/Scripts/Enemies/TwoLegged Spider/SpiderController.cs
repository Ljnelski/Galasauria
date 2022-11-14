/*  Filename:           TestNavMeshAgent.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 9th, 2022
 *  Description:        Spider Controller
 *  Revision History:   November 9th, 2022 (Liam Nelski): Initial script.
 *                      November 10th 2022 (Liam Nelski): Added Chase and Idle State
 *                      November 12th 2022 (Liam Nelski): Added SpiderContext to store Stats
 */
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SpiderController : BaseController<SpiderController>
{
    // Scripts
    public NavMeshAgent Agent { get; private set; }
    public HealthSystem SpiderHealth { get; private set; }
    // States
    public SpiderChaseState ChaseState { get; private set; }
    public SpiderIdleState IdleState { get; private set; }

    // Editor Accessable
    [SerializeField]
    private SpiderContext spiderContext;

    // Spider Input
    public Transform ChaseTarget { get; private set; }

    // Spider Stats
    public float MaxHealth { get => spiderContext._maxHealth; }
    public float CurrentHealth { get => spiderContext._currentHealth; }
    public float BaseSpeed { get => spiderContext._baseSpeed; }
    public float TurnSpeed { get => spiderContext._turnSpeed; }
    public float Acceleration { get => spiderContext._acceleration; }
    public float BaseDamage { get => spiderContext._baseDamage; }  
    public float DetectionRange { get => spiderContext._detectionRange; }
    // Start is called before the first frame update
    void Awake()
    {
        // SetUp Agent
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = BaseSpeed;
        Agent.angularSpeed = TurnSpeed;
        Agent.acceleration = Acceleration;

        // Init States
        ChaseState = new SpiderChaseState(this);
        IdleState = new SpiderIdleState(this);

        FindPlayer();

        activeState = IdleState;
        activeState.OnStateEnter();
    }   
    public bool PlayerInRange()
    {
        if(ChaseTarget == null)        
            return false;
        return Vector3.Distance(transform.position, ChaseTarget.position) <= DetectionRange;
    }
    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if(player == null)
        {
            Debug.LogError("SpiderController ERROR: Spider Controller Cannot find player in Scene, Chase Behaviour will not work");
            ChaseTarget = null;
            
        } else
        {
            ChaseTarget = player.transform;
        }
    }
    private void OnDrawGizmos()
    {
        // Target
        if (ChaseTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(ChaseTarget.position, 3f);
        }
        // View Range
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}