using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class AltarLine : MonoBehaviour
{
    public float activationDuration = 3;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void ActivateLine()
    {
        DOTween.To(GetValue, SetValue, -2f, activationDuration).OnComplete(delegate 
        {
            //Ativar HUB aqui
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
