using UnityEngine;

public class CookButton : MonoBehaviour
{
    public void OnClick()
    {
        Cauldron.instance.Cook();
    }
}