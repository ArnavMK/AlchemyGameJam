using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanetData
{
    public string planetName;
    public string resourceName;
    public int requiredUniqueResources;
    public bool isDiscovered;
}

public class PlanetDiscoverySystem : MonoBehaviour
{
    public static PlanetDiscoverySystem instance { get; private set; }

    private HashSet<string> discoveredResources = new HashSet<string>();

    [SerializeField] private float discoveryTimeInterval = 10f;    

    // Planet data with their requirements
    private List<PlanetData> planetDataList = new List<PlanetData>();
    
    // Timer for random discovery
    private float discoveryTimer;
    private float nextDiscoveryTime;
    
    public event EventHandler<string> OnPlanetDiscovered;
    public event EventHandler<string> OnResourceDiscovered;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializePlanetData();
        InitializeDiscoveredResources();
        SetNextDiscoveryTime();
        
        // Subscribe to inventory changes to track new resources
        Inventory.instance.OnInventoryChanged += OnInventoryChanged;
    }

    private void OnDestroy()
    {
        if (Inventory.instance != null)
        {
            Inventory.instance.OnInventoryChanged -= OnInventoryChanged;
        }
    }

    private void Update()
    {
        discoveryTimer += Time.deltaTime;
        
        if (discoveryTimer >= nextDiscoveryTime)
        {
            TryDiscoverPlanet();
        }
    }

    private void InitializePlanetData()
    {
        planetDataList.Clear();
        
        // Add planets in order of required unique resources
        planetDataList.Add(new PlanetData { planetName = "Moon", resourceName = "silver", requiredUniqueResources = 3, isDiscovered = false });
        planetDataList.Add(new PlanetData { planetName = "Venus", resourceName = "copper", requiredUniqueResources = 4, isDiscovered = false });
        planetDataList.Add(new PlanetData { planetName = "Mars", resourceName = "iron", requiredUniqueResources = 5, isDiscovered = false });
        planetDataList.Add(new PlanetData { planetName = "Mercury", resourceName = "tin", requiredUniqueResources = 7, isDiscovered = false });
        planetDataList.Add(new PlanetData { planetName = "Jupiter", resourceName = "tin", requiredUniqueResources = 9, isDiscovered = false });
        planetDataList.Add(new PlanetData { planetName = "Mercury", resourceName = "mercury", requiredUniqueResources = 11, isDiscovered = false });
        planetDataList.Add(new PlanetData { planetName = "Saturn", resourceName = "lead", requiredUniqueResources = 13, isDiscovered = false });
    }

    private void InitializeDiscoveredResources()
    {
        // Start with salt and sulfur as initially available
        discoveredResources.Add("salt");
        discoveredResources.Add("sulfur");
        
        Debug.Log("PlanetDiscoverySystem initialized with salt and sulfur as base discovered resources");
    }

    private void OnInventoryChanged(object sender, EventArgs e)
    {
        // Check for new unique resources in inventory
        var inventory = Inventory.instance.GetInventory();
        int uniqueResourceCount = 0;
        
        foreach (var item in inventory)
        {
            if (item.Value > 0) // Resource exists in inventory
            {
                uniqueResourceCount++;
            }
        }
        
        Debug.Log($"Total unique resources in inventory: {uniqueResourceCount}");
    }

    private void TryDiscoverPlanet()
    {
        Debug.Log("Trying to discover a planet");
        var inventory = Inventory.instance.GetInventory();
        int uniqueResourceCount = 0;
        
        // Count unique resources in inventory
        foreach (var item in inventory)
        {
            if (item.Value > 0) // Resource exists in inventory
            {
                uniqueResourceCount++;
            }
        }
        
        // Check if we can discover any planets
        foreach (var planet in planetDataList)
        {
            if (!planet.isDiscovered && uniqueResourceCount >= planet.requiredUniqueResources)
            {
                DiscoverPlanet(planet);
                return; // Only discover one planet at a time
            }
        }
        
        // Set next discovery time even if no planet was discovered
        SetNextDiscoveryTime();
    }

    private void DiscoverPlanet(PlanetData planet)
    {
        planet.isDiscovered = true;
        discoveredResources.Add(planet.resourceName);
        
        Debug.Log($"Planet discovered: {planet.planetName}! Resource '{planet.resourceName}' is now available for mining!");
        
        // Trigger events
        OnPlanetDiscovered?.Invoke(this, planet.planetName);
        OnResourceDiscovered?.Invoke(this, planet.resourceName);
        
        // Reset timer for next discovery
        discoveryTimer = 0f;
        SetNextDiscoveryTime();
    }

    private void SetNextDiscoveryTime()
    {
        nextDiscoveryTime = discoveryTimeInterval; 
        Debug.Log($"Next planet discovery attempt in {nextDiscoveryTime:F1} seconds");
    }

    // Public methods for other systems to query
    public bool IsResourceDiscovered(string resourceName)
    {
        return discoveredResources.Contains(resourceName.ToLower());
    }

    public HashSet<string> GetDiscoveredResources()
    {
        return new HashSet<string>(discoveredResources);
    }

    public List<PlanetData> GetPlanetData()
    {
        return new List<PlanetData>(planetDataList);
    }

    public int GetUniqueResourceCount()
    {
        var inventory = Inventory.instance.GetInventory();
        int count = 0;
        
        foreach (var item in inventory)
        {
            if (item.Value > 0)
            {
                count++;
            }
        }
        
        return count;
    }

    // Method to manually trigger planet discovery (for testing)
    [ContextMenu("Force Planet Discovery")]
    public void ForcePlanetDiscovery()
    {
        TryDiscoverPlanet();
    }

    // Method to add a resource to discovered list (for external systems)
    public void AddDiscoveredResource(string resourceName)
    {
        if (!discoveredResources.Contains(resourceName.ToLower()))
        {
            discoveredResources.Add(resourceName.ToLower());
            OnResourceDiscovered?.Invoke(this, resourceName);
            Debug.Log($"Resource '{resourceName}' added to discovered resources list");
        }
    }
}
