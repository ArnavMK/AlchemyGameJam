using UnityEngine;

public class MineButton : MonoBehaviour
{
    public void OnClick()
    {
        TimebarGame.instance.StartMinigame();   
    }

}
