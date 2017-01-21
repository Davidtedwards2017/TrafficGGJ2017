using UnityEngine;
using System.Collections;

public class LookAtBehavior : MonoBehaviour {
	
	public Component target;
	
	void Start () {
		if(target == null) this.enabled = false;
	}
	
	void Update () {
		transform.LookAt(target.transform);
	}
}
