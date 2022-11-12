/*  Filename:           PigenCipherPuzzle.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 12, 2022
 *  Description:        Pigen cipher puzzle for unlocking crate and obtaining random items
 *  Revision History:   November 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PigenCipherPuzzle : MonoBehaviour
{
    [Header("Pigen Cipher")]
    [SerializeField] private PigenCipherPuzzleUI puzzleInterface;
    [SerializeField] private Animation crateAnimation;
    [SerializeField] private List<KeyCode> answerKeyCodes;
    [SerializeField] private RandomListItemCollectableData unlockingCollectable;
    [SerializeField] private RandomListItemCollectableData breakableCollectable;
    [SerializeField] private AudioSource openingSFX;

    [Header("Settings")]
    private string crateOpenAnimation = "Crate_Open";

    [Header("Debug")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private bool playerEntered;
    [SerializeField] private bool solved;


    void Start()
    {
        if (puzzleInterface.AnswerLabelsCount != answerKeyCodes.Count)
        {
            Debug.LogWarning($"The number of answer label is {puzzleInterface.AnswerLabelsCount}, which does not match with that for key codes' ({answerKeyCodes})");
        }

        puzzleInterface.Refresh();
        puzzleInterface.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!solved && playerEntered)
        {
            for (int i = 0; i < answerKeyCodes.Count; i++)
            {
                // Debug.Log($"{answerKeyCodes[i]} vs {puzzleInterface.AnswerIndex} vs {i}");
                if (Input.GetKey(answerKeyCodes[i]))
                {
                    if (puzzleInterface.AnswerIndex == i && inventory != null)
                    {
                        // Play the open crate animation
                        crateAnimation.CrossFade(crateOpenAnimation);

                        // Play the opening sound
                        openingSFX.Play();

                        // Disable the puzzle UI
                        puzzleInterface.gameObject.SetActive(false);

                        // Collect the items
                        unlockingCollectable.Collect(inventory);

                        solved = true;
                    }
                    else
                    {
                        // refresh the pair
                        puzzleInterface.Refresh();
                    }
                }
            }
        }
    }

    // TODO, wire the IDamagable
    public void CollectUponBroken()
    {
        // Play the open crate animation
        crateAnimation.CrossFade(crateOpenAnimation);

        // Disable the puzzle UI
        puzzleInterface.gameObject.SetActive(false);

        // TODO, we have to consider other method to collect the items as the player may use ranged weapons
        // and the crate do not have the inventory
        // we may simply spawn the another list item trigger and leave it on the spot
        if (inventory)
        { 
            // Collect the items
            breakableCollectable.Collect(inventory);
        }

        solved = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!solved && other.CompareTag("Player"))
        {
            puzzleInterface.gameObject.SetActive(true);
            playerEntered = true;
            inventory = other.GetComponent<Inventory>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!solved && other.CompareTag("Player"))
        {
            puzzleInterface.gameObject.SetActive(false);
            playerEntered = false;
            inventory = null;
        }
    }
}
