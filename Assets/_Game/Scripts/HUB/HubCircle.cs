using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class HubCircle : MonoBehaviour
{
    public HubPuzzle hubPuzzle;
    public bool isFocused;

    [SerializeField]
    private bool randomizeRotation;

    [SerializeField]
    private Sprite circleOff;
    [SerializeField]
    private Sprite circleOn;

    private SpriteRenderer spriteRenderer;

    [ReadOnly, SerializeField]
    private int currentRotationIndex;
    [ReadOnly, SerializeField]
    private List<int> possibleRotations = new List<int>();

    public bool IsCorrect
    {
        get
        {
            return currentRotationIndex == 0;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = circleOff;

        if(randomizeRotation)
        {
            SetRandomRotation();
        }
    }

    private void SetRandomRotation()
    {
        for (int i = 0; i < 15; i++)
        {
            possibleRotations.Add(i * 25);
        }

        currentRotationIndex = Random.Range(1, 15);

        transform.localEulerAngles = Vector3.forward * possibleRotations[currentRotationIndex];
    }

    private void Update()
    {
        if (!isFocused)
            return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateCircle(1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RotateCircle(-1);
        }
    }

    private void RotateCircle(int direction)
    {
        currentRotationIndex += direction;

        if(currentRotationIndex == -1)
        {
            currentRotationIndex = possibleRotations.Count - 1;
        }
        else if(currentRotationIndex == possibleRotations.Count)
        {
            currentRotationIndex = 0;
        }

        transform.localEulerAngles = Vector3.forward * possibleRotations[currentRotationIndex];

        CheckActivation();
    }

    private void CheckActivation()
    {
        if (IsCorrect)
        {
            spriteRenderer.sprite = circleOn;

            isFocused = false;

            hubPuzzle.OnCompleteOneCircle();
        }
        else
        {
            spriteRenderer.sprite = circleOff;
        }
    }

    public void ActivateManually()
    {
        CheckActivation();
    }
}
