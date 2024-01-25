using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [SerializeField] private Unit unit;
    public NavMeshAgent navMeshAgent;
    [SerializeField] private float walkRadius = 1f;
    [SerializeField] public Animator animator;

    [SerializeField] public List<Transform> patrolPoints;
    public Transform Target { get; private set; }
    public float stayDuration = 0f;
    public float stayTimer = 0f;
    private int currentPatrolIndex = 0;

    private StateMachine _stateMachine;
    private bool canPlayAnimation;
    private float curSpeed;
    void Start()
    {
        _stateMachine = new StateMachine();

        navMeshAgent = GetComponent<NavMeshAgent>();
        unit = new Unit(RegStatus.Idle, 300, 400, 100);
        InitializeStateMachine();

        curSpeed = Mathf.Min(navMeshAgent.velocity.magnitude, 1);
        animator.SetFloat("Speed", curSpeed);

    }
    void Update()
    {
        unit.MiusStatus(Time.deltaTime);

        _stateMachine.Tick();
        if (Target != null)
        {
            canPlayAnimation = Vector3.Distance(Target.position, transform.position) <= navMeshAgent.stoppingDistance;
        }
        
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedTick();
    }

    private void LateUpdate()
    {
        _stateMachine.LateTick();
    }

    private void InitializeStateMachine()
    {
        IdleState idleState = new IdleState(this);
        WalkState walkState = new WalkState(this);
        SitState sitState = new SitState(this);

        _stateMachine.AddState(idleState, walkState, () => Target != null);
        _stateMachine.AddState(walkState, sitState, () =>  canPlayAnimation && Target != null && Target.CompareTag("Sit"));
        _stateMachine.AddState(sitState, walkState, () => Target != null && !Target.CompareTag("Sit"));

        _stateMachine.SetState(idleState);
    }
    public void SetNextPatrolPoint()
    {
        if (patrolPoints.Count == 0) return;

        Target = null;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        Target = patrolPoints[currentPatrolIndex].transform;
        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].transform.position);
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
