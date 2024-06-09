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
    public Transform[] patrolPoints; // Ѳ�ߵ�
    public float detectionRadius = 20f; // ���з�Χ
    public float attackRadius = 10f; // ������Χ
    public float attackCooldown = 2f; // ������ȴʱ��
    public GameObject shellPrefab; // �ڵ�Ԥ����
    public Transform firePoint; // �ڵ������
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
                // AI���ٺ�Ĵ����߼�
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
        // ��������ֵ
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // �������ֵΪ�㣬�л�������״̬
            currentState = AIState.Destroyed;
            // ���ٴ����߼������粥�ű�ը��Ч
            Destroy(gameObject);
        }
        
    }
}
