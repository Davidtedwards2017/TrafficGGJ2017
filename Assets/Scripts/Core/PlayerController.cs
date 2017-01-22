using UnityEngine;
using System.Collections;

[RequireComponent(typeof(tk2dAnimatedSprite))]
public class PlayerController : MonoBehaviour {

    tk2dAnimatedSprite SquidAnimator;

    public TenticleAnimator NorthFacingTenticle;
    public TenticleAnimator EastFacingTenticle;
    public TenticleAnimator SouthFacingTenticle;
    public TenticleAnimator WestFacingTenticle;

    void Awake()
    {
        SquidAnimator = GetComponent<tk2dAnimatedSprite>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
