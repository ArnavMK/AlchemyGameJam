using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using System;

public class ResourceTile : MonoBehaviour
{

    [SerializeField] private TMP_Text resourceName_TMP;
    [SerializeField] private TMP_Text resourceQuantity_TMP;
    [SerializeField] private Image resourceIcon_Image;

    private Resource resource;
    public static event EventHandler<Resource> OnResourceTileClicked;

    public void Initialize(string resourceName, int quantity)
    {
        resource = Cauldron.instance.GetRecipeManager().GetResourceFromKey(resourceName);
        if (resource == null)
        {
            Debug.LogError("Resource not found: " + resourceName);
            return;
        }

        resourceName_TMP.text = resource.GetName();
        resourceQuantity_TMP.text = quantity.ToString();
    }

    public void OnClick()
    {
        OnResourceTileClicked?.Invoke(this, resource);
    }
}
