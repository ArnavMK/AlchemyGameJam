using TMPro;
using UnityEngine;

public class CauldronOutputResource : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ResourceSpriteDB spriteDB;

    private Resource resource;

    public void Initialize(string resourceName)
    {
        resource = Cauldron.instance.GetRecipeManager().GetResourceFromKey(resourceName);
        if (resource == null)
        {
            Debug.LogError("Resource not found: " + resourceName);
            return;
        }

        Sprite s = spriteDB.GetSprite(resourceName);
        Debug.Log("Resource icon name:" + resourceName);
        if (s != null)
        {
            spriteRenderer.sprite = s; 

            // If you use Image (preferred for sprites), do:
            // imageComponent.sprite = s;
        }
        else
        {
            Debug.LogWarning($"Sprite not found in DB for resource: {resourceName}");
            // optionally set a fallback texture/sprite here
        }
    }

    public void OnClick()
    {
        Cauldron.instance.RemoveResourceIngredient(resource.GetName());
        Inventory.instance.AddItem(resource.GetName(), 1);
    }
}
