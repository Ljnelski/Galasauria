/*  Filename:           ApatosaurusController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 12, 2022
 *  Description:        Apatosaurus Controller
 *  Revision History:   December 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ApatosaurusController : BaseController<ApatosaurusController>
{
    // Scripts
    public NavMeshAgent Agent { get; private set; }
    public HealthSystem ApatosaurusHealth { get; private set; }
    public ActionTimerPool Timers { get; private set; }

    // States
    public ApatosaurusEvadeState EvadeState { get; private set; }
    public ApatosaurusIdleState IdleState { get; private set; }
    public ApatosaurusDieState DieState { get; private set; }

    // Editor Accessable
    [SerializeField] private ApatosaurusContext apatosaurusContext;
    [SerializeField] private AudioClip deathClip;

    // Apatosaurus Input
    public Transform EvadeTarget { get; private set; }

    // Apatosaurus Stats

    public float MaxHealth { get => apatosaurusContext._maxHealth; }

    // Directly using to Apatosaurus context means that all Apatosauruss shares the same scriptable object data
    // 1. If one Apatosaurus has 0 health, all Apatosauruss have 0 health
    // 2. The Apatosaurus context scriptable object retains the last value even when we close the game
    // 3. We have to reset the current health to max health every time an enemy is borned
    // Because of these, current health keeps as a separate int and max health is assigned instead
    // But please feel free to change if you have other idea
    public float CurrentHealth { get; set; }
    public float BaseSpeed { get => apatosaurusContext._baseSpeed; }
    public float TurnSpeed { get => apatosaurusContext._turnSpeed; }
    public float Acceleration { get => apatosaurusContext._acceleration; }
    public float DestinationOffsetDistance { get => apatosaurusContext._destinationOffsetDistance; }
    public float PlayerOffsetDistance { get => apatosaurusContext._playerOffsetDistance; }
    public float DieInterval { get => apatosaurusContext._dieMiliseconds; }
    public float DetectionRange { get => apatosaurusContext._detectionRange; }
    public RandomListItemCollectableData RandomRewards { get => apatosaurusContext._randomCollectable; }

    public Vector3 Destination { get; private set; }

    private Animator animator;
    private Destroyer destroyer;
    private AudioSource growlAudio;

    // Start is called before the first frame update
    void Awake()
    {
        // Assign max health to current health
        CurrentHealth = apatosaurusContext._maxHealth;

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
        ApatosaurusHealth = GetComponent<HealthSystem>();
        ApatosaurusHealth.ReceiveDamage += ReceiveDamage;

        // Init States
        EvadeState = new ApatosaurusEvadeState(this);
        IdleState = new ApatosaurusIdleState(this);
        DieState = new ApatosaurusDieState(this);

        FindPlayer();
        activeState = IdleState;
        activeState.OnStateEnter();
    }

    public void FaceTarget()
    {
        Vector3 direction = (EvadeTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * TurnSpeed);
    }

    public void Evade()
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

    public bool ReachedEvadeDestination()
    {
        if (EvadeTarget == null)
            return false;
        return Vector3.Distance(transform.position, Destination) <= DestinationOffsetDistance;
    }

    public bool PlayerInRange()
    {
        if (EvadeTarget == null)
            return false;
        return Vector3.Distance(transform.position, EvadeTarget.position) <= DetectionRange;
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("ApatosaurusController ERROR: Apatosaurus Controller Cannot find player in Scene, Chase Behaviour will not work");
            EvadeTarget = null;

        }
        else
        {
            EvadeTarget = player.transform;
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
                ApatosaurusHealth.ReceiveDamage -= ReceiveDamage;
                CurrentHealth = 0;
                ApatosaurusHealth.Die?.Invoke(apatosaurusContext._score);
                SpawnRandomRewards();
            }
        }
    }

    private void SpawnRandomRewards()
    {
        if (RandomRewards != null)
        {
            RandomRewards.SpawnRandomItem(transform.position);
        }
    }

    public void UpdateDestination()
    {
        if (EvadeTarget != null)
        {
            Vector3 evadeDirection = (transform.position - EvadeTarget.transform.position).normalized;

            Destination = transform.position + evadeDirection * DetectionRange;
        }
        else
        {
            Destination = transform.position;
        }
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        // Target
        if (EvadeTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(EvadeTarget.position, 3f);
        }
        // View Range
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}
