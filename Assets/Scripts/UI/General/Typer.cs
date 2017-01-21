using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Text))]
public class Typer : MonoBehaviour {

	public string msg = "Replace";
	private Text textComp;
	public float startDelay = 2f;
	public float typeDelay = 0.0f;
	public AudioClip typeSound;

	void Awake() {
		textComp = GetComponent<Text>();
	}

	void Start () {
		StartCoroutine ("TypeIn");
	}
	
	public IEnumerator TypeIn() {
		yield return new WaitForSeconds(startDelay);

		for(int i = 0; i < msg.Length; i++) {
			textComp.text = msg.Substring(0, i);
			GetComponent<AudioSource>().PlayOneShot(typeSound);
			yield return new WaitForSeconds(typeDelay);
		}
	}
	
	public IEnumerator TypeOff() {
		yield return new WaitForSeconds(startDelay);
		
		for(int i = 0; i >= 0; i--) {
			textComp.text = msg.Substring(0, i);
			yield return new WaitForSeconds(typeDelay);
		}
	}	

}

