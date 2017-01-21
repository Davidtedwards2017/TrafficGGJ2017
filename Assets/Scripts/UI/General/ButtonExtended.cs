using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace UnityEngine.UI {
	[AddComponentMenu ("UI/Button Extended", 30)]
	public class ButtonExtended : UnityEngine.UI.Button
	{
		public mouseButtons acceptedMouseButtons;

//		public void Start() {
//		}
	
//		public override void OnPointerClick (PointerEventData eventData)
//		{
//			//Button button;
//			//button.
//			//this.Press ();
//			//this.DoStateTransition(SelectionState.Pressed, false);
//			//this.
//			this.OnSubmit(eventData);
//		}
		
		public override void OnPointerDown(PointerEventData eventData) {
			if (eventData.pointerId == -1 && !acceptedMouseButtons.left) return;  
			if (eventData.pointerId == -2 && !acceptedMouseButtons.right) return; // -1 for Left Click, -3 for Middle Click 
			if (eventData.pointerId == -3 && !acceptedMouseButtons.middle) return; 		
			DoStateTransition(SelectionState.Pressed, false);
		}
		
		public override void OnPointerUp(PointerEventData eventData) {
			DoStateTransition(SelectionState.Highlighted, false);
		}
		
		public override void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.pointerId == -1 && !acceptedMouseButtons.left) return;  
			if (eventData.pointerId == -2 && !acceptedMouseButtons.right) return; // -1 for Left Click, -3 for Middle Click 
			if (eventData.pointerId == -3 && !acceptedMouseButtons.middle) return; 
				
			//(this as Button).onClick.Invoke();
			base.OnSubmit(eventData);
			//DoStateTransition(SelectionState.Pressed, false);
			
		}		
	
	}
	
	[System.Serializable]
	public class mouseButtons {
		public bool left = true;
		public bool right = true;
		public bool middle = false;	
	}
}

//using System;
//using System.Collections;
//using UnityEngine.Events;
//using UnityEngine.EventSystems;
//using UnityEngine.Serialization;
//
//namespace UnityEngine.UI
//{
//	// Button that's meant to work with mouse or touch-based devices.
//	[AddComponentMenu("UI/Button", 30)]
//	public class Button : Selectable, IPointerClickHandler, ISubmitHandler
//	{
//		public bool leftClicks = true;
//		public bool rightClicks = false;
//		public bool middleClicks = false;
//		
//		[Serializable]
//		public class ButtonClickedEvent : UnityEvent { }
//		
//		// Event delegates triggered on click.
//		[FormerlySerializedAs("onClick")]
//		[SerializeField]
//		private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();
//		//[SerializeField]
//		//private ButtonClickedEvent m_OnClickR = new ButtonClickedEvent();
//		
//		protected Button()
//		{ }
//		
//		public ButtonClickedEvent onClick
//		{
//			get { return m_OnClick; }
//			set { m_OnClick = value; }
//		}
//		    
//		
//		public void Press()
//		{
//			if (!IsActive() || !IsInteractable())
//				return;
//			
//			m_OnClick.Invoke();
//			//Added by jumpdrive
//			//DoStateTransition(SelectionState.Pressed, false);
//			//StartCoroutine(OnFinishSubmit());
//		}
//		
//		// Trigger all registered callbacks.
//		public virtual void OnPointerClick(PointerEventData eventData)
//		{
//			//            if (eventData.button != PointerEventData.InputButton.Left)
//			//                return;
//			if (eventData.button == PointerEventData.InputButton.Left && !leftClicks) return;
//			if (eventData.button == PointerEventData.InputButton.Middle && !middleClicks) return;
//			if (eventData.button == PointerEventData.InputButton.Right && !rightClicks) return;
//			
//			//Press();
//			OnSubmit(eventData);
//		}
//		
//		public virtual void OnSubmit(BaseEventData eventData)
//		{
//			Press();
//			
//			// if we get set disabled during the press
//			// don't run the coroutine.
//			if (!IsActive() || !IsInteractable())
//				return;
//			
//			//Added by jumpdrive
//			DoStateTransition(SelectionState.Pressed, false);
//			StartCoroutine(OnFinishSubmit());
//		}
//		
//		private IEnumerator OnFinishSubmit()
//		{
//			var fadeTime = colors.fadeDuration;
//			var elapsedTime = 0f;
//			
//			while (elapsedTime < fadeTime)
//			{
//				elapsedTime += Time.unscaledDeltaTime;
//				yield return null;
//			}
//			
//			DoStateTransition(currentSelectionState, false);
//		}
//	}
//}
