using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider _sliderController;
    private bool _skillActive;

    private bool _canAdd;

    public void Start()
    {
        SetValueMax(4);
    }

    public void SetValueMax(int value)
    {
        _sliderController.maxValue = value;
        _sliderController.value = value;
    }

    public void StartActions(bool canSubtract)
    {
        _skillActive = canSubtract;
        StopCoroutine(SliderAddValue());
        StartCoroutine(SliderSubtractValue());
        _canAdd = false;
    }

    public void StopAllActions()
    {
        StopCoroutine(SliderSubtractValue());
        _skillActive = false;
        _canAdd = true;
        StartCoroutine(SliderAddValue());
    }

    private IEnumerator SliderSubtractValue()
    {
        while(_skillActive)
        {
            if (_sliderController.value == 0)
            {
                _skillActive = false;
                StopCoroutine(SliderSubtractValue());
            }
            else 
            {
                _sliderController.value -= 1;
                _canAdd = false;
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private IEnumerator SliderAddValue()
    {
        while (_canAdd)
        {
            if (_sliderController.value < _sliderController.maxValue)
            {
                _sliderController.value += 1;
                yield return new WaitForSeconds(1f);
            }
            else 
            {
                _canAdd = false;
                StopCoroutine(SliderAddValue());
            }
        }
    }

    public bool GetSlider()
    {
        return _skillActive;
    }
}
