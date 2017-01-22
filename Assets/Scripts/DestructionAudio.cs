using UnityEngine;
using System.Collections;

public class DestructionAudio : MonoBehaviour {

    [Range(0, 1)]
    public float Max;
    public AudioSource destructionAudio;
	

    
    // Use this for initialization
	void Start () {
        GameController.instance.Destruction.OnValueChanged += OnDestructionChanged;
    }
	
    public void OnDestructionChanged()
    {
          
        var destruction = GameController.instance.Destruction.value;
        destructionAudio.volume = Mathf.Lerp(0, Max, destruction);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
