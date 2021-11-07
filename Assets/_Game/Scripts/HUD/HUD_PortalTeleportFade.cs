using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Blazewing.DataEvent;

public class HUD_PortalTeleportFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    private void OnEnable()
    {
        DataEvent.Register<OnTeleportFadeOutStart>(DoFadeOut);
        DataEvent.Register<OnTeleportFadeInStart>(DoFadeIn);
    }

    private void OnDisable()
    {
        DataEvent.Unregister<OnTeleportFadeOutStart>(DoFadeOut);
        DataEvent.Unregister<OnTeleportFadeInStart>(DoFadeIn);
    }

    private void DoFadeOut(OnTeleportFadeOutStart eventData)
    {
        fadeImage.raycastTarget = true;
        fadeImage.DOFade(1, 0.5f).OnComplete(() => DataEvent.Notify(new OnTeleportFadeOutEnd()));
    }

    private void DoFadeIn(OnTeleportFadeInStart eventData)
    {
        fadeImage.raycastTarget = false;
        fadeImage.DOFade(0, 0.5f).OnComplete(() => DataEvent.Notify(new OnTeleportFadeInEnd()));
    }
}

public struct OnTeleportFadeOutStart { }
public struct OnTeleportFadeOutEnd { }
public struct OnTeleportFadeInStart { }
public struct OnTeleportFadeInEnd { }