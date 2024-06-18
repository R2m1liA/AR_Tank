using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Slider slider;
    public AudioSource BackgroundMusic;

    public void Volume()
    {
        BackgroundMusic.volume = slider.value;
    }
}
