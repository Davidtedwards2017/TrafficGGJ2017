using UnityEngine;
using System.Collections;

public class Wiggle : MonoBehaviour {

    [Range(0,1)]
    public float Intensity;

    public float XStr = 1f;
    public float XSpeed = 1f;
    public float XtimingOffset = 0.0f;

    public float YStr = 1f;
    public float YSpeed = 1f;
    public float YtimingOffset = 0.0f;

    //private float count;

    private Vector3 startingPosition;
    private Vector3 offset;

    // Use this for initialization
	void Start () {
        startingPosition = transform.localPosition;
	}

    // Update is called once per frame
    void Update()
    {
        //count += Time.deltaTime;

        offset.x = Mathf.Sin(Time.time * (XSpeed * Intensity) + XtimingOffset) * XStr * Intensity;
        offset.y = Mathf.Sin(Time.time * (YSpeed * Intensity) + YtimingOffset) * YStr * Intensity;

        if(offset.sqrMagnitude > 0.001f)
        {
            transform.localPosition = startingPosition + offset;

        } else
        {
            transform.localPosition = startingPosition;
        }
    }
}
