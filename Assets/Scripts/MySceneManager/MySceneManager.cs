using System.Collections;
using TMPro;
using UnityEngine;

public enum SceneState
{
    Lab,
    Observatory,
    Mine
}

public class MySceneManager : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform labPosition; // Lab
    [SerializeField] private Transform observatoryPosition; // Observatory
    [SerializeField] private Transform minePosition; // Mine
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject cookButton;
    [SerializeField] private TMP_Text sceneText;
    [SerializeField] private Animator sceneCoverController; 

    private SceneState sceneState = SceneState.Lab;

    private void Start()
    {
        // Subscribe to the button event
        SceneButton.OnSceneChangeRequested += SceneButton_OnSceneChangeRequested;
    }

    private void OnDestroy()
    {
        SceneButton.OnSceneChangeRequested -= SceneButton_OnSceneChangeRequested;
    }

    private void SceneButton_OnSceneChangeRequested(object sender, SceneState state)
    {
        Debug.Log("Scene change requested to scene ID: " + state);

        sceneCoverController.gameObject.SetActive(true);
        sceneCoverController.SetTrigger("OnSceneChanged");
        StartCoroutine(DelaySwitchScene(state, 1f));
        StartCoroutine(AnimationControllerSetter(2f));

    }

    private IEnumerator AnimationControllerSetter(float time)
    {
        yield return new WaitForSeconds(time);
        sceneCoverController.gameObject.SetActive(false);
    }

    private IEnumerator DelaySwitchScene(SceneState state, float time)
    {
        yield return new WaitForSeconds(time);
        SwitchScene(state);
    }

    private void SwitchScene(SceneState newState)
    {
        sceneState = newState;

        switch (sceneState)
        {
            case SceneState.Lab:
                cameraTransform.position = labPosition.position;
                cameraTransform.rotation = labPosition.rotation;
                inventoryUI.SetActive(true);
                cookButton.SetActive(true);
                break;

            case SceneState.Observatory:
                cameraTransform.position = observatoryPosition.position;
                cameraTransform.rotation = observatoryPosition.rotation;
                inventoryUI.SetActive(false);
                cookButton.SetActive(false);
                break;

            case SceneState.Mine:
                cameraTransform.position = minePosition.position;
                cameraTransform.rotation = minePosition.rotation;
                inventoryUI.SetActive(false);
                cookButton.SetActive(false);
                break;
        }
        sceneText.SetText(sceneState.ToString());
        Debug.Log("Switched to scene: " + sceneState);
    }
}
