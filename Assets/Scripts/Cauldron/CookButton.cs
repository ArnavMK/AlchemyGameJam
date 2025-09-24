using UnityEngine;

public class CookButton : MonoBehaviour
{

    public void OnClick()
    {
        if (!DialogueSystem.instance.isDialogueOngoing)
        {
            Cauldron.instance.Cook();
        }
    }
}