using UnityEngine;
using UnityEngine.AI;
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
    public Transform[] patrolPoints; // 巡逻点
    public float detectionRadius = 20f; // 索敌范围
    public float attackRadius = 10f; // 攻击范围
    public float attackCooldown = 2f; // 攻击冷却时间
    public GameObject shellPrefab; // 炮弹预制体
    public Transform firePoint; // 炮弹发射点
    public int currentHealth = 100;

    private int currentPatrolIndex = 0;
    private NavMeshAgent navMeshAgent;
    private float attackTimer;
    private Transform player;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = AIState.Patrolling;
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
                break;
            case AIState.Destroyed:
                // AI销毁后的处理逻辑
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
            // 发射炮弹
            Instantiate(shellPrefab, firePoint.position, firePoint.rotation);
            attackTimer = attackCooldown;
        }

        navMeshAgent.transform.LookAt(player.position);

        if (Vector3.Distance(transform.position, player.position) > attackRadius)
        {
            navMeshAgent.isStopped = false;
            currentState = AIState.Chasing;
        }
    }

    public void TakeDamage(int damage)
    {
        // 减少生命值
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // 如果生命值为零，切换到销毁状态
            currentState = AIState.Destroyed;
            // 销毁处理逻辑，例如播放爆炸特效
            Destroy(gameObject);
        }
        
    }
}
