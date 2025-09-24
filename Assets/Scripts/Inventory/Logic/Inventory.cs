using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance { get; private set; }

    private Dictionary<string, int> inventory = new();
    private Dictionary<string, string> discoverdResources = new();

    public event EventHandler OnInventoryChanged;
    public event EventHandler OnNewResourceAdded;
    public event EventHandler<string> OnItemUsed;
    public event EventHandler OnDiscoveredResourcesChanged;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ResourceTile.OnResourceTileClicked += ResourceTile_OnResourceTileClicked;
        Cauldron.instance.OnCook += Cauldron_OnCook;
    }


    private void Cauldron_OnCook(object sender, KeyValuePair<Resource, int> e)
    {
        AddItem(e.Key.GetName(), e.Value);
    }

    private void ResourceTile_OnResourceTileClicked(object sender, Resource r)
    {
        if (Cauldron.instance.IsFull())
        {
            return;
        }
        UseItem(r.GetName());
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnInventoryChanged?.Invoke(this, EventArgs.Empty);
            OnNewResourceAdded?.Invoke(this, EventArgs.Empty);
            OnDiscoveredResourcesChanged?.Invoke(this, EventArgs.Empty);
            Debug.Log("New resources added init");
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
            OnNewResourceAdded?.Invoke(this, EventArgs.Empty);
        }
        AddItemtoDiscoveredList(itemName);
        OnInventoryChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddItemtoDiscoveredList(string itemName)
    {
        if (!discoverdResources.ContainsKey(itemName))
        {
            discoverdResources.Add(itemName, itemName);
            OnDiscoveredResourcesChanged?.Invoke(this, EventArgs.Empty);
        }
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

    public Dictionary<string, string> GetDiscoveredResourcesList()
    {
        return discoverdResources;
    }

    public bool HasItem(string itemName)
    {
        return inventory.ContainsKey(itemName);
    }
}