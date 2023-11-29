using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchGameTile : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    public Button Button => _button;

    [SerializeField]
    private Image _frontSideImage;
    public Image FrontSideImage { get => _frontSideImage; set { _frontSideImage = value; } }

    [SerializeField]
    private int _id;
    public int ID { get => _id; set { _id = value; } }

    private MatchGameManager _gameManager;
    public MatchGameManager GameManager { set { _gameManager = value; } }
    
    private void Start()
    {
        _button.onClick.AddListener(OnTileClick);
        _gameManager.RefreshMatchTiles += RefreshTile;
    }

    private void OnTileClick()
    {
        _gameManager.CheckMatch(this);
    }

    private void RefreshTile()
    {
        _button.gameObject.SetActive(true);
    }
}
