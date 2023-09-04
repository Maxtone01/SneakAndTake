using Assets.Scripts.ClassSystem;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using static ClassSO;

namespace Assets.Scripts.Controllers
{
    public class ClassChooseController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _className;
        [SerializeField]
        private Image _classIcon;
        [SerializeField]
        private TextMeshProUGUI _classDescription;
        [SerializeField]
        GameObject perksPanelContainer;
        [SerializeField]
        GameObject perksPanel;

        protected internal void SetClassInformation(ClassTypeHolder classType)
        {
            _className.text = classType.GetClassType().className;
            _classDescription.text = classType.GetClassType().name;
            this.gameObject.name = classType.GetClassType().name;
            foreach (ClassPerks classPerk in classType.GetPerks())
            {
                var perkPanel = Instantiate(perksPanel, perksPanelContainer.transform);
                perkPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = classPerk.perkDescription;
            }
        }

        //private InitialStatsProvider _statsProvider;

        //public void SetPlayerClassStats()
        //{
        //    _statsProvider.GetPlayerStats(PlayerClass.Sneaker);
        //}
    }
}