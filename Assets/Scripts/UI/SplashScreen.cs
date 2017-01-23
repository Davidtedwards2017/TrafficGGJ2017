using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class SplashScreen : Singleton<SplashScreen> {

    public float FadeDuration = 1.0f;
    public float HoldDuration = 2.0f;
    public CanvasGroup group1;
    public CanvasGroup group2;

    void Awake()
    {
        group1.alpha = 1;
        group2.alpha = 0;
    }


    public IEnumerator DisplaySequence()
    {
        yield return new WaitForSeconds(HoldDuration);
        group1.DOFade(0, FadeDuration);
        group2.DOFade(1, FadeDuration);
        yield return new WaitForSeconds(FadeDuration);
        yield return new WaitForSeconds(HoldDuration);
        yield return group2.DOFade(0, FadeDuration);
    }

}
