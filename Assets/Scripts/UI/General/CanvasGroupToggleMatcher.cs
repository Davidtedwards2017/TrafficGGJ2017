using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasGroupToggleMatcher : MonoBehaviour {

	public Toggle toggleToMatch;
	public CanvasGroup canvasGroup;
	public bool isInteractive; // Whether the canvas group needs to be interacted with

    public float fadeDuration = 0f;

	// Use this for initialization
	void Awake () {
		if(canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
		if(canvasGroup && toggleToMatch) MatchToggle(toggleToMatch.isOn);
	}
	

	void MatchToggle(bool value) {
		if(value) {
			Show();
		} else {
			Hide();
		}
	}
	
	public void Hide() {
		if(isInteractive) { 
			canvasGroup.SetInteractive(false, fadeDuration);
		} else {
			canvasGroup.Hide();
		}
	}
	
	public void Show() {
		if(isInteractive) { 
			canvasGroup.SetInteractive(true, fadeDuration);
		} else {
			canvasGroup.Show();
		}
	}	
	
	public void OnEnable() {
		if(toggleToMatch) toggleToMatch.onValueChanged.AddListener(MatchToggle);
	}
	
	public void OnDisable() {
		if(toggleToMatch) toggleToMatch.onValueChanged.RemoveListener(MatchToggle);
	}

}
