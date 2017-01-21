using UnityEngine;
using System.Collections;

public class HoveringObjectBehavior : MonoBehaviour
{
    public float amplitude = 1f;          //Set in Inspector 
    public float speed = 1f;                  //Set in Inspector 
    private float tempVal;
    private Vector3 tempPos;

    //IEnumerator hoverRoutine = null;

    void Start()
    {
        tempVal = transform.position.y;
    }

    void Update()
    {
        tempPos.x = transform.position.x;
        tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
        tempPos.z = transform.position.z;

        transform.position = tempPos;
    }
}