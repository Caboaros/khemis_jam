using _Game.Scripts.Player;
using Blazewing.DataEvent;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.HUD
{
    public class HUD_PlayerHearts : MonoBehaviour
    {
        [SerializeField] private RectTransform heartTransform;
        [SerializeField] private Image heartFill;
        [SerializeField] private TextMeshProUGUI heartsAmountText;
    
        private void OnEnable()
        {
            DataEvent.Register<PlayerHeartsStruct>(OnPlayerHeartsChanged);
        }

        private void OnDisable()
        {
            DataEvent.Unregister<PlayerHeartsStruct>(OnPlayerHeartsChanged);
        }

        private void OnPlayerHeartsChanged(PlayerHeartsStruct eventData)
        {
            heartFill.DOFillAmount((float)eventData.amount / PlayerController.Instance.Life.maxHearts, .25f);
            
            heartTransform.DOScale(1.2f, .15f).SetEase(Ease.OutBack).onComplete = () =>
            {
                heartTransform.DOScale(1f, .5f).SetEase(Ease.OutBack);
            };

            heartsAmountText.text = eventData.amount.ToString();
        }
    }
}
