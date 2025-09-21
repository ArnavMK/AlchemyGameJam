using TMPro;
using UnityEngine;

public class ResourceTileCauldron : MonoBehaviour
{
    [SerializeField] private TMP_Text resourceName_TMP;

    private Resource resource;

    public void Initialize(string resourceName)
    {
        resource = Cauldron.instance.GetRecipeManager().GetResourceFromKey(resourceName);
        if (resource == null)
        {
            Debug.LogError("Resource not found: " + resourceName);
            return;
        }

        resourceName_TMP.text = resource.GetName(); 
    }

    public void OnClick()
    {
        Cauldron.instance.RemoveResourceIngredient(resource.GetName());
        Inventory.instance.AddItem(resource.GetName(), 1);
    }
}
