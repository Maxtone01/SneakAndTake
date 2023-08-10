using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ToolSpawner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip = null;

    [SerializeField]
    GameObject toolPrefab = null;
    public abstract void UpdateTool(GameObject tooltip);

    public abstract bool CanCreateTool();

    public void OnPointerEnter(PointerEventData eventData)
    {
        var canvas = GetComponentInParent<Canvas>();

        if (tooltip && !CanCreateTool())
        {
            ClearTooltip();
        }
        if (!tooltip && CanCreateTool())
        {
            tooltip = Instantiate(toolPrefab, canvas.transform);
        }
        if (tooltip)
        {
            UpdateTool(tooltip);
            PositionTooltip();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ClearTooltip();
    }

    private void PositionTooltip()
    {
        Canvas.ForceUpdateCanvases();

        Vector3[] tooltipCorners = new Vector3[4];
        tooltip.GetComponent<RectTransform>().GetWorldCorners(tooltipCorners);

        Vector3[] slotCorners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(slotCorners);

        bool below = transform.position.y > Screen.height / 2;
        bool right = transform.position.x < Screen.width / 2;

        int slotCorner = GetCornerIndex(below, right);
        int tooltipCorner = GetCornerIndex(!below, !right);

        tooltip.transform.position = slotCorners[slotCorner] - tooltipCorners[tooltipCorner] + tooltip.transform.position;
    }

    private int GetCornerIndex(bool below, bool right)
    {
        if (below && !right) return 0;
        else if (!below && !right) return 1;
        else if (!below && right) return 2;
        else return 3;
    }

    private void ClearTooltip()
    {
        if (tooltip)
        {
            Destroy(tooltip.gameObject);
        }
    }

    private void OnDestroy()
    {
        ClearTooltip();
    }

    private void OnDisable()
    {
        ClearTooltip();
    }
}
