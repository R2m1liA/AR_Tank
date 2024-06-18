using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    private Slider healthSlider;
    public Image fillImage; // 填充图像

    private void Start()
    {
        healthSlider = GetComponent<Slider>();

        if (healthSlider != null)
        {
            healthSlider.onValueChanged.AddListener(OnHealthValueChanged);
        }
    }

    public void UpdateHealthBar(float currentValue)
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentValue / 100;
        }
    }

    private void OnHealthValueChanged(float value)
    {
        // 根据当前健康值修改填充颜色
        if (value > 0.5f)
        {
            fillImage.color = Color.green;
        }
        else if (value > 0.2f)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
