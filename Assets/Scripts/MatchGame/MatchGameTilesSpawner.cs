using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MatchGameTilesSpawner : MonoBehaviour
{
    private int _tilesAmount = 20;

    [SerializeField]
    private MatchGameManager _gameManager;

    [SerializeField]
    private MatchTilesConfig config;

    [SerializeField]
    private GameObject _parent;

    [SerializeField]
    private MatchGameTile _prefab;

    private List<MatchGameTile> _matchTilesPrefabPool = new List<MatchGameTile>();
    private List<MatchTileData> _matchTileDatas = new List<MatchTileData>();

    private void Awake()
    {
        _gameManager.SetTilesAmount(_tilesAmount);
        InitTilesOnBoard();
        RefreshBoard();
    }
    public void RefreshBoard()
    {
        foreach (var item in config.Collectables)
        {
            _matchTileDatas.Add(item);
            _matchTileDatas.Add(item);
        }

        for (int i = 0; i < _tilesAmount; i++)
        {
            var data = _matchTileDatas[Random.Range(0, _matchTileDatas.Count)];
            _matchTilesPrefabPool[i].ID = data.Id;
            _matchTilesPrefabPool[i].FrontSideImage.sprite = data.Sprite;
            _matchTilesPrefabPool[i].GameManager = _gameManager;
            _matchTileDatas.Remove(data);
        }
    }

    private void InitTilesOnBoard()
    {
        for(int i = 0; i < _tilesAmount; i++)
        {
            var child = Instantiate(_prefab, _parent.transform);
            _matchTilesPrefabPool.Add(child);
        }        
    }
}
