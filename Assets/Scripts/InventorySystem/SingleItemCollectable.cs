/*  Filename:           SingleItemCollectable.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        October 14, 2022
 *  Description:        Finds inventory attached 
 *  Revision History:   October 14, 2022 (Liam Nelski): Initial script.
 */
using UnityEngine;

public class SingleItemCollectable : MonoBehaviour, ICollectable
{
    public ItemData item;
    
    public void Collect(Inventory inventory)
    {
        inventory.AddItem(item);
        Destroy(gameObject);
    }
}
