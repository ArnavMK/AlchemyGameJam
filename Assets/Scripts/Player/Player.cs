using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance { get; private set; }

    private Inventory inventory;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventory = Inventory.instance;
    }
    
}
