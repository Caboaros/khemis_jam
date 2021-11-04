using UnityEngine;

[CreateAssetMenu(menuName = "Map Data", fileName = "New Map")]
public class SO_MapData : ScriptableObject
{
    public TileBehaviour tilePrefab;
    [Space]
    public int columnsAmount;
    public int rowsAmount;
    [Space]
    [Range(0f, 1f)] public float holePercentage = .5f;
}
