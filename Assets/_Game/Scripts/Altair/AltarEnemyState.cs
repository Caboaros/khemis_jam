using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AltarEnemyState
{
    private static GameObject currentSpawnedEffect;
    public static GameObject CurrentSpawnedEffect
    {
        get
        {
            return currentSpawnedEffect;
        }
        set
        {
            if(currentSpawnedEffect != null)
            {
                Object.Destroy(currentSpawnedEffect);
            }

            currentSpawnedEffect = value;
        }
    }

    public Sprite stateSprite;
    public GameObject stateEffects;
    public AudioClip onEnterStateSound;

    public void OnEnterState(SpriteRenderer spriteRendererSource, AudioSource audioSource, Transform effectsAnchor)
    {
        spriteRendererSource.sprite = stateSprite;

        if(onEnterStateSound)
            audioSource.PlayOneShot(onEnterStateSound);

        if(stateEffects)
            CurrentSpawnedEffect = Object.Instantiate(stateEffects, effectsAnchor);
    }
}
