
public class Recipe
{
    private Resource[] resources;
    private Resource result;

    public static string CreateId(Resource a, Resource b)
    {
        string[] names = new string[] { a.GetName(), b.GetName()};
        System.Array.Sort(names); 
        return $"{names[0]}+{names[1]}";
    }

    public Recipe(Resource r1, Resource r2, Resource result)
    {
        this.resources = new Resource[2];
        resources[0] = r1;
        resources[1] = r2;
        this.result = result;
    }

    public Resource GetResult()
    {
        return result;
    }

    public override string ToString()
    {
        string[] names = new string[] { resources[0].GetName(), resources[1].GetName()};
        System.Array.Sort(names); 
        return $"{names[0]}+{names[1]}";
    }


    public string getId()
    {
        string[] names = new string[] { resources[0].GetName(), resources[1].GetName()};
        System.Array.Sort(names); 
        return $"{names[0]}+{names[1]}";
    }
} 