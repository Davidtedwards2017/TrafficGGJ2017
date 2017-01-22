using UnityEngine;
using System.Collections;

public class DestructionController : MonoBehaviour {

    public ParticleSystem[] Fires;

	// Use this for initialization
	void Start () {
        
        GameController.instance.Destruction.OnValueChanged += OnDestructionChanged;
	}
	
    public void OnDestructionChanged()
    {
        var percent = GameController.instance.Destruction.percentage;

        var count = (int)Mathf.Lerp(0, Fires.Length, percent);

        for(int i = 0; i < Fires.Length; i ++)
        {
            var particle = Fires[i];
            if(i < count)
            {
                particle.Play();
            }
            else
            {
                particle.Stop();
            }
        }
    }

}
