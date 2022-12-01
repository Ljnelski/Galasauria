/*  Filename:           CrateController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        Crate Controller
 *  Revision History:   November 26, 2022 (Yuk Yee Wong): Initial script.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : BaseController<CrateController>
{
    // Scripts
    public HealthSystem Health { get; private set; }

    // States
    public CrateLockState LockState { get; private set; }
    public CrateQuestState QuestState { get; private set; }
    public CrateUnlockState UnlockState { get; private set; }

    // Editor Accessable
    [SerializeField] private CrateContext crateContext;
    [SerializeField] private RandomListItemCollectableData unlockingCollectable;
    [SerializeField] private RandomListItemCollectableData breakableCollectable;
    [SerializeField] private AudioSource openingSFX;
    [SerializeField] private GameObject interactSign;

    // Crate owner Input
    public Transform OwnerTarget { get; private set; }
    
    // Crate Stats
    public float MaxHealth { get => crateContext._maxHealth; }
    public float CurrentHealth { get; set; }
    public float InteractionDistance { get => crateContext._interactionDistance; }
    public bool Unlocked { get; private set; }

    private PigpenCipherPuzzleUI quest;
    private Animation crateAnimation;
    private string crateOpenAnimation = "Crate_Open";
    private Transform[] players;
    private Inventory inventoryOwner;

    void Awake()
    {
        // Assign max health to current health
        CurrentHealth = crateContext._maxHealth;

        // SetUp Animator
        crateAnimation = GetComponent<Animation>();

        // SetUp Quest
        quest = GetComponentInChildren<PigpenCipherPuzzleUI>();
        CloseQuestInterface();

        // SetUp Health
        Health = GetComponentInChildren<HealthSystem>();
        Health.ReceiveDamage += ReceiveDamage;

        // Init States
        LockState = new CrateLockState(this);
        QuestState = new CrateQuestState(this);
        UnlockState = new CrateUnlockState(this);

        FindPlayers();

        // Assign actions
        if (players != null && players.Length > 0)
        {
            foreach (Transform playerTransform in players)
            {
                PlayerController playerController = playerTransform.GetComponent<PlayerController>();
                playerController.OnCrateInteracted += PressInteractKey;
                playerController.OnCrateQuestAnswered += AnswerQuest;
            }
        }

        activeState = LockState;
        activeState.OnStateEnter();
    }

    private void FindPlayers()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        if (playerObjects == null || playerObjects.Length == 0)
        {
            Debug.LogError("CrateController ERROR: Crate Controller Cannot find player in Scene, interating Behaviour will not work");
            players = null;

        }
        else
        {
            players = new Transform[playerObjects.Length];

            for (int i = 0; i < playerObjects.Length; i++)
            {
                players[i] = playerObjects[i].transform;
            }
        }
    }

    private void AssignInventoryOwner(Inventory inventory)
    {
        //Debug.Log($"{Vector3.Distance(inventory.transform.position, transform.position)} <= {InteractionDistance}");
        if (Vector3.Distance(inventory.transform.position, transform.position) <= InteractionDistance)
        {
            inventoryOwner = inventory;
        }
        //Debug.Log($"{inventoryOwner == null}");
    }

    public bool IsInventoryOwnerNear()
    {
        return inventoryOwner != null && Vector3.Distance(inventoryOwner.transform.position, transform.position) <= InteractionDistance;
    }

    public void ClearInventoryOwner()
    {
        inventoryOwner = null;
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
                Health.ReceiveDamage -= ReceiveDamage;
                CurrentHealth = 0;
                
                if (!Unlocked)
                {
                    // Temp solution, assign to the nearest player
                    FindPlayers();
                    if (players.Length > 0)
                    {
                        float shortestDistance = Mathf.Infinity;
                        foreach (Transform playerTransform in players)
                        {
                            float distance = Vector3.Distance(transform.position, playerTransform.position);
                            if (distance < shortestDistance)
                            {
                                inventoryOwner = playerTransform.GetComponent<Inventory>();
                            }
                        }
                        breakableCollectable.Collect(inventoryOwner);
                    }
                }
            }
        }
    }

    public void OpenQuestInterface()
    {
        if (!Unlocked)
        {
            quest.gameObject.SetActive(true);
            quest.Refresh();

            if (interactSign != null)
            {
                interactSign.gameObject.SetActive(false);
            }
        }
    }

    public void CloseQuestInterface()
    {
        quest.gameObject.SetActive(false);

        if (!(Unlocked || CurrentHealth == 0) && interactSign != null)
        {
            interactSign.gameObject.SetActive(true);
        }
    }

    public void OpenCrate()
    {
        // Disable the puzzle UI
        CloseQuestInterface();

        // Play the open crate animation
        crateAnimation.CrossFade(crateOpenAnimation);

        // Play the opening sound
        openingSFX.Play();
    }

    public void PressInteractKey(Inventory inventory)
    {
        AssignInventoryOwner(inventory);
    }

    public void AnswerQuest(Inventory inventory, int answer)
    {
        AssignInventoryOwner(inventory);

        if (!Unlocked && inventory != null && inventoryOwner == inventory && IsInventoryOwnerNear())
        {
            if (answer == quest.AnswerIndex + 1)
            { 
                Unlocked = true; 
                unlockingCollectable.Collect(inventory);
            }
            else
            {
                quest.Refresh();
            }
        }
    }
}
