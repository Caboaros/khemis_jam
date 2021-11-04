using _Game.Scripts.Player;
using Blazewing.DataEvent;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.HUD
{
    public class HUD_PlayerHearts : MonoBehaviour
    {
        [SerializeField] private RectTransform heartTransform;
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
            heartTransform.DOScale(1.2f, .3f).SetEase(Ease.InBack).onComplete = () =>
            {
                heartTransform.DOScale(1f, .5f).SetEase(Ease.OutBack);
            };
            
            heartsAmountText.text = eventData.amount.ToString();
        }
    }
}
