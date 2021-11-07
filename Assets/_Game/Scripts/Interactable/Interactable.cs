using _Game.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private IInteractable interactable;
    private bool isPlayerNearby;

    private void Awake()
    {
        interactable = GetComponent<IInteractable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponentInParent<PlayerController>();

        if (playerController != null)
        {
            isPlayerNearby = true;
            interactable.OnPlayerEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponentInParent<PlayerController>();

        if(playerController != null)
        {
            isPlayerNearby = false;
            interactable.OnPlayerExit();
        }
    }

    private void Update()
    {
        if (isPlayerNearby)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                interactable.OnInteract();
            }
        }
    }
}

public interface IInteractable
{
    void OnPlayerEnter();
    void OnPlayerExit();
    void OnInteract();
}
