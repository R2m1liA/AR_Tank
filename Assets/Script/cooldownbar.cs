using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    public Slider cooldownSlider; // 引用UI Slider
    public float cooldownTime = 2.0f; // 冷却时间，单位秒
    private float currentTime = 0.0f; // 当前冷却时间

    private bool isCooldown = false; // 是否处于冷却中

    void Start()
    {
        cooldownSlider.value = 1.0f; // 初始时冷却条满
    }

    void Update()
    {
        if (isCooldown)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isCooldown = false;
            }

            UpdateCooldownUI();
        }
    }

    void UpdateCooldownUI()
    {
        cooldownSlider.value = currentTime / cooldownTime;
    }

    // 开始冷却
    public void StartCooldown()
    {
        if (!isCooldown)
        {
            isCooldown = true;
            currentTime = cooldownTime;
            UpdateCooldownUI();
        }
    }

    // 检查是否在冷却中
    public bool IsOnCooldown()
    {
        return isCooldown;
    }
}

