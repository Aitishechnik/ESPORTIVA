using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class MatchGameManager : MonoBehaviour
{
    private SaveSystem _saveSystem = new SaveSystem();
    private int _bestSatge;

    [SerializeField]
    private float _initialTime;

    private float _stageTime;

    [SerializeField]
    private MatchGameTilesSpawner _spawner;

    private MatchGameTile _pickedTile;

    [SerializeField]
    private TextMeshProUGUI _timer;

    [SerializeField]
    private TextMeshProUGUI _bestStageResultText;

    private int _stage = 1;

    private int _tilesAmount;
    private int _tilesLeft;

    private Coroutine _timerCoroutine;

    private void Awake()
    {
        _bestStageResultText.text = BestStageString(_saveSystem.Load());
        _stageTime = _initialTime;
        _timer.text = InitialTimerText();
    }

    private string InitialTimerText()
    {
        return $"Stage {_stage}\n\nTimer:  {_stageTime.ToString("F2")}";
    }

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
                _tilesLeft -= 2;
            }
            else
            {
                _pickedTile.Button.gameObject.SetActive(true);
                _pickedTile = null;
            }
        }
    }

    private void NextStage()
    {
        StopCoroutine( _timerCoroutine );
        _timerCoroutine = null;
        _stageTime -= _stage;
        _tilesLeft = _tilesAmount;       
        _pickedTile = null;
        if (_stage > _saveSystem.Load())
        {
            _saveSystem.Save(_stage);
            _bestStageResultText.text = BestStageString(_stage);
        }
        _stage++;
        RefreshMatchTiles?.Invoke();        
        _spawner.RefreshBoard();
        _timer.text = InitialTimerText();
        
            
    }

    private string BestStageString(int bestStage)
    {
        return $"Best stage: {bestStage}";
    }

    private IEnumerator TimerRoutine()
    {
        float delta = _stageTime;
        while (delta > 0)
        {
            delta -= Time.deltaTime;
            _timer.text = $"Stage {_stage}\n\nTimer:  {(delta > 0 ? delta.ToString("F2") : 0)}";

            if (_tilesLeft <= 0)
            {                
                NextStage();
            }

            yield return null;
        }

        if (delta <= 0)
        {
            RestartGame();
        }        
    }

    public void RestartGame()
    {
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);
        _timerCoroutine = null;
        _pickedTile = null;
        _stageTime = _initialTime;
        _tilesLeft = _tilesAmount;
        _stage = 1;
        RefreshMatchTiles?.Invoke();
        _spawner.RefreshBoard();        
        _timer.text = InitialTimerText();
    }

    public event Action RefreshMatchTiles;
}
