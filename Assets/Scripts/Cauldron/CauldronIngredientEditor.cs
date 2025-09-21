using UnityEngine;
using System.Collections.Generic;

public class CauldronIngredientEditor : MonoBehaviour
{
    [SerializeField] private ResourceTileCauldron resourceTileCauldronPrefab;
    [SerializeField] private GameObject resourceTileCauldronParentPanel;

    private void Start()
    {
        Cauldron.instance.OnResourceListChanged += Cauldron_OnResourceListChanged;
    }

    private void Cauldron_OnResourceListChanged(object sender, List<Resource> resource)
    {
        Debug.Log("Resource list for cauldron changed, updating UI");
        UpdateUi(resource);
    }

    private void UpdateUi(List<Resource> resources)
    {
        if (resourceTileCauldronParentPanel != null)
        {
            foreach (Transform child in resourceTileCauldronParentPanel.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < resources.Count; i++)
            {
                ResourceTileCauldron resourceTile = Instantiate(resourceTileCauldronPrefab, resourceTileCauldronParentPanel.transform);
                resourceTile.Initialize(resources[i].GetName());
            }

        }
    }
}