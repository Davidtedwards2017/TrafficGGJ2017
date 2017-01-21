using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class EventForwarding : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {

	public GameObject target;

	
	//These booleans are used to prevent memory leaks caused by having two objects mutually forward events
	bool triggered = false;
	
	bool triggeredEnter = false;
	bool triggeredExit = false;
	bool triggeredDown = false;
	bool triggeredUp = false;
	bool triggeredClick = false;

	
	void Update () {
		if(!triggered) return;
		
		triggeredEnter = false;
		triggeredExit = false;
		triggeredDown = false;
		triggeredUp = false;
		triggeredClick = false;
		
		triggered = false;
	}
	#region IPointerEnterHandler implementation
	
	public void OnPointerEnter (PointerEventData eventData)
	{
		if(triggeredEnter) return;
		triggeredEnter = true;
	
		if(target) ExecuteEvents.Execute(target, eventData, ExecuteEvents.pointerEnterHandler);	
		
		triggered = true;
	}
	
	#endregion
	
	#region IPointerExitHandler implementation
	
	public void OnPointerExit (PointerEventData eventData)
	{
		if(triggeredExit) return;
		triggeredExit = true;
	
		if(target) ExecuteEvents.Execute(target, eventData, ExecuteEvents.pointerExitHandler);
	}
	
	#endregion
	
	#region IPointerDownHandler implementation
	
	public void OnPointerDown (PointerEventData eventData)
	{
		if(triggeredDown) return;
		triggeredDown = true;
	
		if(target) ExecuteEvents.Execute(target, eventData, ExecuteEvents.pointerDownHandler);
		
		triggered = true;
	}
	
	#endregion
	
	#region IPointerUpHandler implementation
	
	public void OnPointerUp (PointerEventData eventData)
	{
		if(triggeredUp) return;
		triggeredUp = true;
	
		if(target) ExecuteEvents.Execute(target, eventData, ExecuteEvents.pointerUpHandler);
		
		triggered = true;
	}
	
	#endregion
	
	#region IPointerClickHandler implementation
	
	public void OnPointerClick (PointerEventData eventData)
	{
		if(triggeredClick) return;
		triggeredClick = true;
		
		if(target) ExecuteEvents.Execute(target, eventData, ExecuteEvents.pointerClickHandler);

		triggered = true;
		
	}
	
	#endregion

}
