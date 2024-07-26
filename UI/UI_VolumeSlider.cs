using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public string paramater;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float multiplier;

    public void SliderValue(float _value)
    { 
        audioMixer.SetFloat(paramater,Mathf.Log10(_value) * multiplier);
    }

    public void LoadSlider(float _value)
    {
        if (_value >= 0.001f)
        { 
            slider.value = _value;
        }
    }

}
