using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    [SerializeField] private SO_MapData mapData;
    [Space] [SerializeField] private Grid grid;

    private Dictionary<Vector3Int, TileBehaviour> _mapTiles = new Dictionary<Vector3Int, TileBehaviour>();

    private static MapManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        StartCoroutine(InstantiateMap());
    }

    private IEnumerator InstantiateMap()
    {
        WaitForSeconds wait = new WaitForSeconds(.01f);
        
        int totalCells = mapData.columnsAmount * mapData.rowsAmount;
        for (int i = 0; i < mapData.columnsAmount; i++)
        {
            for (int j = 0; j < mapData.rowsAmount; j++)
            {
                if (Random.value <= mapData.holePercentage)
                {
                    totalCells--;
                    continue;
                }
                
                Vector3Int gridPosition = new Vector3Int(i, j, 0);
                TileBehaviour newTile = Instantiate(mapData.tilePrefab, grid.transform);
                _mapTiles.Add(gridPosition, newTile);
                newTile.Init(grid.GetCellCenterLocal(gridPosition), gridPosition, totalCells);
                totalCells--;
                yield return wait;
            }
        }
    }

    public static TileBehaviour GetNearestCellAtPosition(Vector3 position)
    {
        position.z = 0.5f;
        Vector3Int pos = _instance.grid.WorldToCell(position);

        return _instance._mapTiles.ContainsKey(pos) ? _instance._mapTiles[pos] : null;
    }

    public static TileBehaviour GetTileByCellPosition(Vector3Int cellPosition)
    {
        if (_instance._mapTiles.TryGetValue(cellPosition, out TileBehaviour tile))
        {
            return tile;
        }

        throw new Exception($"Tile {cellPosition} não encontrado!");
    }

    public static TileBehaviour GetRandomAvailableTile()
    {
        List<TileBehaviour> tiles = new List<TileBehaviour>();
        foreach (var tile in _instance._mapTiles)
        {
            if (!tile.Value.isAvailable) continue;

            tiles.Add(tile.Value);
        }

        return tiles.Count == 0 ? null : tiles[Random.Range(0, tiles.Count)];
    }
}