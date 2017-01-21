using UnityEngine;
using System.Collections;

public class ConstrainedPanFollowBehavior : MonoBehaviour
{

    public GameObject followObject;

    public Vector3 followObjectStartPos;

    public MinMaxFloat objectPanRange = new MinMaxFloat(-10f, 10f, 0f);

	public MinMaxFloat panRange = new MinMaxFloat(-2f, 2f, 0f);

    private Vector3 startPos;

    private float offset;



    [Range(0f, 1f)]
    public float interpolation = 0.6f;

    void Awake()
    {
        //startPos = transform.position;
        followObjectStartPos = followObject.transform.position;
    }

	// Update is called once per frame
	void Update ()
	{
        float difference = followObject.transform.position.x - followObjectStartPos.x;
	    objectPanRange.value = difference;

	    panRange.SetToPercent(objectPanRange.percentage);

        //Debug.Log(panRange.value);

        offset = panRange.value;

	    float targetPosX = followObjectStartPos.x + offset;

        Vector3 vToTargetPos = new Vector3(targetPosX, transform.position.y, transform.position.z) - transform.position;

	    transform.position += vToTargetPos * interpolation * Time.deltaTime;

	}
}
