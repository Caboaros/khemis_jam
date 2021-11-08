using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Blazewing.DataEvent;
using System;

public class HUD_FadeManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    private void OnEnable()
    {
        DataEvent.Register<DoFadeOut>(DoFadeOut);
        DataEvent.Register<DoFadeIn>(DoFadeIn);
    }

    private void OnDisable()
    {
        DataEvent.Unregister<DoFadeOut>(DoFadeOut);
        DataEvent.Unregister<DoFadeIn>(DoFadeIn);
    }

    private void DoFadeOut(DoFadeOut eventData)
    {
        fadeImage.raycastTarget = true;
        fadeImage.DOFade(1, 1).OnComplete(() => eventData.onEndFade?.Invoke());
    }

    private void DoFadeIn(DoFadeIn eventData)
    {
        fadeImage.raycastTarget = false;
        fadeImage.DOFade(0, 1).OnComplete(() => eventData.onEndFade?.Invoke());
    }
}

public struct DoFadeOut
{
    public Action onEndFade;
}

public struct DoFadeIn
{
    public Action onEndFade;
}