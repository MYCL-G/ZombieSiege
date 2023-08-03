using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    CanvasGroup canvasGroup;
    float alphaSpeed = 10;
    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }
    protected virtual void Start()
    {
        Init();
    }
    private void Update()
    {

    }
    public abstract void Init();
    public virtual void ShowMe()
    {
        StartCoroutine(AlphaShow());
    }
    public virtual void HideMe(UnityAction callBack)
    {
        StartCoroutine(AlphaHide(callBack));

    }
    IEnumerator AlphaShow()
    {
        canvasGroup.alpha = 0;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            yield return 0;
        }
        canvasGroup.alpha = 1;
    }
    IEnumerator AlphaHide(UnityAction callBack)
    {
        canvasGroup.alpha = 1;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            yield return 0;
        }
        canvasGroup.alpha = 0;
        callBack?.Invoke();
    }
}
