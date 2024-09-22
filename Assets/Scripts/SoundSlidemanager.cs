using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //slider uses UI namespace

public class SoundSlidemanager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; //private but shows up in editor


    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume")) //if there is no previous volume adjustment,default
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
            Load();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value; //volume of the music equals the value from the slider
        Save(); //save the changed value
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume"); //assigning the value from the musicVolume
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}

 
