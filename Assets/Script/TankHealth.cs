using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public int currentHealth = 100;
    public GameManager gameManager;
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    void Die()
    {
        gameManager.PlayerDied();
        // 处理坦克死亡逻辑，如播放死亡动画、销毁坦克等
        Destroy(gameObject);
    }
}
