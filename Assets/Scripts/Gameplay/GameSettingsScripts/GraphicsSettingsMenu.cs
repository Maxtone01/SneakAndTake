using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettingsMenu : MonoBehaviour
{
    #region Variables
    [SerializeField] private PauseMenu _pauseMenu;

    public int gorizontal, vertical;
    public Toggle fscreenTogle;
    public int selectedRes;
    public TMP_Text resolution;

    List<string> fscreenList = new List<string>();
    Resolution[] resolutions;
    #endregion

    void Start()
    {
        _pauseMenu = GetComponent<PauseMenu>();

        fscreenTogle.isOn = Screen.fullScreen;

        resolutions = Screen.resolutions;

        foreach (var res in resolutions)
        {
            fscreenList.Add($"{res.width}x{res.height}");
        }

        resolution.text = $"{Screen.currentResolution.width} X {Screen.currentResolution.height}";
    }

    #region Actions

    public void SwitchLeft()
    {
        selectedRes--;
        if (selectedRes < 0)
        {
            selectedRes = 0;
        }
        UpdateResolutionText();
    }

    public void SwitchRight()
    {
        selectedRes++;
        if (selectedRes > fscreenList.Count - 1)
        {
            selectedRes = fscreenList.Count - 1;
        }
        UpdateResolutionText();
    }

    void UpdateResolutionText()
    {
        resolution.text = fscreenList[selectedRes];
    }

    public void AppllyChanges()
    {
        Screen.SetResolution(resolutions[selectedRes].width, resolutions[selectedRes].height, fscreenTogle.isOn);
        ClosePauseMenu();
    }
    
    public void StepBack()
    {
        ClosePauseMenu();
    }

    private void ClosePauseMenu()
    {
        _pauseMenu.optionsMenuUI.SetActive(true);
        _pauseMenu.videoSettings.SetActive(false);
    }
    #endregion
}