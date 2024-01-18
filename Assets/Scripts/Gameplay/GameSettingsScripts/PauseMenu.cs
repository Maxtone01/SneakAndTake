using Assets.Scripts.Interfaces;
using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, IPauseHandler
{
    #region Variables
    [Header("GameState")]
    public bool gameIsPaused = false;

    [Header("PauseMenus")]
    public GameObject pauseMenuUI;
    public GameObject pauseMenu;
    public GameObject optionsMenuUI;
    public GameObject optionsMenu;
    public GameObject videoSettings;
    public GameObject controlsSettings;
    private GameObject dialogueController;

    protected private bool IsPaused => GameContext.Instance.PauseManager.IsPaused;
    #endregion

    void Update()
    {
        KeyboardInputs();
    }

    #region Inputs
    public void KeyboardInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            dialogueController = GameObject.FindGameObjectWithTag("Dialogue Panel");
            if (dialogueController == null & !IsPaused)
            {
                pauseMenu.SetActive(true);
                SetPause(true);
                GameManager.Instance.SetGameState(States.GameStates.Pause);
            }
            else
            {
                ResumeGame();
                SetPause(false);
                GameManager.Instance.SetGameState(States.GameStates.Game_Go);
            }
        }
    }
    #endregion

    #region MenuActions

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        SetPause(false);
        GameManager.Instance.SetGameState(States.GameStates.Game_Go);
        if (optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
    }

    private void QuitGame()
    {
        //to do quit mechanic
    }
    #endregion

    #region MenuPages
    public void OptionsMenu()
    {
        pauseMenuUI.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void LoadGameMenu()
    {
        //to do load game mechanic
    }
    public void ControlsMenu()
    {
        optionsMenuUI.SetActive(false);
        controlsSettings.SetActive(true);
    }

    public void VideoSettingsMenu()
    {
        optionsMenuUI.SetActive(false);
        videoSettings.SetActive(true);
    }

    public void SetPause(bool isPaused)
    {
        GameContext.Instance.PauseManager.SetPause(isPaused);
    }

    #endregion

}
