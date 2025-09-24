using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void OnClick()
    {
        ActualSceneManager.instance.LoadScene2();
    }
}
