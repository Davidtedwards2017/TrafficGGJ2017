using UnityEngine;
using System.Collections;

public class BillBoardBehavior : MonoBehaviour {
	
	public bool orientUp = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(orientUp) {
			Vector3 vToCamera = (Camera.main.transform.position - transform.position).normalized;
			//transform.up = Camera.main.transform.up;
			transform.rotation = Quaternion.LookRotation(vToCamera, Camera.main.transform.up);
			//transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
		} else {
			transform.LookAt(Camera.main.transform);
		}
			
		
	}
}
