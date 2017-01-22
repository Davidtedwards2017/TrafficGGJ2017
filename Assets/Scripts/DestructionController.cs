using UnityEngine;
using System.Collections;
using DG.Tweening;


public class DestructionController : MonoBehaviour {

    public tk2dSprite[] Fires;

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
            var fire = Fires[i];
            if(i < count)
            {
                fire.DOFade(1, 0.1f);
            }
            else
            {
                fire.DOFade(0, 0.1f);
            }
        }
    }

}
