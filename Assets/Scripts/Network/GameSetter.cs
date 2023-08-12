using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class GameSetter : MonoBehaviour
{
    [SerializeField] private NetworkRunner _networkRunnerPrefab = null;
    [SerializeField] private PlayerData _playerDataPrefab;
    
    [SerializeField] private InputField _userName = null;
    [SerializeField] private Text _usernamePlaceHolder = null;

    [SerializeField] private string _gameSceneName = null;

    private NetworkRunner _runnerInstance;
    public GameObject joinButton;



    public void Start()
    {
        ListenJoinButton();
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            StartSharedSession();
        }
    }

    public void ListenJoinButton()
    {
        joinButton.GetComponent<Button>().onClick.AddListener(()=> StartSharedSession());
    }

    public void StartSharedSession()
    {
        SetPlayerData();
        StartGame(GameMode.Shared, _gameSceneName);
    }


    public void SetPlayerData()
    {
        var playerData = FindObjectOfType<PlayerData>();
        if (playerData == null)
        {
            playerData = Instantiate(_playerDataPrefab);
            
        }

        if (string.IsNullOrWhiteSpace(_userName.text))
        {
            playerData.SetUserName(_usernamePlaceHolder.text);
        }
        else
        {
            playerData.SetUserName(_userName.text);
        }
    }

    private async void StartGame(GameMode mode, string sceneName)
    {
        _runnerInstance = FindObjectOfType<NetworkRunner>();

        if (_runnerInstance == null)
        {
            _runnerInstance = Instantiate(_networkRunnerPrefab);
        }

        _runnerInstance.ProvideInput = true;
        
        var startGameArgs = new StartGameArgs()
        {
            GameMode = mode
        };
        await _runnerInstance.StartGame(startGameArgs);
        _runnerInstance.SetActiveScene(sceneName);
        
    }
    
}
