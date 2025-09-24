using UnityEngine;
using UnityEngine.SceneManagement;

public class ActualSceneManager: MonoBehaviour
{
    public static ActualSceneManager instance { get; private set; }
    
    void Awake()
    {
        // Make this object persist between scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void LoadScene1() { SceneManager.LoadScene(0); }
    public void LoadScene2() { SceneManager.LoadScene(1); }
    public void LoadScene3() { SceneManager.LoadScene(2); }
}