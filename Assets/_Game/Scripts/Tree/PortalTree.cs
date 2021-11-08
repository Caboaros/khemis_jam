using Blazewing.DataController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.HUD;
using Blazewing.DataEvent;
using _Game.Scripts.Player;

public class PortalTree : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform teleportDestination;

    public int priceToActivate;
    private bool isActivated;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Activate()
    {
        isActivated = true;
    }

    public void OnInteract()
    {
        if (isActivated)
        {
            DataEvent.Register<OnTeleportFadeOutEnd>(TeleportPlayer);
            DoFadeOut();
        }
        else
        {
            int currentCrystalAmount = DataController.Get<CrystalsCollected>().amount;

            if (currentCrystalAmount >= priceToActivate)
            {
                Activate();

                CrystalsCollected crystalsRemaining = new CrystalsCollected(currentCrystalAmount - priceToActivate);

                DataEvent.Notify(crystalsRemaining);

                DataController.Add(crystalsRemaining);
            }
        }
    }

    private void DoFadeOut()
    {
        DataEvent.Notify(new OnTeleportFadeOutStart());
    }

    private void TeleportPlayer(OnTeleportFadeOutEnd _event)
    {
        DataEvent.Unregister<OnTeleportFadeOutEnd>(TeleportPlayer);
        PlayerController.Instance.transform.position = teleportDestination.position;
        DataEvent.Notify(new OnTeleportFadeInStart());
    }

    public void OnPlayerEnter()
    {
        
    }

    public void OnPlayerExit()
    {
        
    }
}
