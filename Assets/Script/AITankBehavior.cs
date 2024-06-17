using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITankBehavior : MonoBehaviour
{
    public List<Vector3> patrolPoints;
    private int currentPatrolIndex;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartPatrolling();
    }

    public void StartPatrolling()
    {
        if (patrolPoints != null && patrolPoints.Count > 0)
        {
            currentPatrolIndex = 0;
            MoveToNextPatrolPoint();
        }
    }

    void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Count == 0) return;

        navMeshAgent.destination = patrolPoints[currentPatrolIndex];
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
    }

    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            MoveToNextPatrolPoint();
        }
    }
}
