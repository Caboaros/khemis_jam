using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPuzzle : MonoBehaviour
{
    [Header("Runes:")]
    public HubRunes[] hubRunes;

    [SerializeField, ReadOnly]
    private int runesActivated = 0;

    [Header("Circles:")]
    public HubCircle[] hubCircles;

    [SerializeField, ReadOnly]
    private int currentFocusedCircle = -1;

    public void ActivateRune(int index)
    {
        hubRunes[index].ActivateRune();

        runesActivated++;

        if(runesActivated == 4)
        {
            ActivatePuzzle();
        }
    }

    private void ActivatePuzzle()
    {
        currentFocusedCircle = -1;

        FocusNextCircle();
    }

    private void FocusNextCircle()
    {
        currentFocusedCircle++;

        if(currentFocusedCircle == 2)
        {
            hubCircles[currentFocusedCircle].ActivateManually();

            OnFinishPuzzle();
        }
        else if(currentFocusedCircle < 2)
        {
            hubCircles[currentFocusedCircle].isFocused = true;
        }
    }

    public void OnCompleteOneCircle()
    {
        FocusNextCircle();
    }

    private void OnFinishPuzzle()
    {
        
    }

}
