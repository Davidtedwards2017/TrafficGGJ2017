using UnityEngine;
using System.Collections;

public class AutoRotateBehavior : MonoBehaviour {

	public float Pitch  = 0.0f;
	public float Yaw    = 0.0f;
	public float Roll   = 0.0f;
	public bool unscaledTime = false;

	[Space(10)]
	public bool IntermittentChange = false;
	public float changeRate = 1.0f;
	public float pitchChange = 0.0f;
	public float yawChange = 0.0f;
	public float rollChange = 0.0f;
	
	
	// Use this for initialization
	void Start () {
		if (IntermittentChange) {
			InvokeRepeating ("ChangeDirection", changeRate, changeRate);
		}
	}
	
	// Update is called once per frame
	void Update () {
		

	}

	void ChangeDirection() 
	{
		Pitch += Random.Range (-pitchChange, pitchChange);
		Yaw += Random.Range (-yawChange, yawChange);
		Roll += Random.Range (-rollChange, rollChange);

	}
	
	public IEnumerator Rotation() {
	
		while(true) {
			if(unscaledTime) {
				transform.Rotate(Pitch*Time.unscaledDeltaTime, Yaw*Time.unscaledDeltaTime, Roll*Time.unscaledDeltaTime);
			} else {
				transform.Rotate(Pitch*Time.deltaTime, Yaw*Time.deltaTime, Roll*Time.deltaTime);
			}		
			yield return null;
		}
	}
	
	public void OnEnable() {
		StartCoroutine(RadicalRoutine.Run(Rotation()));
	}
	
	public void OnDisable() {
		StopAllCoroutines();
	}

}
