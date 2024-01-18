using Assets.Scripts.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private void OnEnable()
    {
        ClassChooseController.DisableUIElement += DisableUIPanel;
    }

    private void DisableUIPanel(RectTransform uiPanel)
    {
        uiPanel.gameObject.SetActive(false);
        GameManager.Instance.SetGameState(States.GameStates.Game_Go);
    }
}
