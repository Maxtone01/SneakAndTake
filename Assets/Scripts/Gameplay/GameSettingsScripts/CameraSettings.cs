using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraSettings : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI sensValue;
    [SerializeField] private Slider sensSlider;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] CinemachineFreeLook cameraSpeed;

    /*Need to add config file!*/
    #endregion

    public void Start()
    {
        sensValue.text = GameManager.Instance.defaultCamSpeed.ToString();
        sensSlider.value = GameManager.Instance.defaultCamSpeed;

        //_configFile = "Assets/GameFiles/ConfigurationFile/" + "Configuration.txt";
    }

    public void UpdateSensitivityText()
    {
        sensValue.text = sensSlider.value.ToString();
    }

    #region Buttons
    public void ApplySensitivityAndClose()
    {
        cameraSpeed.m_XAxis.m_MaxSpeed = sensSlider.value;
        cameraSpeed.m_YAxis.m_MaxSpeed = sensSlider.value;
        //if (!File.Exists(_configFile))
        //{ 
        //    File.WriteAllText(_configFile, $"camera_speed={sensSlider.value}");
        //}
        CloseMenu();
    }

    private void CloseMenu()
    {
        _pauseMenu.optionsMenuUI.SetActive(true);
        _pauseMenu.controlsSettings.SetActive(false);
    }

    public void BackButton()
    {
        CloseMenu();
    }
    #endregion
}
