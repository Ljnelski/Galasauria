/*  Filename:           SoundManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 26, 2022
 *  Description:        Contains functions to mute and unmute sfx and music; stores audio clips for different screens and scenes to play sfx and music
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 *                      November 8, 2022 (Yuk Yee Wong): Added score audio.
 *                      November 26, 2022 (Yuk Yee Wong): Fixed a null reference when the game stops.
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioMixer audioMixer;

    [Header("SFX")]
    [SerializeField] private AudioSource buttonAudio;
    [SerializeField] private AudioSource changeSceneAudio;
    [SerializeField] private AudioSource toggleAudio;
    [SerializeField] private AudioSource checkboxAudio;
    [SerializeField] private AudioSource scoreAudio;
    [SerializeField] private AudioSource doorOpenAudio;

    [Header("Music")]
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private AudioClip gameEndClip;
    [SerializeField] private AudioClip gameplayClip;
    [SerializeField] private AudioClip instructionsClip;
    [SerializeField] private AudioClip inventoryClip;
    [SerializeField] private AudioClip optionsClip;
    [SerializeField] private AudioClip startClip;
    [SerializeField] private AudioClip upgradeClip;

    [Header("Debug")]
    [SerializeField] private bool musicUnmuted = true;
    [SerializeField] private bool sfxUnmuted = true;
    public bool MusicUnmuted { get { return musicUnmuted; } }
    public bool SfxUnmuted { get { return sfxUnmuted; } }

    private const string MusicParameter = "MusicVol";
    private const string SfxParameter = "SfxVol";
    private const float unmutedVolume = 0f;
    private const float mutedVolume = -100f;

    private Stack<GameEnums.Screen> screens = new Stack<GameEnums.Screen>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SetUnmuted(GameEnums.SoundType type, bool unmuted)
    {
        switch (type)
        {
            case GameEnums.SoundType.MUSIC:
                musicUnmuted = unmuted;
                audioMixer.SetFloat(MusicParameter, unmuted? unmutedVolume : mutedVolume);
                break;
            case GameEnums.SoundType.SFX:
                sfxUnmuted = unmuted;
                audioMixer.SetFloat(SfxParameter, unmuted ? unmutedVolume : mutedVolume);
                break;
            default:
                Debug.LogError("Please assign the sound type before setting volume");
                break;
        }
    }

    public void PlayGeneralAudio(GameEnums.GeneralAudio audio)
    {
        switch (audio)
        {
            case GameEnums.GeneralAudio.BUTTON:
                buttonAudio.Play();
                break;
            case GameEnums.GeneralAudio.CHANGESCENE:
                changeSceneAudio.Play();
                break;
            case GameEnums.GeneralAudio.TOGGLE:
                toggleAudio.Play();
                break;
            case GameEnums.GeneralAudio.CHECKBOX:
                checkboxAudio.Play();
                break;
            case GameEnums.GeneralAudio.SCORE:
                scoreAudio.Play();
                break;
            case GameEnums.GeneralAudio.DOOROPEN:
                doorOpenAudio.Play();
                break;
            default:
                Debug.LogError("Please assign the general audio type before playing audio");
                break;
        }
    }

    public void OpenScreen(GameEnums.Screen screen)
    {
        screens.Push(screen);
        UpdateMusic();
    }

    public void CloseScreen()
    {
        screens.Pop();
        UpdateMusic();
    }

    public void ChangeScene(GameEnums.Screen screen)
    {
        screens.Clear();
        screens.Push(screen);
        UpdateMusic();
    }

    private void UpdateMusic()
    {
        if (musicAudio != null)
        {
            switch (screens.Peek())
            {
                case GameEnums.Screen.GAMEEND:
                    musicAudio.clip = gameEndClip;
                    goto playMusic;
                case GameEnums.Screen.GAMEPLAY:
                    musicAudio.clip = gameplayClip;
                    goto playMusic;
                case GameEnums.Screen.INSTRUCTIONS:
                    musicAudio.clip = instructionsClip;
                    goto playMusic;
                case GameEnums.Screen.INVENTORY:
                    musicAudio.clip = inventoryClip;
                    goto playMusic;
                case GameEnums.Screen.OPTIONS:
                    musicAudio.clip = optionsClip;
                    goto playMusic;
                case GameEnums.Screen.START:
                    musicAudio.clip = startClip;
                    goto playMusic;
                case GameEnums.Screen.UPGRADE:
                    musicAudio.clip = upgradeClip;
                    goto playMusic;
                playMusic:
                    if (musicAudio.clip != null)
                    {
                        musicAudio.Play();
                    }
                    break;
                default:
                    Debug.LogError("Please assign the screen before update music");
                    break;
            }
        }
    }
}
