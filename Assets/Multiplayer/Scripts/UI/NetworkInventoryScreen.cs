using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class NetworkInventoryScreen : UINetworkPlayerMonoBehaviourDataReaderScreen<Inventory>
{
    [SerializeField] private Transform inventoryContainer;
    [SerializeField] private InventoryItemDisplay inventoryItemPrefab;
    [SerializeField] private NetworkInventoryItemDetailsDisplay inventoryItemDetailsDisplay;

    protected override void OnEnable()
    {
        base.OnEnable();

        inventoryItemDetailsDisplay.RefreshInventoryScreen += Refresh;

        Refresh();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        inventoryItemDetailsDisplay.RefreshInventoryScreen -= Refresh;
    }

    public override void SetUp(Inventory inventory)
    {
        base.SetUp(inventory);

        if (_targetScript == null)
        {
            Debug.LogError("PlayerHealthBar ERROR: HUDTargetPlayer Script does not have a valid Target");
            return;
        }
    }

    private void Refresh()
    {
        // clear the grid
        if (inventoryContainer.childCount > 0)
        {
            for (int i = inventoryContainer.childCount - 1; i > -1; i--)
            {
                // TODO, recycle
                Destroy(inventoryContainer.GetChild(i).gameObject);
            }
        }

        if (_targetScript != null)
        {
            // add item to the grid
            foreach (Item item in _targetScript.inventory)
            {
                InventoryItemDisplay display = Instantiate(inventoryItemPrefab, inventoryContainer);
                ItemData itemData = item.data;
                if (itemData != null)
                {
                    display.Init(itemData, item.stackSize);
                    display.OnItemClick = null;
                    display.OnItemClick += inventoryItemDetailsDisplay.Display;
                }
            }

            inventoryItemDetailsDisplay.ResetDisplay();
        }
    }
}

