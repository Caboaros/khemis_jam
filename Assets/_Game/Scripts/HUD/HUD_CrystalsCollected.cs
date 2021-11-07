using System;
using Blazewing.DataEvent;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.HUD
{
    public class HUD_CrystalsCollected : MonoBehaviour
    {
        [SerializeField] private RectTransform crystalTransform;
        [SerializeField] private TextMeshProUGUI crystalsAmountText;

        private void OnEnable()
        {
            DataEvent.Register<CrystalsCollected>(OnCrystalCollected);
        }

        private void OnDisable()
        {
            DataEvent.Unregister<CrystalsCollected>(OnCrystalCollected);
        }

        private void OnCrystalCollected(CrystalsCollected eventData)
        {
            crystalTransform.DOScale(1.2f, .15f).SetEase(Ease.OutBack).onComplete = () =>
            {
                crystalTransform.DOScale(1f, .5f).SetEase(Ease.OutBack);
            };
            
            crystalsAmountText.text = eventData.amount.ToString();
        }
    }

    [Serializable]
    public struct CrystalsCollected
    {
        public int amount;

        public CrystalsCollected(int amount)
        {
            this.amount = amount;
        }
    }
}
