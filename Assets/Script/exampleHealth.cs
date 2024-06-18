using UnityEngine;

public class ExampleHealth : MonoBehaviour
{
    public HealthSliderController healthSliderController;

    private float maxHealth = 100;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthSliderController.SetHealthSliderValue(currentHealth );
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f); // 按空格键对角色造成10点伤害
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10f); // 按H键治疗角色10点生命值
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthSliderController.SetHealthSliderValue(currentHealth);
    }

    private void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthSliderController.SetHealthSliderValue(currentHealth);
    }
}
