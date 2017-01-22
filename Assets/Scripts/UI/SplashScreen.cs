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
    }


    public IEnumerator DisplaySequence()
    {
        yield return group.DOFade(1, 0.1f);
        yield return new WaitForSeconds(HoldDuration);
        yield return group.DOFade(0, FadeDuration);
    }

}
