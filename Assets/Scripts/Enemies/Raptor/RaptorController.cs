/*  Filename:           RaptorController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        Raptor Controller
 *  Revision History:   November 26, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaptorController : BaseController<RaptorController>
{
    // Scripts
    public NavMeshAgent Agent { get; private set; }
    public HealthSystem RaptorHealth { get; private set; }
    public ActionTimerPool Timers { get; private set; }

    // States
    public RaptorChaseState ChaseState { get; private set; }
    public RaptorIdleState IdleState { get; private set; }
    public RaptorAttackState AttackState { get; private set; }
    public RaptorDieState DieState { get; private set; }

    // Editor Accessable
    [SerializeField] private RaptorContext raptorContext;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip attackClip;

    // Raptor Input
    public Transform ChaseTarget { get; private set; }

    // Raptor Stats

    public float MaxHealth { get => raptorContext._maxHealth; }

    // Directly using to raptor context means that all raptors shares the same scriptable object data
    // 1. If one raptor has 0 health, all raptors have 0 health
    // 2. The raptor context scriptable object retains the last value even when we close the game
    // 3. We have to reset the current health to max health every time an enemy is borned
    // Because of these, current health keeps as a separate int and max health is assigned instead
    // But please feel free to change if you have other idea
    public float CurrentHealth { get; set; }
    public float BaseSpeed { get => raptorContext._baseSpeed; }
    public float TurnSpeed { get => raptorContext._turnSpeed; }
    public float Acceleration { get => raptorContext._acceleration; }
    public float DestinationOffsetDistance { get => raptorContext._destinationOffsetDistance; }
    public float PlayerOffsetDistance { get => raptorContext._playerOffsetDistance; }
    public float AttackInterval { get => raptorContext._attackCoolDownMiliseconds; }
    public float DieInterval { get => raptorContext._dieMiliseconds; }
    public float BaseDamage { get => raptorContext._baseDamage; }
    public float DetectionRange { get => raptorContext._detectionRange; }

    public Vector3 Destination { get; private set; }
    private Vector3 destinationBehind;
    private Vector3 destinationLeft;
    private Vector3 destinationRight;

    private Animator animator;
    private Destroyer destroyer;
    private AudioSource growlAudio;

    // Start is called before the first frame update
    void Awake()
    {
        // Assign max health to current health
        CurrentHealth = raptorContext._maxHealth;

        // SetUp Timers
        Timers = GetComponent<ActionTimerPool>();

        // SetUp Animator
        animator = GetComponentInChildren<Animator>();

        // SetUp Audio
        growlAudio = GetComponent<AudioSource>();

        // SetUp Agent
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = BaseSpeed;
        Agent.angularSpeed = TurnSpeed;
        Agent.acceleration = Acceleration;

        // SetUp Health
        RaptorHealth = GetComponent<HealthSystem>();
        RaptorHealth.ReceiveDamage += ReceiveDamage;

        // SetUp Destroyer
        destroyer = GetComponentInChildren<Destroyer>();
        if (destroyer != null)
        {
            destroyer.Damage = BaseDamage;
        }

        // Init States
        ChaseState = new RaptorChaseState(this);
        IdleState = new RaptorIdleState(this);
        AttackState = new RaptorAttackState(this);
        DieState = new RaptorDieState(this);

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

    public void EnableDestroyer(bool enable)
    {
        destroyer.gameObject.SetActive(enable);
    }

    public void Attack()
    {
        if (animator)
        {
            animator.SetTrigger("attack");
            growlAudio.clip = attackClip;
            growlAudio.Play();
        }
    }

    public void Chase()
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

    public void Die()
    {
        if (animator)
        {
            animator.SetTrigger("die");
            growlAudio.clip = deathClip;
            growlAudio.Play();
        }
    }

    public bool ReachedDestination()
    {
        if (ChaseTarget == null)
            return false;
        return Vector3.Distance(transform.position, Destination) <= DestinationOffsetDistance;
    }

    public bool PlayerInRange()
    {
        if (ChaseTarget == null)
            return false;
        return Vector3.Distance(transform.position, ChaseTarget.position) <= DetectionRange;
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("RaptorController ERROR: Raptor Controller Cannot find player in Scene, Chase Behaviour will not work");
            ChaseTarget = null;

        }
        else
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
                RaptorHealth.ReceiveDamage -= ReceiveDamage;
                CurrentHealth = 0;
                RaptorHealth.Die?.Invoke(raptorContext._score);
            }
        }
    }

    public void UpdateDestination()
    {
        destinationBehind = ChaseTarget.TransformPoint(-Vector3.forward * PlayerOffsetDistance);
        destinationLeft = ChaseTarget.TransformPoint(-Vector3.right * PlayerOffsetDistance);
        destinationRight = ChaseTarget.TransformPoint(Vector3.right * PlayerOffsetDistance);

        float behindDestinationDistance = Vector3.Distance(transform.position, destinationBehind);
        float leftDestinationDistance = Vector3.Distance(transform.position, destinationLeft);
        float rightDestinationDistance = Vector3.Distance(transform.position, destinationRight);

        Vector3 comparedDestination = transform.position;
        float shortestDistance = Mathf.Infinity;

        if (behindDestinationDistance < shortestDistance)
        {
            shortestDistance = behindDestinationDistance;
            comparedDestination = destinationBehind;
        }
        if (leftDestinationDistance < shortestDistance)
        {
            shortestDistance = leftDestinationDistance;
            comparedDestination = destinationLeft;
        }
        if (rightDestinationDistance < shortestDistance)
        {
            shortestDistance = rightDestinationDistance;
            comparedDestination = destinationRight;
        }

        Destination = comparedDestination;
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
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
