using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChanger : MonoBehaviour {


    Slider sliderScript;
	// Use this for initialization
	void Start () {
        sliderScript = GetComponent<Slider>();
        sliderScript.value = AudioSliderHandler.VOLUME;
        sliderScript.onValueChanged.AddListener(updateVolumeValue);
	}

    void updateVolumeValue(float volume)
    {
        AudioSliderHandler.VOLUME = volume;
    }
	
	
}
