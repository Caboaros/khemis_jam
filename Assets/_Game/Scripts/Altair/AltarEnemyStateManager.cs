using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarEnemyStateManager : MonoBehaviour
{
    [Header("Visuals:")]
    [SerializeField]
    private SpriteRenderer altarSpriteRenderer;
    [SerializeField]
    private AudioSource altarAudioSource;
    [SerializeField]
    private Transform altarEffectsAchor;

    [Space(2)]
    [SerializeField]
    private SpriteRenderer runeSpriteRenderer;
    [SerializeField]
    private Sprite runeSprite;

    [Header("States:")]
    [SerializeField]
    private AltarEnemyState altarIdle;
    [SerializeField]
    private AltarEnemyState altarInProgress;
    [SerializeField]
    private AltarEnemyState altarComplete;


    public void OnAltarInitialization()
    {
        altarIdle.OnEnterState(altarSpriteRenderer, altarAudioSource, altarEffectsAchor);
    }

    public void OnAltarActivate()
    {
        altarIdle.OnExitState();

        altarInProgress.OnEnterState(altarSpriteRenderer, altarAudioSource, altarEffectsAchor);
    }

    public void OnAltarComplete()
    {
        altarInProgress.OnExitState();

        altarComplete.OnEnterState(altarSpriteRenderer, altarAudioSource, altarEffectsAchor);

        runeSpriteRenderer.sprite = runeSprite;
        runeSpriteRenderer.enabled = true;
    }
}
