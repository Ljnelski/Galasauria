/*  Filename:           NetworkSpiderController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.AI;
using Unity.Netcode;

public class NetworkSpiderController : BaseNetworkController<NetworkSpiderController>
{
    public enum eNetworkSpiderState
    {
        NONE = 0,
        IDLE = 1,
        CHASE = 2,
        ATTACK = 3,
    }

    // Scripts
    public NavMeshAgent Agent { get; private set; }
    public HealthSystem SpiderHealth { get; private set; }
    public ActionTimerPool Timers { get; private set; }

    // States
    public NetworkSpiderChaseState chaseState { get; private set; }
    public NetworkSpiderIdleState idleState { get; private set; }
    public NetworkSpiderAttackState attackState { get; private set; }

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
    public float BaseSpeed { get => spiderContext._baseSpeed; }
    public float TurnSpeed { get => spiderContext._turnSpeed; }
    public float Acceleration { get => spiderContext._acceleration; }
    public float PlayerOffsetDistance { get => spiderContext._playerOffsetDistance; }
    public float AttackInterval { get => spiderContext._attackCoolDownMiliseconds; }
    public float AttackDuration { get => spiderContext._attackDuration; }
    public float BaseDamage { get => spiderContext._baseDamage; }
    public float DetectionRange { get => spiderContext._detectionRange; }

    public RandomListItemCollectableData RandomRewards { get => spiderContext._randomCollectable; }

    private Animator animator;
    private Destroyer destroyer;
    private AudioSource gruntAudio;

    // Network
    private NetworkVariable<float> networkSpiderHealthPoint = new NetworkVariable<float>();
    private NetworkVariable<eNetworkSpiderState> networkSpiderActiveState = new NetworkVariable<eNetworkSpiderState>();
    private eNetworkSpiderState networkSpiderState;

    private float findPlayerInterval = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        // SetUp Timers
        Timers = GetComponent<ActionTimerPool>();

        // SetUp Animator
        animator = GetComponent<Animator>();

        // SetUp Audio
        gruntAudio = GetComponent<AudioSource>();

        // SetUp Agent
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = BaseSpeed;
        Agent.angularSpeed = TurnSpeed;
        Agent.acceleration = Acceleration;

        // SetUp Health
        SpiderHealth = GetComponent<HealthSystem>();
        SpiderHealth.ReceiveDamage += ReceiveDamage;

        // SetUp Destroyer
        destroyer = GetComponentInChildren<Destroyer>();
        if (destroyer != null)
        {
            destroyer.Damage = BaseDamage;
        }

        EnableDestroyer(false);

        // avoid self-destruction from local because of 0 health
        // this will produce warning
        // networkSpiderHealthPoint.Value = spiderContext._maxHealth;
    }

    private void CreateTimerToUpdateChaseTarget()
    {
        if (this != null)
        {
            Timers.CreateTimer(findPlayerInterval, () =>
            {
                if (this != null)
                {
                    UpdateChaseTarget();
                    CreateTimerToUpdateChaseTarget();
                }
            });
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

    private void Update()
    {
        if (IsServer)
        {
            if (!networkSpiderState.Equals(networkSpiderActiveState.Value))
            {
                networkSpiderState = networkSpiderActiveState.Value;
                SwitchState();
            }
        }
    }

    private void SwitchState()
    {
        switch (networkSpiderActiveState.Value)
        {
            case eNetworkSpiderState.NONE:
                break;
            case eNetworkSpiderState.IDLE:
                ChangeState(idleState);
                break;
            case eNetworkSpiderState.CHASE:
                ChangeState(chaseState);
                break;
            case eNetworkSpiderState.ATTACK:
                ChangeState(attackState);
                break;
            default:
                Debug.LogWarning($"NetworkPlayerController ERROR. eNetworkPlayerState {networkSpiderActiveState.Value} did not handled in ChangeState. Please update the method.");
                break;
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkDespawn();

        if (IsServer)
        {
            // Assign max health to current health
            UpdateSpiderHeathPointServerRpc(spiderContext._maxHealth);

            // Init States
            chaseState = new NetworkSpiderChaseState(this);
            idleState = new NetworkSpiderIdleState(this);
            attackState = new NetworkSpiderAttackState(this);

            // Update intial player state
            UpdateSpiderActiveStateServerRpc(eNetworkSpiderState.IDLE);

            CreateTimerToUpdateChaseTarget();
        }
    }


    #region server rpc
    [ServerRpc]
    public void UpdateSpiderHeathPointServerRpc(float healthPoint)
    {
        if (IsServer)
        {
            this.networkSpiderHealthPoint.Value = healthPoint;
        }
    }

    [ServerRpc]
    public void UpdateSpiderActiveStateServerRpc(eNetworkSpiderState spiderState)
    {
        if (IsServer)
        {
            networkSpiderActiveState.Value = spiderState;
        }
    }
    #endregion server rpc

    private void UpdateChaseTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players == null || players.Length == 0)
        {
            Debug.LogWarning("SpiderController ERROR: Spider Controller Cannot find player in Scene, Chase Behaviour will not work");
            ChaseTarget = null;

        }
        else
        {
            float closestDistance = Mathf.Infinity;
            foreach(GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    ChaseTarget = player.transform;
                }
            }
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
        if (ChaseTarget == null)
            return false;
        return Vector3.Distance(transform.position, ChaseTarget.position) <= DetectionRange;
    }

    #region server determined methods
    public void FaceTarget()
    {
        if (IsServer)
        {
            Vector3 direction = (ChaseTarget.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * TurnSpeed);
        }
    }

    public void EnableDestroyer(bool enable)
    {
        if (IsServer)
        {
            destroyer.gameObject.SetActive(enable);
        }
    }

    public void Attack()
    {
        if (IsServer)
        {
            if (animator)
            {
                animator.SetTrigger("attack");
                gruntAudio.Play();
            }
        }
    }

    public void Walk()
    {
        if (IsServer)
        {
            if (animator)
            {
                animator.SetFloat("velocity", 1f);
            }
        }
    }

    public void Idle()
    {
        if (IsServer)
        {
            if (animator)
            {
                animator.SetFloat("velocity", 0f);
            }
        }
    }

    private void ReceiveDamage(float damage)
    {
        float newHealthPoint = networkSpiderHealthPoint.Value;

        if (networkSpiderHealthPoint.Value > 0)
        {
            if (networkSpiderHealthPoint.Value - damage > 0)
            {
                newHealthPoint = networkSpiderHealthPoint.Value - damage;
            }
            else
            {
                SpiderHealth.ReceiveDamage -= ReceiveDamage;
                SpiderHealth.Die?.Invoke(spiderContext._score);
                SpawnRandomRewards();
                newHealthPoint = 0;
                transform.GetComponent<NetworkObject>().Despawn();
            }
        }

        UpdateSpiderHeathPointServerRpc(newHealthPoint);
    }

    private void SpawnRandomRewards()
    {
        if (RandomRewards != null)
        {
            RandomRewards.SpawnRandomItem(transform.position);
        }
    }
    #endregion

}