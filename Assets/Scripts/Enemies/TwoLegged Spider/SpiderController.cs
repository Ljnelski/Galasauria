/*  Filename:           TestNavMeshAgent.cs
 *  Author:             Liam Nelski (301064116), Yuk Yee Wong (301234795)
 *  Last Update:        November 25th, 2022
 *  Description:        Spider Controller
 *  Revision History:   November 9th, 2022 (Liam Nelski): Initial script.
 *                      November 10th 2022 (Liam Nelski): Added Chase and Idle State
 *                      November 12th 2022 (Liam Nelski): Added SpiderContext to store Stats
 *                      November 25th, 2022 (Yuk Yee Wong): Fixed bugs. Not direcly use context health for CurrentHealth. Added attack state, reach player method; replaced suicide method to receive damage; added animator, destroy set up, animation methods.
 */
using System;
using UnityEngine;
using UnityEngine.AI;

public class SpiderController : BaseController<SpiderController>
{
    // Scripts
    public NavMeshAgent Agent { get; private set; }
    public HealthSystem SpiderHealth { get; private set; }
    public TimerPool Timers { get; private set; }

    // States
    public SpiderChaseState ChaseState { get; private set; }
    public SpiderIdleState IdleState { get; private set; }
    public SpiderAttackState AttackState { get; private set; }

    // Editor Accessable
    [SerializeField]
    private SpiderContext spiderContext;

    // Spider Input
    public Transform ChaseTarget { get; private set; }

    // Spider Stats

    public float MaxHealth { get => spiderContext._maxHealth; }

    // Directly using to spider context means that all spiders shares the same scriptable object data
    // 1. If one spider has 0 health, all spiders have 0 health
    // 2. The spider context scriptable object retains the last value even when we close the game
    // 3. We have to reset the current health to max health every time an enemy is borned
    // Because of these, current health keeps as a separate int and max health is assigned instead
    // But please feel free to change if you have other idea
    public float CurrentHealth { get; set; }
    public float BaseSpeed { get => spiderContext._baseSpeed; }
    public float TurnSpeed { get => spiderContext._turnSpeed; }
    public float Acceleration { get => spiderContext._acceleration; }
    public float PlayerOffsetDistance { get => spiderContext._playerOffsetDistance; }
    public float AttackInterval { get => spiderContext._attackCoolDownMiliseconds; }
    public float BaseDamage { get => spiderContext._baseDamage; }  
    public float DetectionRange { get => spiderContext._detectionRange; }

    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        // Assign max health to current health
        CurrentHealth = spiderContext._maxHealth;

        // SetUp Timers
        Timers = GetComponent<TimerPool>();

        // SetUp Animator
        animator = GetComponent<Animator>();

        // SetUp Agent
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = BaseSpeed;
        Agent.angularSpeed = TurnSpeed;
        Agent.acceleration = Acceleration;

        // SetUp Health
        SpiderHealth = GetComponent<HealthSystem>();
        SpiderHealth.ReceiveDamage += ReceiveDamage;

        // SetUp Destroyer
        Destroyer destroyer = GetComponentInChildren<Destroyer>();
        if (destroyer != null)
        {
            destroyer.Damage = BaseDamage;
        }

        // Init States
        ChaseState = new SpiderChaseState(this);
        IdleState = new SpiderIdleState(this);
        AttackState = new SpiderAttackState(this);

        FindPlayer();

        activeState = IdleState;
        activeState.OnStateEnter();
    }   

    public void FaceTarget()
    {
        Vector3 direction = (ChaseTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * TurnSpeed);
    }

    public void Attack()
    {
        if (animator)
        {
            animator.SetTrigger("attack");
        }
    }

    public void Walk()
    {
        if (animator)
        {
            animator.SetFloat("velocity", 1f);
        }
    }

    public void Idle()
    {
        if (animator)
        {
            animator.SetFloat("velocity", 0f);
        }
    }

    public bool ReachedPlayer()
    {
        if (ChaseTarget == null)
            return false;
        return Vector3.Distance(transform.position, ChaseTarget.position) <= PlayerOffsetDistance;
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

    private void ReceiveDamage(float damage)
    {
        // Debug.Log($"{CurrentHealth} - {damage}");
        if (CurrentHealth > 0)
        {
            if (CurrentHealth - damage > 0)
            {
                CurrentHealth -= damage;
            }
            else
            {
                SpiderHealth.ReceiveDamage -= ReceiveDamage;
                CurrentHealth = 0;
                SpiderHealth.Die?.Invoke(spiderContext._score);
                Destroy(gameObject);
            }
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