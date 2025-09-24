using UnityEngine;

/// <summary>
/// Back button that always switches back to the Lab scene regardless of current scene
/// </summary>
public class BackButton : MonoBehaviour
{
    [Header("Back Button Settings")]
    [SerializeField] private bool showDebugLogs = true;
    
    private void Start()
    {
        // Ensure this button only works when not already in the lab
        UpdateVisibility();
    }

    /// <summary>
    /// Called when the back button is clicked - always switches to Lab scene
    /// </summary>
    public void OnBackButtonClicked()
    {

        if (DialogueSystem.instance.isDialogueOngoing == true)
        {
            return;
        }

        if (showDebugLogs)
        {
            Debug.Log("Back button clicked - switching to Lab scene");
        }

        // Trigger the scene change event to switch to Lab
        SceneButton.InvokeEvent(this, SceneState.Lab);
    }
    
    
    /// <summary>
    /// Method to check if back button should be visible
    /// Can be called by UI systems to show/hide the button
    /// </summary>
    public bool ShouldShowBackButton()
    {
        // Hide the button when already in Lab since there's nowhere to go back to
        return MySceneManager.instance != null && MySceneManager.instance.GetCurrentSceneState() != SceneState.Lab;
    }
    
    /// <summary>
    /// Updates the button's visibility based on current scene
    /// Call this method when scenes change to show/hide the back button appropriately
    /// </summary>
    public void UpdateVisibility()
    {
        bool shouldShow = ShouldShowBackButton();
        gameObject.SetActive(shouldShow);
        
        if (showDebugLogs)
        {
            Debug.Log($"BackButton visibility updated: {shouldShow} (Current scene: {MySceneManager.instance?.GetCurrentSceneState()})");
        }
    }
}