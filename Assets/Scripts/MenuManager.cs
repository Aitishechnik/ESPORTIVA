using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private RectTransform _activeUI;

    [SerializeField]
    private Button _miniGameButton;

    [SerializeField]
    private Button _matchesButton;

    [SerializeField]
    private Button _settingsButton;

    [SerializeField]
    private Button _returnButton;

    [SerializeField]
    private MatchGameManager _gameManager;

    [SerializeField]
    private RectTransform _mainMenu;

    [SerializeField]
    private RectTransform _miniGame;

    [SerializeField]
    private RectTransform _settings;

    private void Awake()
    {
        _activeUI = _mainMenu;
        _returnButton.gameObject.SetActive(false);
        _miniGameButton.onClick.AddListener(()=> ChangeScreen(_miniGame));
        _returnButton.onClick.AddListener(()=> ChangeScreen(_mainMenu));
        _settingsButton.onClick.AddListener(()=> ChangeScreen(_settings));
    }
    private void ChangeScreen(RectTransform rect)
    {        
        if (_miniGame.gameObject.activeSelf)
            _gameManager.RestartGame();
        SetActiveRecursively(rect, true);
        SetActiveRecursively(_activeUI, false);
        _activeUI = rect;
        SetReturnButton();
    }

    private void SetReturnButton()
    {
        _returnButton.gameObject.SetActive(_activeUI == _mainMenu ? false : true);
    }

    private static void SetActiveRecursively(RectTransform obj, bool isActive)
    {
        obj.gameObject.SetActive(isActive);

        for(int i = 0; i < obj.childCount; i++)
        {
            var child = obj.GetChild(i) as RectTransform;
            SetActiveRecursively(child, isActive);
        }
    }
}
