using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToBox : MonoBehaviour, ISkill
{
    GameObject _modelToSwap;
    GameObject _previousModel;
    private SliderController _slider;

    private void Start()
    {
        _previousModel = GameObject.Find("PlayerModel");
        _slider = GameObject.Find("Slider").GetComponent<SliderController>();
    }

    public void SkillAction()
    {
        if (_previousModel.activeSelf)
        {
            _previousModel.SetActive(false);
            _modelToSwap = Instantiate(GetComponent<PlayerController>()._modelToSwap, this.transform.position, transform.rotation);
            _modelToSwap.transform.SetParent(this.transform);
            _slider.StartActions(true);
        }
        else 
        {
            _previousModel.SetActive(true);
            _slider.StopAllActions();
            Destroy(_modelToSwap);
        }
    }
}
