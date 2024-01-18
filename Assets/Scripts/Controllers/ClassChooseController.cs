using Assets.Scripts.ClassSystem;
using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

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
        TextMeshProUGUI _perkName;
        [SerializeField]
        TextMeshProUGUI _perkDescription;
        [SerializeField]
        RectTransform _classChosablePanel;

        private InitialStatsProvider _statsProvider;

        private string _classNameEng;

        private PlayerGeneral _player;

        public static event Action<RectTransform> DisableUIElement;

        private void Awake()
        {
            _statsProvider = new InitialStatsProvider();
            _player = GameObject.Find("Player").GetComponent<PlayerGeneral>();
        }

        protected internal void SetUiClassInformation(ClassElementsProvider classType)
        {
            _className.text = classType.GetClassName();
            _classDescription.text = classType.GetClassDescription();
            gameObject.name = classType.GetClassName();
            _perkName.text = classType.GetPerkName();
            _perkDescription.text = classType.GetPerkDescription();
            _classNameEng = classType.GetClassNameEng();
        }

        public void SetPlayerClassStats()
        {
            switch (_classNameEng)
            {
                case "Thief":
                    _player.SetUpPlayerStats(_statsProvider.GetPlayerStats(PlayerClass.Sneaker),
                        PlayerClass.Sneaker);
                    break;
                case "Strongman":
                    _player.SetUpPlayerStats(_statsProvider.GetPlayerStats(PlayerClass.Strongman),
                        PlayerClass.Strongman);
                    break;
                case "Scout":
                    _player.SetUpPlayerStats(_statsProvider.GetPlayerStats(PlayerClass.Scout),
                        PlayerClass.Scout);
                    break;
                case "Trickster":
                    _player.SetUpPlayerStats(_statsProvider.GetPlayerStats(PlayerClass.Trickster),
                        PlayerClass.Trickster);
                    break;
            }
            DisableUIElement?.Invoke(_classChosablePanel);
        }
    }
}