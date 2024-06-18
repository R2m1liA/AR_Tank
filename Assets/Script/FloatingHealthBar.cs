using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHealthBar : MonoBehaviour
{
    private Slider healthslider;
    private Slider blueslider;

    public void UpdateHealthBar(float currentValue)
    {
        healthslider.value=currentValue/100;
    }

    public void UpdateBlueBar(){
        blueslider.value=attackTimer/2;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
