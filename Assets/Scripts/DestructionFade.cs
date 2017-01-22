using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class DestructionFade : MonoBehaviour {

    [Range(0,1)]
    public float Max;
    Image fadeImage;
	// Use this for initialization
	void Start () {
        fadeImage = GetComponent<Image>();
        GameController.instance.Destruction.OnValueChanged += OnDestuctionUpdated;
        OnDestuctionUpdated();

    }
	
    void OnDestuctionUpdated()
    {
        var destruction = GameController.instance.Destruction.value;

        fadeImage.DOFade(Mathf.Lerp(0, Max, destruction), 0.1f);
    }
}
