using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFader : MonoBehaviour {

    CanvasGroup canvasGroup;

    CoroutineManager.Item fadeSequence = new CoroutineManager.Item();

    public MinMaxFloat alpha = new MinMaxFloat(0.0f, 1f, 0f);
    public float fadeDuration = 0.5f;

    public bool fadeOutOnAwake = false;

    public bool unscaledTime = false;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (fadeOutOnAwake) FadeOut();
    }

    public void FadeIn()
    {
        fadeSequence.value = FadeInSequence();
    }

    public void FadeOut()
    {
        fadeSequence.value = FadeOutSequence();
    }
    
    IEnumerator FadeInSequence()
    {
        float timeElapsed = 0f;
        float duration = fadeDuration * (1 - alpha.percentage);

        float originalAlpha = canvasGroup.alpha;

        while (timeElapsed < duration)
        {
            timeElapsed += (unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
            alpha.value = originalAlpha + (timeElapsed / duration * (1 - originalAlpha));

            canvasGroup.alpha = alpha.value;

            yield return null;
        }
    }

    IEnumerator FadeOutSequence()
    {
        float timeElapsed = 0f;
        float duration = fadeDuration * alpha.percentage;

        float originalAlpha = canvasGroup.alpha;

        while (timeElapsed < duration)
        {
            timeElapsed += (unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);

            alpha.value = (originalAlpha - timeElapsed / duration);

            canvasGroup.alpha = alpha.value;

            yield return null;
        }
    }
}
