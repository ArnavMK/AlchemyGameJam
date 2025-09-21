using UnityEngine;

public class SceneButton : MonoBehaviour
{

    [SerializeField] private SceneState state;

    public static event System.EventHandler<SceneState> OnSceneChangeRequested;

    public void OnClick()
    {
        Debug.Log("Scene button clicked for scene ID: " + state.ToString());
        OnSceneChangeRequested?.Invoke(this, state);
    }

}
