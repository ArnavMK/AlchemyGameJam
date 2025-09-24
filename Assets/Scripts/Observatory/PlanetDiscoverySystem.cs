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


    // Planet data with their requirements
    private List<PlanetData> planetDataList = new List<PlanetData>();
    
    // Timer for random discovery
    
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
        
        // Subscribe to discovered resources changes to trigger planet discovery
        Inventory.instance.OnDiscoveredResourcesChanged += OnDiscoveredResourcesChanged;
    }

    private void OnDestroy()
    {
        if (Inventory.instance != null)
        {
            Inventory.instance.OnDiscoveredResourcesChanged -= OnDiscoveredResourcesChanged;
        }
    }

    private void InitializePlanetData()
    {
        planetDataList.Clear();
        
        // Add planets in order of required unique resources
        planetDataList.Add(new PlanetData { planetName = "Moon", resourceName = "silver", requiredUniqueResources = 3, isDiscovered = false });
        planetDataList.Add(new PlanetData { planetName = "Venus", resourceName = "copper", requiredUniqueResources = 7, isDiscovered = false });
        planetDataList.Add(new PlanetData { planetName = "Mars", resourceName = "iron", requiredUniqueResources = 10, isDiscovered = false });
        planetDataList.Add(new PlanetData { planetName = "Jupiter", resourceName = "tin", requiredUniqueResources = 16, isDiscovered = false });
        planetDataList.Add(new PlanetData { planetName = "Mercury", resourceName = "mercury", requiredUniqueResources = 25, isDiscovered = false });
        planetDataList.Add(new PlanetData { planetName = "Saturn", resourceName = "lead", requiredUniqueResources = 35, isDiscovered = false });
    }

    private void InitializeDiscoveredResources()
    {
        // Sync with inventory's discovered resources list
        var inventoryDiscoveredResources = Inventory.instance.GetDiscoveredResourcesList();
        discoveredResources.Clear();
        
        // Add all resources from inventory's discovered list
        foreach (var resource in inventoryDiscoveredResources)
        {
            discoveredResources.Add(resource.Key);
        }

        // Ensure salt and sulfur are always available initially
        if (!discoveredResources.Contains("salt"))
        {
            discoveredResources.Add("salt");
        }
        if (!discoveredResources.Contains("sulfur"))
        {
            discoveredResources.Add("sulfur");
        }
        Debug.Log($"PlanetDiscoverySystem initialized with {discoveredResources.Count} discovered resources: {string.Join(", ", discoveredResources)}");
    }

    private void OnDiscoveredResourcesChanged(object sender, EventArgs e)
    {
        TryDiscoverPlanet();    
    }

    private void TryDiscoverPlanet()
    {
        var inventory = Inventory.instance.GetDiscoveredResourcesList();
        int uniqueResourceCount = 0;

        // Count unique resources in inventory
        foreach (var _ in inventory)
        {
            uniqueResourceCount++;
        }

        Debug.Log("Trying to discover a planet. total unique items: " + uniqueResourceCount.ToString());
        // Check if we can discover any planets
        foreach (var planet in planetDataList)
        {
            if (!planet.isDiscovered && uniqueResourceCount >= planet.requiredUniqueResources)
            {
                DiscoverPlanet(planet);
                return; // Only discover one planet at a time
            }
        }

        Debug.Log("No planets were discovered"); 
    }

    private void DiscoverPlanet(PlanetData planet)
    {
        planet.isDiscovered = true;
        discoveredResources.Add(planet.resourceName);
        
        Debug.Log($"Planet discovered: {planet.planetName}! Resource '{planet.resourceName}' is now available for mining!");
        
        // Trigger events
        OnPlanetDiscovered?.Invoke(this, planet.planetName);
        OnResourceDiscovered?.Invoke(this, planet.resourceName);
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
        var discoveredResourcesList = Inventory.instance.GetDiscoveredResourcesList();
        return discoveredResourcesList.Count;
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
