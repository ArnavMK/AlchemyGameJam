using UnityEngine;

public class SceneButton : MonoBehaviour
{

    [SerializeField] private SceneState state;

    public static event System.EventHandler<SceneState> OnSceneChangeRequested;

    public void OnClick()
    {
        if (DialogueSystem.instance.isDialogueOngoing == true)
        {
            return;
        }

        Debug.Log("Scene button clicked for scene ID: " + state.ToString());
        // Gate navigation if Observatory visit is required
        if (MySceneManager.instance == null || MySceneManager.instance.CanNavigateTo(state))
        {
            OnSceneChangeRequested?.Invoke(this, state);
        }
        else
        {
            Debug.Log("Navigation blocked: Observatory visit required.");
        }
    }

    public static void InvokeEvent(object o, SceneState state)
    {
        OnSceneChangeRequested?.Invoke(o, state);
    } 

}
