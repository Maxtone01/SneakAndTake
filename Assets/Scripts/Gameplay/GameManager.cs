using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("GameSettings")]
    public float defaultCamSpeed;
    public float defaultResolution;
    private ConversantController conversantController;
    [SerializeField] GameObject cameraState;
    [SerializeField] QuestGiver _questGiver;
    PlayerGeneral _player;
    Animator animator;

    private void Awake()
    {
        Cursor.visible = false;
    }

    public void Start()
    {
        Instance = this;
        SettingsInitialization();
        conversantController = GameObject.FindGameObjectWithTag("Player").GetComponent<ConversantController>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGeneral>();
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        //_player.Initialize(Assets.Scripts.Player.PlayerType.Guard);
        //_questGiver.GiveQuest();
    }

    public void MouseState(States.GameStates action)
    {
        switch (action)
        {
            case States.GameStates.Enter_Dialogue:
                Cursor.visible = true;
                cameraState.SetActive(false);
                _player.enabled = false;
                animator.SetFloat("State", 0);
                break;

            case States.GameStates.Exit_Dialogue:
                Cursor.visible = false;
                cameraState.SetActive(true);
                _player.enabled = true;
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
}
