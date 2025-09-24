using System.Collections;
using UnityEngine;

public class Planet : MonoBehaviour
{

    [SerializeField] private GameObject shadowPlanet;
    [SerializeField] private GameObject realPlanet;
    private PlanetShadowAnimation shadowAnimation;

    private void Start()
    {
        shadowAnimation = GetComponent<PlanetShadowAnimation>();
        shadowPlanet.SetActive(true);
        realPlanet.SetActive(false);
    }

    public IEnumerator RemoveShadow(float time)
    {
        yield return new WaitForSeconds(time);
        realPlanet.SetActive(true);

        yield return StartCoroutine(shadowAnimation.FadeOutCoroutine(1f));

        shadowPlanet.SetActive(false);

    }
}
