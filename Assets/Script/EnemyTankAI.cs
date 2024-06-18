using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum AIState
{
    Initializing,
    Patrolling,
    Chasing,
    Attacking,
    Destroyed
}

public class EnemyTankAI : MonoBehaviour
{
    public AIState currentState = AIState.Initializing;
    public Transform[] patrolPoints; // Ѳ�ߵ�
    public float detectionRadius = 20f; // ���з�Χ
    public float attackRadius = 10f; // ������Χ
    public float attackCooldown = 2f; // ������ȴʱ��
    public GameObject shellPrefab; // �ڵ�Ԥ����
    public Transform firePoint; // �ڵ������
    public int currentHealth = 100;
    public GameObject explosionEffect;// ��ը��ЧԤ����
    public AudioClip explosionSound;// ��ը��Ч
    public AudioClip FireSound;// ��ը��Ч

    private int currentPatrolIndex = 0;
    private NavMeshAgent navMeshAgent;
    private float attackTimer;
    private Transform player;
    private GameManager gameManager;

    public CooldownBar cooldownBar;

    public HealthSliderController healthSliderController;

    public Slider healthSlider; // 引用UI Slider


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = FindObjectOfType<GameManager>();
        currentState = AIState.Patrolling;
        currentHealth = 100;
        healthSlider.maxValue = 100; // 设置滑动条最大值
        healthSlider.value = currentHealth; // 设置滑动条当前值
        UpdateHealthUI(); // 初始时更新血量UI
    }

    void Update()
    {
        
        switch (currentState)
        {
            case AIState.Patrolling:
                Patrol();
                DetectPlayer();
                break;
            case AIState.Chasing:
                ChasePlayer();
                CheckAttackRange();
                break;
            case AIState.Attacking:
                AttackPlayer();
                cooldownBar.StartCooldown();
                break;
            case AIState.Destroyed:
                // AI���ٺ�Ĵ����߼�
                DestroyTank();
                break;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0)
            return;

        navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void DetectPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            currentState = AIState.Chasing;
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.destination = player.position;
        navMeshAgent.transform.LookAt(player.position);
    }

    void CheckAttackRange()
    {
        if (Vector3.Distance(transform.position, player.position) < attackRadius)
        {
            currentState = AIState.Attacking;
        }
    }

    void AttackPlayer()
    {
        navMeshAgent.isStopped = true;
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            // �����ڵ�
            Instantiate(shellPrefab, firePoint.position, firePoint.rotation);
            AudioSource.PlayClipAtPoint(FireSound, transform.position);
            attackTimer = attackCooldown;
        }

        if(player)
        {
            navMeshAgent.transform.LookAt(player.position);
        }

        if (Vector3.Distance(transform.position, player.position) > attackRadius)
        {
            navMeshAgent.isStopped = false;
            currentState = AIState.Chasing;
        }
    }

    public void TakeDamage(int damage)
    {
        // ��������ֵ
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // �������ֵΪ�㣬�л�������״̬
            currentState = AIState.Destroyed;
        }
        
    }

    public void DestroyTank()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation).GetComponent<ParticleSystem>().Play();
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        gameManager.AddScore(20);
        Destroy(gameObject);
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

    void UpdateHealthUI()
    {
        // 更新血量滑动条的值
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth;
        }
    }
}
