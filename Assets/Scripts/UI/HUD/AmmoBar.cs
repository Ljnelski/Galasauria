/*  Filename:           AmmoBar.cs
 *  Author:             Liam Nelski (301064116)
 *  Last Update:        November 13, 2022
 *  Description:        Translates the ammo count to the UI
 *  Revision History:   November 13, 2022 (Liam Nelski): Initial script.
 */
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : UIPlayerDataReader<Inventory>
{
    [SerializeField] private ItemData _plasmaCartridge;
    private const int TEMP_MAX_AMMO = 300;
    private List<GameObject> _ammoBarFillers;
    private Text _ammoCount;
    private void Start()
    {
        GetTargetScript();
        GetAmmoBarFillers();
        _ammoCount = transform.GetChild(0).GetComponent<Text>();

        _targetScript.OnInventoryUpdate += DrawAmmoBar;
        DrawAmmoBar();
    }

    private void DrawAmmoBar()
    {
        float numberToFill = 0;
        int ammoCount = 0;
        bool found = _targetScript.itemDictionary.TryGetValue(_plasmaCartridge, out Item plasmaCartridge);

        if(found && plasmaCartridge.stackSize >= 0)
        {
            // Convert to float to find percentage
            float stackSizeFloat = plasmaCartridge.stackSize;
            float maxFloat = TEMP_MAX_AMMO;

            // Calculate Percent
            float percentFilled = stackSizeFloat / maxFloat;
            
            // Generate Steps, One more than numbe of bars so last one can be filled in before 100% capacity
            List<float> steps = Enumerable.Range(0, _ammoBarFillers.Count + 1).Select(x => x / (float)_ammoBarFillers.Count).ToList();

            // Iterate thorugh steps and fill number based on being less than step
            for (int i = 0; i < steps.Count; i++)
            {
                if (percentFilled <= steps[i])
                {
                    numberToFill = i;
                    break;
                }
            }

            ammoCount = plasmaCartridge.stackSize;
        } 

        // Enable and Disable Bars based on the number required to fill
        for (int i = 0; i < _ammoBarFillers.Count; i++)
        {
            bool doFill = (i + 1) <= numberToFill;
            _ammoBarFillers[i].SetActive(doFill);
        }

        // Set the text
        _ammoCount.text = ammoCount.ToString();
    }

    private void GetAmmoBarFillers()
    {
        _ammoBarFillers = new List<GameObject>();
        
        RectTransform ammoContainer = transform.GetChild(1).GetComponent<RectTransform>();

        for (int i = 0; i < ammoContainer.childCount; i++)
        {
            _ammoBarFillers.Add(ammoContainer.GetChild(i).GetChild(0).gameObject);
        }
    }



}
