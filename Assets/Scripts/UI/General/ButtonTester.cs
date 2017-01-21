using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonTester : MonoBehaviour {

	public Slider slider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ButtonTest() {
	
		Debug.Log (slider.value.ToString ());
	}
	
	public void DoSomethingWithASlider(Slider slider) {
		Debug.Log(slider.value.ToString ());
	}
}
