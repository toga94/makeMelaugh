using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [SerializeField] private Unit unit;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float walkRadius = 1f;
    [SerializeField] private Animator animator;

    [SerializeField] private List<Vector3> patrolPoints;
    private float stayDuration = 0f;
    private float stayTimer = 0f;
    private int currentPatrolIndex = 0;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        unit = new Unit(RegStatus.Idle, 300, 400, 100);
        SetNextPatrolPoint();

    }



    void Update()
    {
        unit.MiusStatus(Time.deltaTime);


        float curSpeed = Mathf.Min(navMeshAgent.velocity.magnitude, 1);
        animator.SetFloat("Speed", curSpeed);

        if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
        {
            //navMeshAgent.SetDestination(RandomNavmeshLocation(walkRadius));

            if (stayTimer < stayDuration)
            {
                stayTimer += Time.deltaTime;
            }
            else
            {
                SetNextPatrolPoint();
            }
        }
    }

    private void SetNextPatrolPoint()
    {
        if (patrolPoints.Count == 0) return;
        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex]);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        stayDuration = Random.Range(1f, 5f); 
        stayTimer = 0f;
    }
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
