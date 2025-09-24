using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public static Tooltip instance;

    [SerializeField] private TMP_Text tooltipText;
    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private Vector2 offset = new Vector2(-150f, 0f); // left side offset
    [SerializeField] private RectTransform rectTransform;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        HideTooltip();
    }

    private void Update()
    {
        if (backgroundPanel.activeSelf)
        {
            // Convert mouse position to world position for UI
            Vector2 mousePos = Input.mousePosition;
            rectTransform.position = mousePos + offset;
        }

        if (Input.GetMouseButtonDown(0))
        {
            HideTooltip();
        }
    }

    public void ShowTooltip(string message)
    {
        backgroundPanel.SetActive(true);
        tooltipText.text = message;
    }

    public void HideTooltip()
    {
        backgroundPanel.SetActive(false);
    }
}
