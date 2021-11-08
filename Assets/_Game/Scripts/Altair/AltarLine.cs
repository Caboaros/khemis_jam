using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class AltarLine : MonoBehaviour
{
    [SerializeField]
    private HubPuzzle hubPuzzle;

    public int runeIndex;
    public float activationDuration = 4;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        ActivateLine();
    }

    public void ActivateLine()
    {
        DOTween.To(GetValue, SetValue, -1.8f, activationDuration).OnComplete(delegate 
        {
            hubPuzzle.ActivateRune(runeIndex - 1);
        });
    }

    private void SetValue(float pNewValue)
    {
        lineRenderer.material.SetFloat("_AlphaSlider", pNewValue);
    }

    private float GetValue()
    {
        return lineRenderer.material.GetFloat("_AlphaSlider");
    }
}
