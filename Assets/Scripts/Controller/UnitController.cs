using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private float walkRadius = 1f;
    [SerializeField] public Animator animator;

    public NavMeshAgent navMeshAgent { get; private set; }
    public PatrolPath Target { get; private set; }
    private float stayDuration = 0f;
    private float stayTimer = 0f;

    private StateMachine _stateMachine;
    private bool _canPlayAnimation;
    private float _curSpeed;
    private bool isTrapped;

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
            _canPlayAnimation = Vector3.Distance(Target.Transform.position, transform.position) < 0.5f && Vector3.Distance(Target.Transform.position, transform.position) >= navMeshAgent.stoppingDistance;

            if (_canPlayAnimation) navMeshAgent.speed = 0;

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
        WashState washState = new WashState(this);
        CookState cookState = new CookState(this);
        TrapState trapState = new TrapState(this);

        SetNextPatrolPoint();


        _stateMachine.AddState(idleState, walkState, () => Target != null );
        _stateMachine.AddState(idleState, trapState, () => isTrapped);

        _stateMachine.AddState(walkState, sitState, () =>  _canPlayAnimation && Target != null && Target.Transform.name.Contains("sit"));
        _stateMachine.AddState(walkState, washState, () =>  _canPlayAnimation && Target != null && Target.Transform.name.Contains("wash"));
        _stateMachine.AddState(walkState, cookState, () =>  _canPlayAnimation && Target != null && Target.Transform.name.Contains("cook"));
        _stateMachine.AddState(walkState, trapState, () => isTrapped);
        _stateMachine.AddState(walkState, idleState, () => Target != null &&  _canPlayAnimation);

        _stateMachine.AddState(sitState, walkState, () => Target != null && !Target.Transform.name.Contains("sit"));

        _stateMachine.AddState(washState, walkState, () => Target != null && !Target.Transform.name.Contains("wash"));

        _stateMachine.AddState(cookState, walkState, () => Target != null && !Target.Transform.name.Contains("cook"));

        _stateMachine.AddState(trapState, walkState, () => !isTrapped);
        _stateMachine.AddState(trapState, idleState, () => !isTrapped);


        _stateMachine.SetState(idleState);
    }
    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            isTrapped = true;
        }
        await Task.Delay(1000);
        isTrapped = false;
        Destroy(other.gameObject);
    }

    public void SetNextPatrolPoint()
    {

        if (Target != null)
        {
            PathManager.Instance.ReleasePath(Target);
            Target = null;
        }

        Target = PathManager.Instance.RequestPath();
        navMeshAgent.speed = 1;
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
