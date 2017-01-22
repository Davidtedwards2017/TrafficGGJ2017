using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class UiController : MonoBehaviour {

    public CanvasGroup Group;

    protected virtual void Awake()
    {
        Group = GetComponent<CanvasGroup>();
    }

    protected void Show()
    {
        Group.alpha = 1;
    }

    protected void Hide()
    {
        Group.alpha = 0;
    }
}
