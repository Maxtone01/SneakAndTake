using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class ClassPanelController : MonoBehaviour
    {
        [SerializeField] private ClassChooseController _classInfoPanel;

        private void Start()
        {
            GameManager.Instance.SetGameState(States.GameStates.Select_Class);
        }

        public void InitializeClassPanel(ClassElementsProvider classType)
        {
            _classInfoPanel.SetUiClassInformation(classType);
            _classInfoPanel.gameObject.SetActive(true);
        }
    }
}