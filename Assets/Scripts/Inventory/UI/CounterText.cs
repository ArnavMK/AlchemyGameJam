using System;
using TMPro;
using UnityEngine;

public class CounterText : MonoBehaviour 
{
    [SerializeField] private TMP_Text counterText;

    private void Start()
    {
        Inventory.instance.OnDiscoveredResourcesChanged += OnDiscoveredResourcesChanged;
    }

    private void OnDiscoveredResourcesChanged(object sender, EventArgs e)
    {
        Debug.Log("Setting the counter");
        counterText.text = "Found: " + Inventory.instance.GetDiscoveredResourcesList().Count.ToString() + "/45";
    }
}
