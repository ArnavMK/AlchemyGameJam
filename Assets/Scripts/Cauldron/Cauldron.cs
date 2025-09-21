using System;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{

    public static Cauldron instance { get; private set; }

    private RecipeManager recipeManager;
    private List<Resource> currentResources = new();

    public event EventHandler<KeyValuePair<Resource, int>> OnCook;
    public event EventHandler<string> OnCookFailed;
    public event EventHandler<List<Resource>> OnResourceListChanged;

    private enum CauldronState
    {
        Empty,
        Cooking,
        Ready
    }

    private CauldronState currentState;

    private void Awake()
    {
        instance = this;
        recipeManager = new RecipeManager();
        Debug.Log("recipeManager initialized");
    }

    private void Start()
    {
        Inventory.instance.OnItemUsed += Inventory_OnItemUsed;
    }

    private void Inventory_OnItemUsed(object sender, string itemName)
    {
        Resource resource = recipeManager.GetResourceFromKey(itemName);
        if (resource == null)
        {
            Debug.LogError("Resource not found in recipe manager: " + itemName);
            return;
        }

        if (currentResources.Count < 2 & resource.CanBeCooked())
        {
            AddResourceIngredient(resource);
        }
        else
        {
            Debug.Log("Cauldron is full! Cannot add more resources!");
        }
    } 

    public Resource Cook()
    {

        if (currentState == CauldronState.Ready)
        {
            string id = Recipe.CreateId(currentResources[0], currentResources[1]);
            Recipe matchingRecipe = recipeManager.GetRecipeFromKey(id);

            if (matchingRecipe != null)
            {
                Resource result = matchingRecipe.GetResult();
                OnCook?.Invoke(this, new KeyValuePair<Resource, int>(result, UnityEngine.Random.Range(1, 4)));
                currentState = CauldronState.Empty;
                currentResources.Clear();
                OnResourceListChanged?.Invoke(this, currentResources);
                return result;
            }

            OnCookFailed?.Invoke(this, "Cooking failed! Your ingredients are not making a result!");
            return null;
        }

        OnCookFailed?.Invoke(this, "Cauldron might be empty or still cooking!");
        return null;
    }

    public void AddResourceIngredient(Resource resource)
    {
        if (currentResources.Count >= 2)
        {
            Debug.Log("Cauldron is full! Cannot add more resources!");
            return;
        }

        currentResources.Add(resource);
        Debug.Log($"Added resource: {resource.ToString()}");
        OnResourceListChanged?.Invoke(this, currentResources);
        if (currentResources.Count == 2)
        {
            currentState = CauldronState.Ready;
        }
    }

    public void RemoveResourceIngredient(string resourceName)
    {
        Resource resourceToRemove = currentResources.Find(r => r.GetName() == resourceName);
        if (resourceToRemove != null)
        {
            currentResources.Remove(resourceToRemove);
            Debug.Log($"Removed resource: {resourceToRemove.ToString()}");
            OnResourceListChanged?.Invoke(this, currentResources);
            if (currentResources.Count < 2)
            {
                currentState = CauldronState.Empty;
            }
        }
        else
        {
            Debug.Log($"Resource not found in cauldron: {resourceName}");
        }
    }

    public void ClearCauldron()
    {
        currentResources.Clear();
        currentState = CauldronState.Empty;
    }

    public RecipeManager GetRecipeManager()
    {
        return recipeManager;
    }

}
