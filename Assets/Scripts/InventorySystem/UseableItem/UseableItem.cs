using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UseableItem : MonoBehaviour
{
    protected PlayerController _targetPlayerController;
    protected ItemData _itemData;

    public void InitEffect(PlayerController targetPlayerController, ItemData itemData)
    {
        _targetPlayerController = targetPlayerController;
        _itemData = itemData;
        Debug.Log("Initing Effect");
    }

    public virtual UnityAction GetItemEffect()
    {
        if (_targetPlayerController == null || _itemData == null)
        {
            Debug.LogError("Useable Item ERROR: Useable Item Was not Intalized, InitEffect Must be called to provide reference to a PlayerController and ItemData");
            return null;
        }
        return new UnityAction(ItemEffect);
    }

    public abstract void ItemEffect();
}
