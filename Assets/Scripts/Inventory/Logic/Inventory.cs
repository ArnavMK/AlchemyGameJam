using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance { get; private set; }

    private Dictionary<string, int> inventory = new();

    public event EventHandler OnInventoryChanged;
    public event EventHandler<string> OnItemUsed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ResourceTile.OnResourceTileClicked += ResourceTile_OnResourceTileClicked;
        // Add some test items to the inventory
        inventory["salt"] = 5;
        inventory["sulfur"] = 3;
        inventory["copper"] = 8;
        inventory["iron"] = 12;
        inventory["tin"] = 4;
        inventory["silver"] = 2;
        inventory["crystal"] = 6;
        inventory["bronze"] = 3;
        inventory["mercury"] = 1;
        inventory["lead"] = 2;
        inventory["titanium"] = 1;
        inventory["aluminium"] = 1;
        inventory["cobalt"] = 4;
        inventory["zinc"] = 3;
        inventory["palladium"] = 2;
        inventory["ruby"] = 1;
        inventory["red aether"] = 1;

        // Add some crafted items with 0 recipes to test those too
        inventory["rust"] = 2;
        inventory["tarnish"] = 1;
        inventory["brass"] = 3;
        inventory["quartz"] = 2;

        Debug.Log("Inventory initialized with " + inventory.Count + " items");
    }

    private void ResourceTile_OnResourceTileClicked(object sender, Resource r)
    {
        UseItem(r.GetName());
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnInventoryChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void AddItem(string itemName, int quantity)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] += quantity;
        }
        else
        {
            inventory[itemName] = quantity;
        }
        OnInventoryChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(string itemName)
    {
        if (inventory.ContainsKey(itemName) && inventory[itemName] >= 1)
        {
            inventory[itemName]--;

            if (inventory[itemName] <= 0)
            {
                inventory.Remove(itemName);
            }
            OnInventoryChanged?.Invoke(this, EventArgs.Empty);
            OnItemUsed?.Invoke(this, itemName);
        }
    }

    public Dictionary<string, int> GetInventory()
    {
        return inventory;
    }
}