/*  Filename:           GameEnums.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        October 12, 2022
 *  Description:        A collection of enums used in the game, data, and scriptable objects
 *  Revision History:   October 12, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnums
{
    public enum ItemType
    {
        NONE = 0,
        PIPE = 1,
        PLASMACELL = 2,
        PLASMACAPACITOR = 3,
        PLASMACARTRIDGE = 4,
        WIRE = 5,
        MICROCHIP = 6,

        // upgradable item (i.e. weapons, power suit)
        CYBERBLADE = 1001,
        PLASMACASTER = 1002,
        POWERSUIT = 1003,
    }

    public enum Screen
    {
        NONE = 0,
        START = 1,
        INSTRUCTIONS = 2,
        OPTIONS = 3,
        GAMEPLAY = 4,
        UPGRADE = 5,
        INVENTORY = 6,
        GAMEEND = 7,
    }

    public enum GameDifficulty
    {
        EASY = 0,
        MEDIUM = 1,
        HARD = 2,
    }

    public enum SoundType
    {
        NONE= 0,
        MUSIC = 1,
        SFX = 2,
        AMBIENCE = 3,
        VOICE = 4,
    }

    public enum GeneralAudio
    {
        NONE = 0,
        BUTTON = 1,
        CHANGESCENE = 2,
        TOGGLE = 3,
        CHECKBOX = 4,
    }
}
