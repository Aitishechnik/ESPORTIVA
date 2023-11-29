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

    private List<MatchTileData> _matchTileDatas = new List<MatchTileData>();

    private void Awake()
    {       
        foreach(var item in config.Collectables)
        {
            _matchTileDatas.Add(item);
            _matchTileDatas.Add(item);
        }

        _tilesAmount = _matchTileDatas.Count;
        _gameManager.SetTilesAmount(_tilesAmount);

        for (int i = 0; i < _tilesAmount; i++)
        {
            var child = Instantiate(_prefab, _parent.transform);
            var data = _matchTileDatas[Random.Range(0, _matchTileDatas.Count)];
            child.ID = data.Id;
            child.FrontSideImage.sprite = data.Sprite;
            child.GameManager = _gameManager;
            _matchTileDatas.Remove(data);
        }
    }
}
