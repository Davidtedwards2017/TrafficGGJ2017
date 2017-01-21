using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupToggler : MonoBehaviour {

	CanvasGroup canvasGroup;
	public bool isInteractive = false; // Whether this is an interactive canvas group

	public List<CanvasGroup> toggleGroups;
	

	// Use this for initialization
	void Start () {
		canvasGroup = GetComponent<CanvasGroup>();
	}
	
//	void Update() {
//		if(OtherGroupVisible()) {
//			Hide ();
//		} else {
//			Show();
//		}
//	}
	
	public void Hide() {
		if(isInteractive) { 
			canvasGroup.SetInteractive(false);
		} else {
			canvasGroup.Hide();
		}
	}
	
	public void Show() {
		if(isInteractive) { 
			canvasGroup.SetInteractive(true);
		} else {
			canvasGroup.Show();
		}
	}
	
	public void Toggle() {
		if(canvasGroup.alpha == 0f) {
			Show ();
		} else {
			Hide ();
		}
	}
	
	bool OtherGroupVisible () {
		foreach(CanvasGroup group in toggleGroups) {
			if(group.alpha > 0.0f) return true;
		}
		return false;
	}
}
