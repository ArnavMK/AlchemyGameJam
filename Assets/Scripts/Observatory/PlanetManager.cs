using UnityEngine;
using System.Collections.Generic;

public class PlanetManager : MonoBehaviour
{
    [SerializeField] private List<Planet> planets;

    private Dictionary<string, Planet> planetDictionary = new();
    private List<Planet> planetDiscoveryAnimationQeue = new();

    private void Start()
    {
        PlanetDiscoverySystem.instance.OnPlanetDiscovered += OnNewPlanetDiscovered;
        foreach (var planet in planets)
        {
            planetDictionary.Add(planet.transform.name, planet);
        }
    }

    private void Update()
    {
        // Only reveal discovered planets when in the Observatory scene
        if (MySceneManager.instance != null &&
            MySceneManager.instance.GetCurrentSceneState() == SceneState.Observatory &&
            planetDiscoveryAnimationQeue.Count > 0)
        {
            // Reveal all pending discovered planets
            for (int i = 0; i < planetDiscoveryAnimationQeue.Count; i++)
            {
                var planet = planetDiscoveryAnimationQeue[i];
                if (planet != null)
                {
                    StartCoroutine(planet.RemoveShadow(1f));
                }
            }
            planetDiscoveryAnimationQeue.Clear();
        }
    }

    private void OnNewPlanetDiscovered(object sender, string e)
    {
        planetDiscoveryAnimationQeue.Add(planetDictionary[e]);

        if (e == "Moon")
        {
            planetDiscoveryAnimationQeue.Add(planetDictionary["Sun"]);
        }
    }

    private void OnDestroy()
    {
        if (PlanetDiscoverySystem.instance != null)
        {
            PlanetDiscoverySystem.instance.OnPlanetDiscovered -= OnNewPlanetDiscovered;
        }
    }
}
