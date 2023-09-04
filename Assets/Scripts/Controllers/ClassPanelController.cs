using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class ClassPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject _classInfoPanel;

        public void InitializeClassPanel(Button btn)
        {
            var _playerClassName = btn.GetComponent<ClassTypeHolder>();
            _classInfoPanel.GetComponent<ClassChooseController>().SetClassInformation(_playerClassName);
            _classInfoPanel.SetActive(true);
        }
    }
}