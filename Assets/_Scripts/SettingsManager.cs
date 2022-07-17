using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sensitivityText;
    private void Start()
    {
        UpdateText(PlayerManager.sensitivity);
        _slider.value = PlayerManager.sensitivity;
        _slider.onValueChanged.AddListener((float value) =>
        {
            var sense = (int)value;
            PlayerManager.sensitivity = sense;
            UpdateText(sense);
        });
    }

    private void UpdateText(int sens)
    {
        _sensitivityText.SetText($"Mouse Sensitivity: {sens}");
    }
}
