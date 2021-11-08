using Blazewing.DataController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.HUD;
using Blazewing.DataEvent;
using _Game.Scripts.Player;
using TMPro;

public class PortalTree : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI costNumber;
    public GameObject interactionFeedback;

    [Space]
    public Sprite treeOn;
    public Sprite treeOff;

    [Space]
    [SerializeField]
    private Transform teleportDestination;

    public int priceToActivate;
    private bool isActivated;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = treeOff;
        costNumber.text = "0/" + priceToActivate;
    }

    private void Activate()
    {
        isActivated = true;
        costNumber.text = priceToActivate + "/" + priceToActivate;
        spriteRenderer.sprite = treeOn;
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
        interactionFeedback.SetActive(true);
    }

    public void OnPlayerExit()
    {
        interactionFeedback.SetActive(false);
    }
}
