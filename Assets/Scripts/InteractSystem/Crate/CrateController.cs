/*  Filename:           CrateController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        Crate Controller
 *  Revision History:   November 26, 2022 (Yuk Yee Wong): Initial script.
 *                      June 29, 2023 (Liam Nelski): Added Interactable Interface to change how player interaction works
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour, IInteractable
{
    // Scripts
    public HealthSystem Health { get; private set; }

    // Editor Accessable
    [SerializeField] private CrateContext crateContext;
    [SerializeField] private RandomListItemCollectableData unlockingCollectable;
    [SerializeField] private RandomListItemCollectableData breakableCollectable;
    [SerializeField] private AudioSource openingSFX;
    [SerializeField] private AudioSource breakingSFX;
    [SerializeField] private GameObject interactSign;

    // Crate Stats
    public float MaxHealth { get => crateContext._maxHealth; }
    public float CurrentHealth { get; set; }
    public float InteractionDistance { get => crateContext._interactionDistance; }
    public bool Unlocked { get; private set; }
    public bool IsInteractable { get => Unlocked; }

    public Vector3 WorldPosition { get => transform.position; }

    private PigpenCipherPuzzleUI quest;
    private Animation crateAnimation;
    private string crateOpenAnimation = "Crate_Open";
    private string crateBreakAnimation = "Crate_Break";

    void Awake()
    {
        // Assign max health to current health
        CurrentHealth = crateContext._maxHealth;

        // Setup Interaction
        GetComponent<CapsuleCollider>().radius = crateContext._interactionDistance;
        GetComponent<CapsuleCollider>().height = crateContext._interactionDistance * 3f;

        // SetUp Animator
        crateAnimation = GetComponent<Animation>();

        // SetUp InteractSign
        HideInteractIndicator();

        // SetUp Quest
        quest = GetComponentInChildren<PigpenCipherPuzzleUI>();
        CloseQuestInterface();

        // SetUp Health
        Health = GetComponentInChildren<HealthSystem>();
        Health.ReceiveDamage += ReceiveDamage;
    }

   

    public void OpenQuestInterface()
    {
        if (!Unlocked)
        {
            quest.gameObject.SetActive(true);
            quest.Refresh();

            if (interactSign != null)
            {
                HideInteractIndicator();
            }
        }
    }

    public void CloseQuestInterface()
    {
        quest.gameObject.SetActive(false);
    }

    public void ShowInteractIndicator()
    {
        interactSign.gameObject.SetActive(true);
    }

    public void HideInteractIndicator()
    {
        interactSign.gameObject.SetActive(false);
    }

    private void OpenCrate()
    {
        // Disable the puzzle UI
        CloseQuestInterface();

        // Play the open crate animation
        crateAnimation.CrossFade(crateOpenAnimation);

        // Play the opening sound
        openingSFX.Play();
    }

    private void BreakCrate()
    {
        // Disable the puzzle UI
        CloseQuestInterface();

        // Play the open crate animation
        crateAnimation.CrossFade(crateBreakAnimation);

        // Play the opening sound
        breakingSFX.Play();
    }

    public void AnswerQuest(Inventory inventory, int answer)
    {
        if (!Unlocked && inventory != null)
        {
            if (answer == quest.AnswerIndex + 1)
            { 
                Unlocked = true; 
                unlockingCollectable.Collect(inventory);
                OpenCrate();
            }
            else
            {
                quest.Refresh(); 
            }
        }
    }

    private void ReceiveDamage(float damage)
    {
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
                Unlocked = true;
                                
                BreakCrate();
                HideInteractIndicator();
            }
        }
    }

    public void Interact(InteractValue value, Inventory inventory)
    {
        switch (value)
        {
            case InteractValue.Base:
                OpenQuestInterface();
                break;
            case InteractValue.One:
            case InteractValue.Two:
            case InteractValue.Three:
            case InteractValue.Four:
                AnswerQuest(inventory, (int)value);
                break;
            default:
                break;
        }        
    }

    public void Target()
    {        
        ShowInteractIndicator();
    }

    public void Detarget()
    {
        CloseQuestInterface();
        HideInteractIndicator();
    }
}
