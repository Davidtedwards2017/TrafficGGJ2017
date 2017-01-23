using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class SplashScreen : Singleton<SplashScreen> {

    public float FadeDuration = 1.0f;
    public float HoldDuration = 2.0f;
    CanvasGroup group;
    
    void Awake()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 1;
    }


    public IEnumerator DisplaySequence()
    {
        yield return new WaitForSeconds(HoldDuration);
        yield return group.DOFade(0, FadeDuration);
    }

}
