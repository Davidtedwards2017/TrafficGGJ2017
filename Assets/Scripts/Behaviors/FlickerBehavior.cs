using UnityEngine;
using System.Collections;

public class FlickerBehavior : MonoBehaviour 
{
	public Color[] c;
	//Color color;

	public MinMaxFloat flickerRate = new MinMaxFloat (0.1f, 20f, 1f);
	private float t = 0;

	public Renderer renderer;

	public bool proximityFlicker = false;
	public float proximityTriggerDistance = 10.0f;
	public GameObject proximityTarget = null;
	private float proximity;

    private int currentIndex = 0;

    private Color previousColor;
    private Color nextColor;

	void Start()
	{
		//color = c[0];
		//maxFlickerRate = flickerRate;
		if(!renderer) renderer = GetComponent<Renderer>();
	    previousColor = c[0];
	    nextColor = c[1];
	}
	
	void Update()
	{
		// if we have a proximity target, modify the flicker rate to a max, based on closeness to the target
		if(proximityFlicker == true && proximityTarget)
		{
			proximity = Vector3.Distance(transform.position, proximityTarget.transform.position);
			flickerRate.SetToPercent(AICore.IsItMin(proximity, 0.0f, proximityTriggerDistance));

		}
		
		// flicker the color
		renderer.material.color = Color.Lerp(previousColor, nextColor, t);

        t += flickerRate.value * Time.deltaTime;

	    if (t >= 1)
	    {
	        currentIndex++;
	        if (currentIndex > c.Length - 1) currentIndex = 0;
	        previousColor = nextColor;
	        nextColor = c[currentIndex];
	        t = 0;
	    }

	}
}
