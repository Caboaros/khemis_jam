using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    [ReadOnly] public bool isAvailable;
    [Space]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer groundSpriteRenderer;

    [HideInInspector] public Vector3Int cellPosition;
    [HideInInspector] public int orderInLayer;

    public void Init(Vector3 worldPosition, Vector3Int cellPosition, int orderInLayer)
    {
        this.cellPosition = cellPosition;
        this.orderInLayer = orderInLayer;
        
        transform.position = worldPosition;
        spriteRenderer.sortingOrder = orderInLayer;
        groundSpriteRenderer.sortingOrder = orderInLayer + 1;

        isAvailable = true;
        
        spriteRenderer.transform.localScale = Vector3.zero;
        spriteRenderer.transform.DOScale(1, .25f).SetEase(Ease.OutBack);
    }

    public void SetHighlightStatus(bool status)
    {
        //spriteRenderer.color = status ? Color.yellow : Color.white;
        spriteRenderer.transform.DOLocalMoveY(status ? 0.25f : 0, .25f);
    }
}
