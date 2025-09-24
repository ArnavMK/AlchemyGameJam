using System;
using System.Collections.Generic;
using UnityEngine;

public class ResultSpawner : MonoBehaviour
{

    [SerializeField] private GameObject resultPrefab;

    private void Start()
    {
        Cauldron.instance.OnCook += Cauldron_OnCook;
    }

    private void Cauldron_OnCook(object sender, KeyValuePair<Resource, int> result)
    {
        Debug.Log("Spawning result: " + result.Key.GetName());

        for (int i = 0; i < result.Value; i++)
        {
            GameObject prefab = Instantiate(this.resultPrefab, transform.position, Quaternion.identity);

            if (prefab.TryGetComponent(out CauldronOutputResource component))
            {
                component.Initialize(result.Key.GetName());
            }

            if (prefab.TryGetComponent(out Rigidbody2D rb))
            {
                rb.gravityScale = 3f;
                rb.AddForce(GetRandomForce(), ForceMode2D.Impulse);
            }
        }
    }

    private Vector2 GetRandomForce()
    {
        float angle = UnityEngine.Random.Range(70f, 120f);
        float rad = angle * Mathf.Deg2Rad;
        float force = UnityEngine.Random.Range(10f, 16f);

        Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        return direction * force;
    }   

}
