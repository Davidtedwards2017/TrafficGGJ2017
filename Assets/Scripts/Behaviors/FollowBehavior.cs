using UnityEngine;
using System.Collections;

public class FollowBehavior : MonoBehaviour
{

    public Transform followTarget;

    [Range(0f, 5f)]
    public float dampFactor = 0.75f;

	// Update is called once per frame
	void Update ()
	{
	    Vector3 vToTarget = followTarget.transform.position - transform.position;

	    transform.position += vToTarget * dampFactor * Time.deltaTime;
	}
}
