using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnClick()
    {
        ActualSceneManager.instance.LoadScene3();
    }
}
