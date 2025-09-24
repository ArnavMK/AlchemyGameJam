using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ResourceTile : MonoBehaviour
{

    [SerializeField] private TMP_Text resourceName_TMP;
    [SerializeField] private TMP_Text resourceQuantity_TMP;
    [SerializeField] private RawImage resourceIcon_Image;
    [SerializeField] private ResourceSpriteDB spriteDB; 

    private Resource resource;

    public static event EventHandler<Resource> OnResourceTileClicked;



    public void Initialize(string resourceName, int quantity)
    {
        // resource look up (existing code)
        Resource resource = Cauldron.instance.GetRecipeManager().GetResourceFromKey(resourceName);

        if (resource == null)
        {
            Debug.LogError("Resource not found: " + resourceName);
            return;
        }
        this.resource = resource;
        resourceName_TMP.text = resource.GetName();
        resourceQuantity_TMP.text = quantity.ToString();

        // get sprite from DB
        if (spriteDB == null)
        {
            Debug.LogError("Sprite DB not assigned on ResourceTile prefab!");
            return;
        }

        Sprite s = spriteDB.GetSprite(resourceName);
        Debug.Log("Resource icon name:" + resourceName);
        if (s != null)
        {
            // If you use RawImage:
            resourceIcon_Image.texture = s.texture;

            // If you use Image (preferred for sprites), do:
            // imageComponent.sprite = s;
        }
        else
        {
            Debug.LogWarning($"Sprite not found in DB for resource: {resourceName}");
            // optionally set a fallback texture/sprite here
        }
    }

    public Resource GetResource()
    {
        return resource;
    }

    public void OnClick()
    {
        AudioManagerForClicking.instance.select.Play();
        OnResourceTileClicked?.Invoke(this, resource);
    }
}
