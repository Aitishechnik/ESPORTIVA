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

    private void Awake()
    {
        _activeUI = _mainMenu;
        _miniGameButton.onClick.AddListener(()=> StartMiniGame(_miniGame));
        _returnButton.onClick.AddListener(() => StartMiniGame(_mainMenu));
    }
    private void StartMiniGame(RectTransform rect)
    {
        SetActiveRecursively(rect, true);
        SetActiveRecursively(_activeUI, false);
        _activeUI = rect;
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
