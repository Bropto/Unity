using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Slider SoundSlider;
    public Toggle[] DisPlayToggle;
    public AudioSource Audio;

    private float Sound = 1f;
    private float saveSound;

    private void Start()
    {
        Sound = PlayerPrefs.GetFloat("Volume", 1f);
        SoundSlider.value = Sound;
        Audio.volume = SoundSlider.value;
    }
    private void Update()
    {
        Slider();
    }
    public void Slider()
    {
        Audio.volume = SoundSlider.value;
        Sound = SoundSlider.value;
        PlayerPrefs.SetFloat("Volume", Sound);
    }

    public void confirm()
    {
        saveSound = Sound;
        PlayerPrefs.SetFloat("Volume", Sound);        
    }
    public void cancel()
    {
        Sound = saveSound;
    }
}
