using System.Collections;
using NUnit.Framework;
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
    public static MySceneManager instance { get; private set; }

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform labPosition; // Lab
    [SerializeField] private Transform observatoryPosition; // Observatory
    [SerializeField] private Transform minePosition; // Mine
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject cookButton;
    [SerializeField] private GameObject backbutton;
    [SerializeField] private GameObject cauldronIngredientEditor;
    [SerializeField] private GameObject timeBarGame;
    [SerializeField] private GameObject mineButton;
    [SerializeField] private TMP_Text sceneText;
    [SerializeField] private Animator sceneCoverController;
    [SerializeField] private AudioSource cauldronBubbleSound;
    [SerializeField] private AudioSource spaceMusic;

    private SceneState sceneState = SceneState.Observatory;
    private bool requireObservatoryVisit;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Subscribe to the button event
        SceneButton.OnSceneChangeRequested += SceneButton_OnSceneChangeRequested;
        SwitchScene(GetCurrentSceneState());
        // Force a visit to Observatory when a planet is discovered
        if (PlanetDiscoverySystem.instance != null)
        {
            PlanetDiscoverySystem.instance.OnPlanetDiscovered += OnPlanetDiscovered;
        }
    }

    private void OnDestroy()
    {
        SceneButton.OnSceneChangeRequested -= SceneButton_OnSceneChangeRequested;
        if (PlanetDiscoverySystem.instance != null)
        {
            PlanetDiscoverySystem.instance.OnPlanetDiscovered -= OnPlanetDiscovered;
        }
    }

    private void SceneButton_OnSceneChangeRequested(object sender, SceneState state)
    {
        ChangeSceneTo(state);
    }

    public void ChangeSceneTo(SceneState state)
    {
        Debug.Log("Scene change requested to scene ID: " + state);

        sceneCoverController.gameObject.SetActive(true);
        sceneCoverController.SetTrigger("OnSceneChanged");
        StartCoroutine(DelaySwitchScene(state, 0.38f));
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
                backbutton.SetActive(false);
                timeBarGame.SetActive(false);
                mineButton.SetActive(false);
                cookButton.SetActive(true);
                cauldronBubbleSound.Play();
                spaceMusic.Stop();
                cauldronIngredientEditor.SetActive(true);
                break;

            case SceneState.Observatory:
                cameraTransform.position = observatoryPosition.position;
                cameraTransform.rotation = observatoryPosition.rotation;
                inventoryUI.SetActive(false);
                cookButton.SetActive(false);
                backbutton.SetActive(true);
                timeBarGame.SetActive(false);
                mineButton.SetActive(false);
                cauldronBubbleSound.Stop();
                spaceMusic.Play();
                cauldronIngredientEditor.SetActive(false);
                // Visiting Observatory clears the requirement
                requireObservatoryVisit = false;
                break;

            case SceneState.Mine:
                cameraTransform.position = minePosition.position;
                cameraTransform.rotation = minePosition.rotation;
                inventoryUI.SetActive(false);
                cookButton.SetActive(false);
                backbutton.SetActive(true);
                timeBarGame.SetActive(true);
                mineButton.SetActive(true);
                cauldronBubbleSound.Stop();
                spaceMusic.Stop();
                cauldronIngredientEditor.SetActive(false);
                break;
        }
        sceneText.SetText(sceneState.ToString());
        Debug.Log("Switched to scene: " + sceneState);
    }
    
    /// <summary>
    /// Gets the current scene state - useful for other systems like BackButton
    /// </summary>
    public SceneState GetCurrentSceneState()
    {
        return sceneState;
    }

    // Gate navigation if Observatory visit is pending
    public bool CanNavigateTo(SceneState target)
    {
        if (!requireObservatoryVisit)
        {
            return true;
        }
        return target == SceneState.Observatory;
    }

    private void OnPlanetDiscovered(object sender, string planetName)
    {
        Debug.Log($"Planet discovered: {planetName}. Forcing visit to Observatory.");
        requireObservatoryVisit = true;
		// Navigate to Observatory after a short delay (plays transition animation)
		StartCoroutine(ForceVisitObservatoryAfterDelay(1f));
    }

	private IEnumerator ForceVisitObservatoryAfterDelay(float delaySeconds)
	{
		yield return new WaitForSeconds(delaySeconds);
		SceneButton.InvokeEvent(this, SceneState.Observatory);
	}
}
