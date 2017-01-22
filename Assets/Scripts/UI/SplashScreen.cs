using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class SplashScreen : Singleton<SplashScreen> {

    public float FadeDuration = 1.0f;
    public float HoldDuration = 2.0f;
    Image image;
    
    void Awake()
    {
        image = GetComponent<Image>();
    }


    public IEnumerator DisplaySequence()
    {
        yield return image.DOFade(1, 0);
        yield return new WaitForSeconds(HoldDuration);
        yield return image.DOFade(0, FadeDuration);
    }

}
