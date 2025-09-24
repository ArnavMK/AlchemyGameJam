using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceTileCauldron : MonoBehaviour
{
    [SerializeField] private TMP_Text resourceName_TMP;
    [SerializeField] private RawImage resourceIcon_Image;
    [SerializeField] private ResourceSpriteDB spriteDB;
    [SerializeField] private AudioSource deselectAudio;

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

    public void OnClick()
    {
        AudioManagerForClicking.instance.deselect.Play();
        Cauldron.instance.RemoveResourceIngredient(resource.GetName());
        Inventory.instance.AddItem(resource.GetName(), 1);
    }
}
