using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class DestructionFade : MonoBehaviour {

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

        fadeImage.DOFade(destruction, 0.1f);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
