using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubRunes : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite runeOff;
    [SerializeField]
    private Sprite runeOn;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = runeOff;
    }

    public void ActivateRune()
    {
        spriteRenderer.sprite = runeOn;
    }
}
