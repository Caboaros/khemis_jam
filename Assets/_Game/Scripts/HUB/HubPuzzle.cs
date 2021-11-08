using _Game.Scripts.Player;
using Blazewing.DataEvent;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubPuzzle : MonoBehaviour, IInteractable
{
    public HubPuzzle otherHub;

    public GameObject interactionFeedback;

    [Header("Runes:")]
    public HubRunes[] hubRunes;

    [ReadOnly]
    public int runesActivated = 0;

    [Header("Circles:")]
    public HubCircle[] hubCircles;

    [SerializeField, ReadOnly]
    private int currentFocusedCircle = -1;

    private bool isPuzzleActive;

    private void Awake()
    {
        currentFocusedCircle = -1;
        isPuzzleActive = false;
    }

    public void ActivateRune(int index)
    {
        hubRunes[index].ActivateRune();

        runesActivated++;

        if(otherHub.runesActivated != runesActivated)
        {
            otherHub.ActivateRune(index);
        }

        if(runesActivated == 4)
        {
            ActivatePuzzle();
        }
    }

    private void ActivatePuzzle()
    {
        currentFocusedCircle = -1;

        isPuzzleActive = true;
        otherHub.isPuzzleActive = true;

        FocusNextCircle();
    }

    private void FocusNextCircle()
    {
        currentFocusedCircle++;

        SetFocused(true);
    }

    private void SetFocused(bool focus)
    {
        if (currentFocusedCircle == 2)
        {
            hubCircles[currentFocusedCircle].ActivateManually();

            OnFinishPuzzle();
        }
        else if (currentFocusedCircle < 2)
        {
            hubCircles[currentFocusedCircle].isFocused = focus;
        }
    }

    public void OnCompleteOneCircle()
    {
        FocusNextCircle();
    }

    private void OnFinishPuzzle()
    {
        DoFadeOut endGameFadeOut = new DoFadeOut()
        {
            onEndFade = () => GameOverController.LoadScene(true)
        };

        DataEvent.Notify<DoFadeOut>(endGameFadeOut);
    }

    public void OnPlayerEnter()
    {
        if (!isPuzzleActive)
            return;

        PlayerController.Instance.Movement.CanMove = false;
        PlayerController.Instance.transform.position = hubCircles[2].transform.position;
        PlayerController.Instance.Movement._rigidbody.velocity = Vector2.zero;

        SetFocused(true);
        interactionFeedback.SetActive(true);
    }

    public void OnPlayerExit()
    {
        if (!isPuzzleActive)
            return;

        SetFocused(false);
        interactionFeedback.SetActive(false);
    }

    public void OnInteract()
    {
        
    }
}
