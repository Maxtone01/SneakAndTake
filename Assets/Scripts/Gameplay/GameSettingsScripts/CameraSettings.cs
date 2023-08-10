using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CameraSettings : MonoBehaviour
{
    #region Variables
    [SerializeField] private Text sensValue = null;
    [SerializeField] private Slider sensSlider = null;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] CinemachineFreeLook cameraSpeed;

    private string _configFile;
    #endregion


    public void Start()
    {
        _pauseMenu = GetComponent<PauseMenu>();

        sensValue.text = GameManager.Instance.defaultCamSpeed.ToString();
        sensSlider.value = GameManager.Instance.defaultCamSpeed;

        _configFile = "Assets/GameFiles/ConfigurationFile/" + "Configuration.txt";
    }

    public void UpdateMouseSensitivity()
    {
        sensValue.text = sensSlider.value.ToString();
    }

    #region Buttons
    public void ApplyButton()
    {
        cameraSpeed.m_XAxis.m_MaxSpeed = sensSlider.value;
        cameraSpeed.m_YAxis.m_MaxSpeed = sensSlider.value;
        //if (!File.Exists(_configFile))
        //{ 
        //    File.WriteAllText(_configFile, $"camera_speed={sensSlider.value}");
        //}
        _pauseMenu.optionsMenuUI.SetActive(true);
        _pauseMenu.controlsSettings.SetActive(false);
    }

    public void BackButton()
    {
        _pauseMenu.optionsMenuUI.SetActive(true);
        _pauseMenu.controlsSettings.SetActive(false);
    }
    #endregion
}
