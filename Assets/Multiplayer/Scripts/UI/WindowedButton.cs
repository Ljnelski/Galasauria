using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WindowedButton : MonoBehaviour
{
    private Text text;
    private Button button;
    private int smallerResolutionWidth = 960;
    private int smallerResolutionHeight = 540;
    

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
        button.onClick.AddListener(OnButtonClick);

        text.text = IsCurrentModeWindowed() ? "Full Screen" : "Windowed";
    }

    private bool IsCurrentModeWindowed()
    {
        return Screen.fullScreenMode == FullScreenMode.Windowed;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (!IsCurrentModeWindowed())
        {
            Screen.SetResolution(smallerResolutionWidth, smallerResolutionHeight, false); 
            text.text = "Full Screen";
        }
        else
        {
            Screen.SetResolution(GetFullScreenResolution().width, GetFullScreenResolution().height, true); 
            text.text = "Windowed";
        }
    }

    private Resolution GetFullScreenResolution()
    {
        return Screen.resolutions[Screen.resolutions.Length - 1];
    }
}
