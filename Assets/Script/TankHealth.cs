// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// public class TankHealth : MonoBehaviour
// {
//     public int currentHealth = 100;
//     public GameManager gameManager;
//     public void TakeDamage(int damage)
//     {
//         currentHealth -= damage;
//         if (currentHealth <= 0)
//         {
//             Die();
//             gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
//         }
//     }

//     void Die()
//     {
//         Time.timeScale = 0; // 停止游戏时间
//         gameManager.PlayerDied();

        
//          // 停止游戏逻辑
        

//         // ����̹�������߼����粥����������������̹�˵�
//         Destroy(gameObject);
//     }
// }

using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider; // 引用UI Slider
    public GameManager gameManager;
    public HealthSliderController healthSliderController;

    void Start()
    {
        currentHealth = startingHealth;
        UpdateHealthUI(); // 初始时更新血量UI
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI(); // 每次受伤后更新血量UI

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Time.timeScale = 0; // 停止游戏时间
        gameManager.PlayerDied();
        Destroy(gameObject); // 销毁坦克对象
    }

    void UpdateHealthUI()
    {
        // 更新血量滑动条的值
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Takeatt(10); // 按空格键对角色造成10点伤害
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10); // 按H键治疗角色10点生命值
        }
    }

    private void Takeatt(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthSliderController.SetHealthSliderValue(currentHealth);
    }

    private void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        healthSliderController.SetHealthSliderValue(currentHealth);
    }
}
