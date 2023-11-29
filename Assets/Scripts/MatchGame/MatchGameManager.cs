using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class MatchGameManager : MonoBehaviour
{
    [SerializeField]
    private float _initialTime;

    private MatchGameTile _pickedTile;

    [SerializeField]
    private TextMeshProUGUI _timer;

    private int _tilesAmount;
    private int _tilesLeft;

    private Coroutine _timerCoroutine;

    public void SetTilesAmount(int amount)
    {
        _tilesAmount = amount;
        _tilesLeft = _tilesAmount;
    }
    public void CheckMatch(MatchGameTile tile)
    {
        if (_timerCoroutine == null)
        {
            _timerCoroutine = StartCoroutine(TimerRoutine());
        }

        if(_pickedTile == null)
        {
            _pickedTile = tile;
            _pickedTile.Button.gameObject.SetActive(false);
        }           
        else
        {
            if(_pickedTile.ID == tile.ID)
            {
                tile.Button.gameObject.SetActive(false);
                _pickedTile = null;
                _tilesAmount -= 2;
            }
            else
            {
                _pickedTile.Button.gameObject.SetActive(true);
                _pickedTile = null;
            }
        }
    }

    private IEnumerator TimerRoutine()
    {
        yield return null;
    }
}
