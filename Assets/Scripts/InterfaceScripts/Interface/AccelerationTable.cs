using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerationTable : MonoBehaviour
{
    [SerializeField] private Slider _sliderBonusTimeLeft;
    private void OnEnable()
    {
        UpdateSlider();
    }

    private void Update()
    {
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        _sliderBonusTimeLeft.value = PickUpsManager._instance.BonusTimeLeft;
        if(PickUpsManager._instance.BonusTimeLeft <= 0)
            gameObject.SetActive(false);
    }
    public void SetMaxTime(float time) => _sliderBonusTimeLeft.maxValue = time;

}
