using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string tooltipMessage;
    private Resource resource;

    private void Start()
    {
        resource = GetComponent<ResourceTile>().GetResource();
        tooltipMessage = "Used in " + resource.usecases + " recipies.";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Tooltip.instance != null)
        {
            Debug.Log(tooltipMessage);
            Tooltip.instance.ShowTooltip(tooltipMessage);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Tooltip.instance != null)
        {
            Tooltip.instance.HideTooltip();
        }
    }
}
