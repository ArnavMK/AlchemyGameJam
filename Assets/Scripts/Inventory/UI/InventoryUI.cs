using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] private GameObject resourceTileParentPanel;
    [SerializeField] private ResourceTile resourceTilePrefab; 

    private void Start()
    {
        Inventory.instance.OnInventoryChanged += Inventory_OnInventoryChanged;
    }

    private void Inventory_OnInventoryChanged(object sender, System.EventArgs e)
    {
        UpdateUi();
    }

    private void UpdateUi()
    {
        // destroy all existing resource tiles
        foreach (Transform child in resourceTileParentPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // create a new resource tile for each item in the inventory
        Dictionary<string, int> inventory = Inventory.instance.GetInventory();
        foreach (var item in inventory)
        {
            ResourceTile resourceTile = Instantiate(resourceTilePrefab, resourceTileParentPanel.transform);
            resourceTile.Initialize(item.Key, item.Value);
        }
    }
}
