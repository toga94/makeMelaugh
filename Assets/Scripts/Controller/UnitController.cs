using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private float walkRadius = 1f;
    [SerializeField] public Animator animator;

    //[SerializeField] private List<Transform> patrolPoints;
    public NavMeshAgent navMeshAgent { get; private set; }
    public PatrolPath Target { get; private set; }
    private float stayDuration = 0f;
    private float stayTimer = 0f;
    private int currentPatrolIndex = 0;

    private StateMachine _stateMachine;
    private bool _canPlayAnimation;
    private float _curSpeed;
    void Start()
    {
        _stateMachine = new StateMachine();
        navMeshAgent = GetComponent<NavMeshAgent>();
        unit = new Unit(RegStatus.Idle, 300, 400, 100);
        InitializeStateMachine();

        _curSpeed = Mathf.Min(navMeshAgent.velocity.magnitude, 1);
        animator.SetFloat("Speed", _curSpeed);

    }
    void Update()
    {
        unit.MiusStatus(Time.deltaTime);

        _stateMachine.Tick();

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

        if (Target != null)
        {
            _canPlayAnimation = Vector3.Distance(Target.Transform.position, transform.position) < 1.5f && Vector3.Distance(Target.Transform.position, transform.position) >= navMeshAgent.stoppingDistance;
            if(_canPlayAnimation) PathManager.Instance.ReleasePath(Target);
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

        //PathManager.Instance.ReleasePath(Target);
        SetNextPatrolPoint();


        _stateMachine.AddState(idleState, walkState, () => Target != null );
        _stateMachine.AddState(walkState, sitState, () =>  _canPlayAnimation && Target != null && Target.Transform.name.Contains("sit"));
        _stateMachine.AddState(sitState, walkState, () => Target != null && !Target.Transform.name.Contains("sit"));
        _stateMachine.AddState(walkState, idleState, () => Target != null &&  _canPlayAnimation);

        _stateMachine.SetState(idleState);
    }

    
    public void SetNextPatrolPoint()
    {
        //Target = null;
        //(currentPatrolIndex + 1) % patrolPoints.Count
        //currentPatrolIndex =  Random.Range(0, patrolPoints.Count);
        //Target = patrolPoints[currentPatrolIndex].transform;
        //navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].transform.position);
        //stayDuration = Random.Range(1f, 5f); 
        //stayTimer = 0f;
        if (Target != null)
        {
            PathManager.Instance.ReleasePath(Target);
            Target = null;
        }

        if (PathManager.Instance.availablePatrolPaths.Count == 0) return;

        Target = PathManager.Instance.RequestPath();
        navMeshAgent.SetDestination(Target.Transform.position);
        stayDuration = Random.Range(1f, 5f);
        stayTimer = 0f;

    }
    /*
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
    }*/
}
