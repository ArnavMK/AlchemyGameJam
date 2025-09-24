using System;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    public static MineManager instance { get; private set; }

    private List<string> mineableResources = new();

    public event EventHandler<KeyValuePair<string, string>> OnResourceMined;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Initialize with base resources
        mineableResources.Add("salt");
        mineableResources.Add("sulfur");

        PlanetDiscoverySystem.instance.OnResourceDiscovered += OnResourceDiscovered;
        
        Debug.Log("MineManager initialized with base resources: salt, sulfur");
    }

    private void OnResourceDiscovered(object sender, string e)
    {
        if (!mineableResources.Contains(e))
        {
            mineableResources.Add(e);
            Debug.Log($"New resource '{e}' added to mineable resources list");
        }
    }

    /// <summary>
    /// Gets a random mineable resource
    /// </summary>
    public string GetRandomMineableResource()
    {
        if (mineableResources.Count == 0)
        {
            Debug.LogWarning("No mineable resources available!");
            return "salt"; // Fallback
        }

        int randomIndex = UnityEngine.Random.Range(0, mineableResources.Count);
        string reward = mineableResources[randomIndex];
        OnResourceMined?.Invoke(this, new KeyValuePair<string, string>(reward, "New resource mined: " + reward));
        return reward;
    }

    /// <summary>
    /// Gets all mineable resources
    /// </summary>
    public List<string> GetAllMineableResources()
    {
        return new List<string>(mineableResources);
    }

    /// <summary>
    /// Checks if a resource is mineable
    /// </summary>
    public bool IsResourceMineable(string resourceName)
    {
        return mineableResources.Contains(resourceName);
    }
}
