/*  Filename:           TyrannosaurusController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        Tyrannosaurus Controller
 *  Revision History:   November 26, 2022 (Yuk Yee Wong): Initial script.
 */

using System;
using UnityEngine;
using UnityEngine.AI;

public class TyrannosaurusController : BaseController<TyrannosaurusController>
{
    // Scripts
    public NavMeshAgent Agent { get; private set; }
    public TimerPool Timers { get; private set; }

    // States
    public TyrannosaurusChargeState ChargeState { get; private set; }
    public TyrannosaurusIdleState IdleState { get; private set; }
    public TyrannosaurusAttackState AttackState { get; private set; }
    public TyrannosaurusDieState DieState { get; private set; }

    // Editor Accessable
    [SerializeField]
    private TyrannosaurusContext tyrannosaurusContext;
    private HealthSystem [] tyrannosaurusHealthSystems;
    private Destroyer[] destroyers;

    // Tyrannosaurus Input
    public Transform ChargeTarget { get; private set; }

    // Tyrannosaurus Stats

    public float MaxHealth { get => tyrannosaurusContext._maxHealth; }

    // Directly using to Tyrannosaurus context means that all Tyrannosaurus shares the same scriptable object data
    // 1. If one Tyrannosaurus has 0 health, all Tyrannosaurus have 0 health
    // 2. The Tyrannosaurus context scriptable object retains the last value even when we close the game
    // 3. We have to reset the current health to max health every time an enemy is borned
    // Because of these, current health keeps as a separate int and max health is assigned instead
    // But please feel free to change if you have other idea
    public float CurrentHealth { get; set; }
    public float BaseSpeed { get => tyrannosaurusContext._baseSpeed; }
    public float TurnSpeed { get => tyrannosaurusContext._turnSpeed; }
    public float Acceleration { get => tyrannosaurusContext._acceleration; }
    public float PlayerOffsetDistance { get => tyrannosaurusContext._playerOffsetDistance; }

    public float ChargeInterval { get => tyrannosaurusContext._chargeMiliseconds; }
    public float AttackInterval { get => tyrannosaurusContext._attackCoolDownMiliseconds; }
    public float DieInterval { get => tyrannosaurusContext._dieMiliseconds; }
    public float BaseDamage { get => tyrannosaurusContext._baseDamage; }
    public float DetectionRange { get => tyrannosaurusContext._detectionRange; }

    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        // Assign max health to current health
        CurrentHealth = tyrannosaurusContext._maxHealth;

        // SetUp Timers
        Timers = GetComponent<TimerPool>();

        // SetUp Animator
        animator = GetComponentInChildren<Animator>();

        // SetUp Agent
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = BaseSpeed;
        Agent.angularSpeed = TurnSpeed;
        Agent.acceleration = Acceleration;

        // SetUp Health
        tyrannosaurusHealthSystems = GetComponentsInChildren<HealthSystem>();
        foreach (HealthSystem health in tyrannosaurusHealthSystems)
        {
            health.ReceiveDamage += ReceiveDamage;
        }

        // SetUp Destroyer
        destroyers = GetComponentsInChildren<Destroyer>();
        foreach (Destroyer destroyer in destroyers)
        {
            destroyer.Damage = BaseDamage;
        }

        // Init States
        ChargeState = new TyrannosaurusChargeState(this);
        IdleState = new TyrannosaurusIdleState(this);
        AttackState = new TyrannosaurusAttackState(this);
        DieState = new TyrannosaurusDieState(this);

        FindPlayer();

        activeState = IdleState;
        activeState.OnStateEnter();
    }

    public void FaceTarget()
    {
        Vector3 direction = (ChargeTarget.position - transform.position).normalized;
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

    public void Charge()
    {
        if (animator)
        {
            animator.SetFloat("velocity", 2f);
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
            animator.SetTrigger("Die");
        }
    }

    public bool ReachedDestination()
    {
        return Vector3.Distance(Agent.destination, transform.position) <= PlayerOffsetDistance;
    }

    public bool ReachedPlayer()
    {
        if (ChargeTarget == null)
            return false;
        return Vector3.Distance(transform.position, ChargeTarget.position) <= PlayerOffsetDistance;
    }

    public bool PlayerInRange()
    {
        if (ChargeTarget == null)
            return false;
        return Vector3.Distance(transform.position, ChargeTarget.position) <= DetectionRange;
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("TyrannosaurusController ERROR: Tyrannosaurus Controller Cannot find player in Scene, Charge Behaviour will not work");
            ChargeTarget = null;

        }
        else
        {
            ChargeTarget = player.transform;
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
                foreach (HealthSystem health in tyrannosaurusHealthSystems)
                {
                    health.ReceiveDamage -= ReceiveDamage;
                }

                CurrentHealth = 0;

                foreach (HealthSystem health in tyrannosaurusHealthSystems)
                {
                    health.Die?.Invoke(tyrannosaurusContext._score);
                }
            }
        }
    }

    public void SelfDestroy() 
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        // Target
        if (ChargeTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(ChargeTarget.position, 3f);
        }

        // View Range
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}
