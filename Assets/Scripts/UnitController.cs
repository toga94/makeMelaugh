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
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        unit = new Unit(RegStatus.Idle, 300, 400, 100);
    }



    void Update()
    {
        unit.MiusStatus(Time.deltaTime);


        float curSpeed = Mathf.Min(navMeshAgent.velocity.magnitude, 1);
        animator.SetFloat("Speed", curSpeed);

        if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
        {
            navMeshAgent.SetDestination(RandomNavmeshLocation(walkRadius));
        }
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
