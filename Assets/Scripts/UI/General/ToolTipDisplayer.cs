using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[AddComponentMenu("UI/ToolTipDisplayer", 35)]
[RequireComponent (typeof (RectTransform))]

public class ToolTipDisplayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [Multiline]
	public string message;
	
	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		ToolTip.Display(message);
	}
	
	public virtual void OnPointerExit(PointerEventData eventData)
	{
		ToolTip.Clear();
	}

    public virtual void OnMouseEnter()
    {
        ToolTip.Display(message);
    }

    public virtual void OnMouseExit()
    {
        ToolTip.Clear();
    }

    public void UpdateInfo()
    {
        ToolTip.instance.text.text = message;
    }
	
}
