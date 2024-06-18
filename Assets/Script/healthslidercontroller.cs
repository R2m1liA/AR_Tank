using UnityEngine;
using UnityEngine.UI;

public class HealthSliderController : MonoBehaviour
{
    public Slider healthSlider;
    public Image fillImage;
    public Image backgroundImage;

    void Start()
    {
        if (healthSlider != null)
        {
            // 初始化填充和背景颜色
            fillImage.color = Color.green;
            backgroundImage.color = Color.gray;

            // 订阅Slider的值变化事件
            healthSlider.onValueChanged.AddListener(OnHealthValueChanged);
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

    public void SetHealthSliderValue(float value)
    {
        healthSlider.value = value;
    }
}
