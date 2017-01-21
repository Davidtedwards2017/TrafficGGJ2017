using UnityEngine;
using System.Collections;

public class DestroyBehavior : MonoBehaviour {

	public float lifeTime;

	// Use this for initialization
	void Start () {
		Invoke ("DestroyThis", lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void DestroyThis() {
		Destroy (gameObject);
	}
}
