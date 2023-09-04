using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class TurnToBox : MonoBehaviour, ISkill
    {
        [SerializeField]
        public GameObject _modelToSwap;
        private GameObject _modelInstance;
        GameObject _previousModel;
        private SliderController _slider;

        public Action _onSwitchModel;

        private void Start()
        {
            _previousModel = GameObject.Find("PlayerModel");
            _slider = GameObject.Find("Slider").GetComponent<SliderController>();
            _onSwitchModel += SwitchModelToBase;
        }

        public void SkillAction()
        {
            if (_previousModel.activeSelf)
            {
                if (_slider.GetSkillState())
                {
                    _previousModel.SetActive(false);
                    _modelInstance = Instantiate(_modelToSwap, transform.position, transform.rotation);
                    _modelInstance.transform.SetParent(transform);
                    _slider.StartActions(true);
                }
            }
            else
            {
                SwitchModelToBase();
            }
        }

        private void SwitchModelToBase()
        {
            _previousModel.SetActive(true);
            _slider.StopAllActions();
            Destroy(_modelInstance);
        }
    }
}