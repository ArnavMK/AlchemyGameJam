using UnityEngine;

public class HomeButton : MonoBehaviour
{
    public void OnClick()
    {
        ActualSceneManager.instance.LoadScene1();
    }
}
