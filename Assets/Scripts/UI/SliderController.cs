using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private bool _canActivateSkill = true;

    private bool _skillActive;

    private bool _canAdd = false;

    private TurnToBox _skill;

    public void Start()
    {
        _skill = GameObject.Find("Player").GetComponent<TurnToBox>();
    }

    public void SetValueMax(float value)
    {
        _slider.maxValue = value;
        _slider.value = value;
    }

    public void StartActions(bool canSubtract)
    {
        if (_canActivateSkill)
        {
            _skillActive = canSubtract;
            if (_canAdd)
            {
                StopCoroutine(SliderAddValue());
            }
            StartCoroutine(SliderSubtractValue());
            _canAdd = false;
        }
        else 
        {
            Debug.Log("Wait for skil to coldown!");
        }
    }

    public void StopAllActions()
    {
        StopCoroutine(SliderSubtractValue());
        _skillActive = false;
        _canAdd = true;
        _canActivateSkill = false;
        StartCoroutine(SliderAddValue());
    }

    private IEnumerator SliderSubtractValue()
    {
        while(_skillActive)
        {
            if (_slider.value == 0)
            {
                _skillActive = false;
                _skill._onSwitchModel.Invoke();
                StopCoroutine(SliderSubtractValue());
            }
            else
            {
                _slider.value -= 1;
                _canAdd = false;
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private IEnumerator SliderAddValue()
    {
        while (_canAdd)
        {
            if (_slider.value < _slider.maxValue)
            {
                _slider.value += 1;
                yield return new WaitForSeconds(1f);
            }
            if (_slider.value == _slider.maxValue)
            {
                _canActivateSkill = true;
                _canAdd = false;
                StopCoroutine(SliderAddValue());
            }
        }
    }

    public bool GetSkillState()
    {
        return _canActivateSkill;
    }
}
