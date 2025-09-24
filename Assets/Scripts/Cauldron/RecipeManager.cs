using System.Collections.Generic;

public class RecipeManager
{
    // this will hold all the recipes in the game which wil be compared to the players recipes
    private Dictionary<string, Recipe> recipeDictionary;
    private Dictionary<string, Resource> resourceDictionary;

    public RecipeManager()
    {
        resourceDictionary = new Dictionary<string, Resource>
        {
            { "salt", new Resource(ResourceType.Powder, "white", 4, true, "salt") },
            { "sulfur", new Resource(ResourceType.Crystal, "yellow", 7, true, "sulfur") },
            { "silver", new Resource(ResourceType.Metal, "silver", 7, true, "silver") },
            { "copper", new Resource(ResourceType.Metal, "orange", 8, true, "copper") },
            { "iron", new Resource(ResourceType.Metal, "grey", 7, true, "iron") },
            { "tin", new Resource(ResourceType.Metal, "yellow-grey", 4, true, "tin") },
            { "mercury", new Resource(ResourceType.Metal, "lightblue-silver", 4, true, "mercury") },
            { "lead", new Resource(ResourceType.Metal, "darkblue-silver", 3, true, "lead") },
            { "titanium", new Resource(ResourceType.Metal, "silver", 1, true, "titanium") },
            { "aluminium", new Resource(ResourceType.Metal, "silver", 2, true, "aluminium") },
            { "cobalt", new Resource(ResourceType.Metal, "darkblue", 4, true, "cobalt") },
            { "zinc", new Resource(ResourceType.Metal, "silver", 3, true, "zinc") },
            { "palladium", new Resource(ResourceType.Metal, "orange-silver", 2, true, "palladium") },
            { "crystal", new Resource(ResourceType.Crystal, "white", 6, true, "crystal") },
            { "bronze", new Resource(ResourceType.Metal, "lightbrown", 4, true, "bronze") },
            { "stormmetal", new Resource(ResourceType.Metal, "darkgrey", 1, true, "stormmetal") },
            { "ruby", new Resource(ResourceType.Crystal, "red", 1, true, "ruby") },
            { "greyflow", new Resource(ResourceType.Metal, "lightgrey", 1, true, "greyflow") },
            { "skysteel", new Resource(ResourceType.Metal, "blue", 1, true, "skysteel") },
            { "heavensteel", new Resource(ResourceType.Metal, "verydarkblue", 1, true, "heavensteel") },
            { "red aether", new Resource(ResourceType.Crystal, "pink", 1, true, "red aether") },
            { "philosoper's stone", new Resource(ResourceType.SpecialCrystal, "red", 0, true, "philosoper's stone") },
            { "tarnish", new Resource(ResourceType.Powder, "black", 0, true, "tarnish") },
            { "chalcocite", new Resource(ResourceType.Crystal, "black", 0, true, "chalcocite") },
            { "sterling", new Resource(ResourceType.Metal, "silver", 0, true, "sterling") },
            { "rust", new Resource(ResourceType.Powder, "darkred", 0, true, "rust") },
            { "verdantium", new Resource(ResourceType.Metal, "darkgreen", 0, true, "verdantium") },
            { "sulfurous tin", new Resource(ResourceType.Metal, "yellow", 0, true, "sulfurous tin") },
            { "pewter", new Resource(ResourceType.Metal, "silver", 0, true, "pewter") },
            { "greysteel", new Resource(ResourceType.Metal, "grey", 0, true, "greysteel") },
            { "cinnabar", new Resource(ResourceType.Metal, "red", 0, true, "cinnabar") },
            { "morphic copper", new Resource(ResourceType.Metal, "light orange", 0, true, "morphic copper") },
            { "brine", new Resource(ResourceType.Powder, "lightgrey", 0, true, "brine") },
            { "ferrocobalt", new Resource(ResourceType.Metal, "blue", 0, true, "ferrocobalt") },
            { "galvanite", new Resource(ResourceType.Metal, "grey", 0, true, "galvanite") },
            { "permalloy", new Resource(ResourceType.Metal, "lightblue", 0, true, "permalloy") },
            { "brass", new Resource(ResourceType.Metal, "brass", 0, true, "brass") },
            { "amber", new Resource(ResourceType.Crystal, "orange", 0, true, "amber") },
            { "quartz", new Resource(ResourceType.Crystal, "white", 0, true, "quartz") },
            { "moisanite", new Resource(ResourceType.Crystal, "lightblue", 0, true, "moisanite") },
            { "saphire", new Resource(ResourceType.Crystal, "darkblue", 0, true, "saphire") },
            { "diamond", new Resource(ResourceType.Crystal, "lightblue", 0, true, "diamond") },
            { "eclipsed bronze", new Resource(ResourceType.Metal, "darkbrown", 0, true, "eclipsed bronze") },
            { "gold", new Resource(ResourceType.Metal, "yellow", 0, resourceName: "gold", canBeCooked: true)},
            { "soft bronze", new Resource(ResourceType.Metal, "lightbrown", 0, true, "soft bronze") }
        };

        recipeDictionary = new Dictionary<string, Recipe>
        {
            { Recipe.CreateId(resourceDictionary["salt"], resourceDictionary["sulfur"]), new Recipe(resourceDictionary["salt"], resourceDictionary["sulfur"], resourceDictionary["titanium"]) },
            { Recipe.CreateId(resourceDictionary["salt"], resourceDictionary["silver"]), new Recipe(resourceDictionary["salt"], resourceDictionary["silver"], resourceDictionary["crystal"]) },
            { Recipe.CreateId(resourceDictionary["salt"], resourceDictionary["iron"]), new Recipe(resourceDictionary["salt"], resourceDictionary["iron"], resourceDictionary["rust"]) },
            { Recipe.CreateId(resourceDictionary["salt"], resourceDictionary["lead"]), new Recipe(resourceDictionary["salt"], resourceDictionary["lead"], resourceDictionary["brine"]) },
            { Recipe.CreateId(resourceDictionary["sulfur"], resourceDictionary["silver"]), new Recipe(resourceDictionary["sulfur"], resourceDictionary["silver"], resourceDictionary["tarnish"]) },
            { Recipe.CreateId(resourceDictionary["sulfur"], resourceDictionary["copper"]), new Recipe(resourceDictionary["sulfur"], resourceDictionary["copper"], resourceDictionary["chalcocite"]) },
            { Recipe.CreateId(resourceDictionary["sulfur"], resourceDictionary["tin"]), new Recipe(resourceDictionary["sulfur"], resourceDictionary["tin"], resourceDictionary["sulfurous tin"]) },
            { Recipe.CreateId(resourceDictionary["sulfur"], resourceDictionary["mercury"]), new Recipe(resourceDictionary["sulfur"], resourceDictionary["mercury"], resourceDictionary["cinnabar"]) },
            { Recipe.CreateId(resourceDictionary["sulfur"], resourceDictionary["crystal"]), new Recipe(resourceDictionary["sulfur"], resourceDictionary["crystal"], resourceDictionary["amber"]) },
            { Recipe.CreateId(resourceDictionary["sulfur"], resourceDictionary["bronze"]), new Recipe(resourceDictionary["sulfur"], resourceDictionary["bronze"], resourceDictionary["eclipsed bronze"]) },
            { Recipe.CreateId(resourceDictionary["silver"], resourceDictionary["copper"]), new Recipe(resourceDictionary["silver"], resourceDictionary["copper"], resourceDictionary["sterling"]) },
            { Recipe.CreateId(resourceDictionary["silver"], resourceDictionary["tin"]), new Recipe(resourceDictionary["silver"], resourceDictionary["tin"], resourceDictionary["pewter"]) },
            { Recipe.CreateId(resourceDictionary["silver"], resourceDictionary["titanium"]), new Recipe(resourceDictionary["silver"], resourceDictionary["titanium"], resourceDictionary["aluminium"]) },
            { Recipe.CreateId(resourceDictionary["silver"], resourceDictionary["crystal"]), new Recipe(resourceDictionary["silver"], resourceDictionary["crystal"], resourceDictionary["quartz"]) },
            { Recipe.CreateId(resourceDictionary["silver"], resourceDictionary["bronze"]), new Recipe(resourceDictionary["silver"], resourceDictionary["bronze"], resourceDictionary["cobalt"]) },
            { Recipe.CreateId(resourceDictionary["copper"], resourceDictionary["iron"]), new Recipe(resourceDictionary["copper"], resourceDictionary["iron"], resourceDictionary["verdantium"]) },
            { Recipe.CreateId(resourceDictionary["copper"], resourceDictionary["tin"]), new Recipe(resourceDictionary["copper"], resourceDictionary["tin"], resourceDictionary["bronze"]) },
            { Recipe.CreateId(resourceDictionary["copper"], resourceDictionary["mercury"]), new Recipe(resourceDictionary["copper"], resourceDictionary["mercury"], resourceDictionary["morphic copper"]) },
            { Recipe.CreateId(resourceDictionary["copper"], resourceDictionary["aluminium"]), new Recipe(resourceDictionary["copper"], resourceDictionary["aluminium"], resourceDictionary["palladium"]) },
            { Recipe.CreateId(resourceDictionary["copper"], resourceDictionary["zinc"]), new Recipe(resourceDictionary["copper"], resourceDictionary["zinc"], resourceDictionary["brass"]) },
            { Recipe.CreateId(resourceDictionary["copper"], resourceDictionary["bronze"]), new Recipe(resourceDictionary["copper"], resourceDictionary["bronze"], resourceDictionary["soft bronze"]) },
            { Recipe.CreateId(resourceDictionary["iron"], resourceDictionary["tin"]), new Recipe(resourceDictionary["iron"], resourceDictionary["tin"], resourceDictionary["greysteel"]) },
            { Recipe.CreateId(resourceDictionary["iron"], resourceDictionary["cobalt"]), new Recipe(resourceDictionary["iron"], resourceDictionary["cobalt"], resourceDictionary["ferrocobalt"]) },
            { Recipe.CreateId(resourceDictionary["iron"], resourceDictionary["zinc"]), new Recipe(resourceDictionary["iron"], resourceDictionary["zinc"], resourceDictionary["galvanite"]) },
            { Recipe.CreateId(resourceDictionary["iron"], resourceDictionary["palladium"]), new Recipe(resourceDictionary["iron"], resourceDictionary["palladium"], resourceDictionary["zinc"]) },
            { Recipe.CreateId(resourceDictionary["iron"], resourceDictionary["bronze"]), new Recipe(resourceDictionary["iron"], resourceDictionary["bronze"], resourceDictionary["stormmetal"]) },
            { Recipe.CreateId(resourceDictionary["mercury"], resourceDictionary["cobalt"]), new Recipe(resourceDictionary["mercury"], resourceDictionary["cobalt"], resourceDictionary["skysteel"]) },
            { Recipe.CreateId(resourceDictionary["mercury"], resourceDictionary["crystal"]), new Recipe(resourceDictionary["mercury"], resourceDictionary["crystal"], resourceDictionary["moisanite"]) },
            { Recipe.CreateId(resourceDictionary["lead"], resourceDictionary["aluminium"]), new Recipe(resourceDictionary["lead"], resourceDictionary["aluminium"], resourceDictionary["greyflow"]) },
            { Recipe.CreateId(resourceDictionary["lead"], resourceDictionary["crystal"]), new Recipe(resourceDictionary["lead"], resourceDictionary["crystal"], resourceDictionary["saphire"]) },
            { Recipe.CreateId(resourceDictionary["palladium"], resourceDictionary["crystal"]), new Recipe(resourceDictionary["palladium"], resourceDictionary["crystal"], resourceDictionary["ruby"]) },
            { Recipe.CreateId(resourceDictionary["cobalt"], resourceDictionary["zinc"]), new Recipe(resourceDictionary["cobalt"], resourceDictionary["zinc"], resourceDictionary["permalloy"]) },
            { Recipe.CreateId(resourceDictionary["cobalt"], resourceDictionary["crystal"]), new Recipe(resourceDictionary["cobalt"], resourceDictionary["crystal"], resourceDictionary["diamond"]) },
            { Recipe.CreateId(resourceDictionary["ruby"], resourceDictionary["greyflow"]), new Recipe(resourceDictionary["ruby"], resourceDictionary["greyflow"], resourceDictionary["red aether"]) },
            { Recipe.CreateId(resourceDictionary["stormmetal"], resourceDictionary["skysteel"]), new Recipe(resourceDictionary["stormmetal"], resourceDictionary["skysteel"], resourceDictionary["heavensteel"]) },
            { Recipe.CreateId(resourceDictionary["heavensteel"], resourceDictionary["red aether"]), new Recipe(resourceDictionary["heavensteel"], resourceDictionary["red aether"], resourceDictionary["philosoper's stone"]) }
        };

    }

    public Recipe GetRecipeFromKey(string key)
    {
        if (recipeDictionary.ContainsKey(key))
        {
            Recipe recipe = recipeDictionary[key];
            return recipe;
        }
        else
        {
            return null;
        }
    }

    public Resource GetResourceFromKey(string name)
    {
        if (resourceDictionary.ContainsKey(name))
        {
            Resource resource = resourceDictionary[name];
            return resource;
        }
        else
        {
            return null;
        }
    }
}
