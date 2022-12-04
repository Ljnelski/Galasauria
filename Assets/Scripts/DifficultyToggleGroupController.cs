using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyToggleGroupController : MonoBehaviour
{
    public Toggle toggleEasy;
    public Toggle toggleMedium;
    public Toggle toggleHard;
    // Start is called before the first frame update
    void Start()
    {
        toggleEasy.interactable = true;
        toggleMedium.interactable = WinStatTracker.isLv1Complete;
        toggleHard.interactable = WinStatTracker.isLv2Complete;
    }
}
