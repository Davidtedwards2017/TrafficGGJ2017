using UnityEngine;
using System.Collections;

public class DeactivateMe : MonoBehaviour {

	// Use this for initialization
	void Awake() {
		gameObject.SetActive(false);
	}
}
