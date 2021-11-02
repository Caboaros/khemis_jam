using System;
using UnityEngine;

public class TotemBehaviour : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector2 _diff;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        _diff = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition) - _diff;
    }
}
