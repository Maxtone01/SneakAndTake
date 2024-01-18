using Assets.Scripts.Interfaces;
using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IPauseHandler
{
    public static GameManager Instance { get; private set; }
    [Header("GameSettings")]
    public float defaultCamSpeed;
    public float defaultResolution;
    //private ConversantController _conversantController;
    [SerializeField] private GameObject _cameraState;
    [SerializeField] private QuestGiver _questGiver;
    [SerializeField] private PlayerGeneral _player;
    [SerializeField] private Animator _animator;


    private void Awake()
    {
        Cursor.visible = false;
    }

    public void Start()
    {
        Instance = this;
        GameContext.Instance.Initialize();
        SettingsInitialization();
        GameContext.Instance.PauseManager.Register(this);
        DontDestroyOnLoad(gameObject);
    }

    public void SetGameState(States.GameStates action)
    {
        switch (action)
        {
            case States.GameStates.Enter_Dialogue:
                Cursor.visible = true;
                _cameraState.SetActive(false);
                _player.enabled = false;
                _animator.SetFloat("State", 0);
                break;

            case States.GameStates.Exit_Dialogue:
                Cursor.visible = false;
                _cameraState.SetActive(true);
                _player.enabled = true;
                break;

            case States.GameStates.Select_Class:
                Cursor.visible = true;
                _cameraState.SetActive(false);
                break;

            case States.GameStates.Game_Go:
                _cameraState.SetActive(true);
                _player.enabled = true;
                Cursor.visible = false;
                break;

            case States.GameStates.Pause:
                _cameraState.SetActive(false);
                _player.enabled = false;
                Cursor.visible = true;
                break;
        }
    }

    public void SettingsInitialization()
    {
        defaultCamSpeed = 2;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    void IPauseHandler.SetPause(bool isPaused)
    {
        Time.timeScale = isPaused ? 0 : 1f;
    }
}
