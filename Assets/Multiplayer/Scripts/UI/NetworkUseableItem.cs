/*  Filename:           NetworkUseableItem.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 13, 2022
 *  Description:        
 *  Revision History:   December 13, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.Events;

public abstract class NetworkUseableItem : MonoBehaviour
{
    protected NetworkPlayerController _targetNetworkPlayerController;
    protected ItemData _itemData;

    public void InitEffect(NetworkPlayerController targetNetworkPlayerController, ItemData itemData)
    {
        _targetNetworkPlayerController = targetNetworkPlayerController;
        _itemData = itemData;
        Debug.Log("Initing Effect");
    }

    public virtual UnityAction GetItemEffect()
    {
        if (_targetNetworkPlayerController == null || _itemData == null)
        {
            Debug.LogError("Network Useable Item ERROR: Useable Item Was not Intalized, InitEffect Must be called to provide reference to a PlayerController and ItemData");
            return null;
        }
        return new UnityAction(ItemEffect);
    }

    public abstract void ItemEffect();
}