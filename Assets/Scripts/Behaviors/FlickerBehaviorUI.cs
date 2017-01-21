using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlickerBehaviorUI : MonoBehaviour 
{
	public Color[] c;
	//Color color;
	
	public Image image;    
    public Text text;

	public float flickerRate = 1.0f;
	public float maxFlickerRate = 10.0f;
	private MinMaxFloat t = new MinMaxFloat();
	bool decreasingState = false; // True when the flicker is returning to minimum after hitting maximum

    public FlickerBehaviorUI[] subFlickers; // Flicker behaviors that should be enabled, 

	void Awake() {
		if(image == null) image = GetComponent<Image>();
	}
	
	void Update() {

		// flicker the color
        Color lerpColor = Color.Lerp(c[0], c[1], t.value);

        if (image) image.color = lerpColor;        
        if (text) text.color = lerpColor;

		if(!decreasingState)
			t.value += flickerRate * Time.unscaledDeltaTime;
		else
			t.value -= flickerRate * Time.unscaledDeltaTime;
		if(t.percentage>=1)
			decreasingState = true;
		if(t.percentage<=0)
			decreasingState = false;
	}

    /// <summary>
    /// Resets the image's color to the first color in the array, or an optional index.
    /// </summary>
    /// <param name="index"></param>
    public void ResetImageColor(int index = 0)
    {
        if (index > c.Length - 1) return;
        image.color = c[index];

        for (int n = 0; n < subFlickers.Length; n++)
        {
            subFlickers[n].ResetImageColor();
        }
    }

    void OnEnable()
    {
        for (int n = 0; n < subFlickers.Length; n++)
        {
            subFlickers[n].enabled = true;
        }
    }

    void OnDisable()
    {
        for (int n = 0; n < subFlickers.Length; n++)
        {
            subFlickers[n].enabled = false;
        }
    }
}
