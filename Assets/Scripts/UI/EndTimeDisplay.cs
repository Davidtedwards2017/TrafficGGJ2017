using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class EndTimeDisplay : MonoBehaviour {

    public Text minText;
    public Text secText;
    	
	// Update is called once per frame
	void Update () {
        UpdateTime(TimeSpan.FromSeconds(GameController.instance.GameTime));

	}

    void UpdateTime(TimeSpan time)
    {
        var mins = (int) time.TotalMinutes;
        var seconds = (int)time.TotalSeconds;

        if (mins < 1)
        {
            minText.text = "";
            secText.text = string.Format("{0}s", seconds);
        }
        else
        {
            minText.text = string.Format("{0}m", mins );
            secText.text = string.Format("{0}s", seconds - (60 * mins));
        }
    }
}
