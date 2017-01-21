using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayBroadcastManager : Singleton<DisplayBroadcastManager> {
	
	public Text displayText;
	public CanvasGroup displayTextCanvasGroup;
	
	public float displayTime;
	public float fadeTime;
	CoroutineManager.Item fadeSequence = new CoroutineManager.Item();
	
	float duration;
	
	void Awake () {
		displayText.text = "";	
		displayTextCanvasGroup.Hide();
	}
	
	public static void DisplayMessage (string message, float _duration = 0f) {        
		instance.StopAllCoroutines();
		instance.StartCoroutine(instance.DisplayMessageSequence(message, _duration));
	}
	
	
	IEnumerator DisplayMessageSequence (string message, float _duration = 0f) {
		//Fade the existing message out
		fadeSequence.value = FadeOutSequence();
		
		//Assign the new message and duration
		displayText.text = message;
		duration = _duration > 0f ? _duration : displayTime;
		//Fade the new message in
		if(message != "") {
            fadeSequence.value = FadeInSequence();

            yield return new WaitForSeconds(fadeTime);

			//Wait until the duration has completed
			yield return new WaitForSeconds (duration);

            //Fade the message out
            fadeSequence.value = FadeOutSequence();
            yield return new WaitForSeconds(fadeTime);
        }
	}
	
//	public static void FadeIn() {
//		if (instance.fadeAlpha != null) {
//			instance.StopCoroutine (instance.fadeAlpha);
//		}
//		instance.fadeAlpha = instance.FadeOutSequence ();
//		instance.StartCoroutine (instance.fadeAlpha);		
//	}	
	
	IEnumerator FadeInSequence() {
		while (displayTextCanvasGroup.alpha < 1) {
			displayTextCanvasGroup.alpha += Time.unscaledDeltaTime / fadeTime;
			yield return null;
		}
		
	}	
	
//	public static void FadeOut() {
//		if (instance.fadeAlpha != null) {
//			instance.StopCoroutine (instance.fadeAlpha);
//		}
//		instance.fadeAlpha = instance.FadeOutSequence ();
//		instance.StartCoroutine (instance.fadeAlpha);		
//	}

	IEnumerator FadeOutSequence() {
		while (displayTextCanvasGroup.alpha > 0) {
			displayTextCanvasGroup.alpha -= Time.unscaledDeltaTime / fadeTime;
			yield return null;
		}

	}
	
}