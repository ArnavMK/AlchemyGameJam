using UnityEngine;

public enum ResourceType
{
    None,
    Powder,
    Crystal,
    SpecialCrystal,
    Metal
}

public class Resource
{
    private ResourceType type;
    private string color;
    private int usecases;
    private bool canBeCooked = true;
    private string resourceName;

    public Resource(
        ResourceType type,
        string color,
        int usecases,
        bool canBeCooked,
        string resourceName
    )
    {
        this.type = type;
        this.color = color;
        this.usecases = usecases;
        this.canBeCooked = canBeCooked;
        this.resourceName = resourceName;
    }

    public override string ToString()
    {
        return resourceName;
    }

    public string GetName()
    {
        return resourceName;
    }

    public ResourceType GetResourceType()
    {
        return type;
    }

    public string GetColor()
    {
        return color;
    }

    public int GetUsecases()
    {
        return usecases;
    }

    public bool CanBeCooked()
    {
        return canBeCooked;
    }

}