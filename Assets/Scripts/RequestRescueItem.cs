using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class RequestRescueItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private string labelFormat;
    [SerializeField] private GameEndScreen screen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StaticVariablesHolder.EnabledRequestRescue += 1;
            UpdateUI();

            if (StaticVariablesHolder.EnabledRequestRescue >= 5)
            {
                screen.Open(true);
            }
            else
            {
                Debug.Log(StaticVariablesHolder.EnabledRequestRescue);
                Destroy(gameObject);
            }
        }
    }

    private void UpdateUI()
    {
        label.text = Regex.Unescape(string.Format(labelFormat, StaticVariablesHolder.EnabledRequestRescue));
    }
}
