using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private float DefaultSpeed = 5f;
    [SerializeField] private float ChaseSpeed = 8f;

    [Header("Navigation Settings")]
    [SerializeField] private float PatrolRadius = 10f;
    [SerializeField] private float WaitTime = 2f;
    [SerializeField] private int MaxDestinationAttemps = 10;
    private NavMeshAgent CatAgent;
    private CatStateController catStateController;


    private float Timer;
    private bool IsWaiting;
    private Vector3 InitialPosition;

    private void Awake()
    {
        CatAgent = GetComponent<NavMeshAgent>();
        catStateController = GetComponent<CatStateController>();
    }

    private void Start()
    {
        InitialPosition = transform.position;
        SetRandomDestination();
    }
    
    private void Update() 
    {
        SetPatrolMovement();
    }

    private void SetPatrolMovement()
    {
        CatAgent.speed = DefaultSpeed;

        if (!CatAgent.pathPending && CatAgent.remainingDistance <= CatAgent.stoppingDistance)
        {
            if (!IsWaiting)
            {
                IsWaiting = true;
                Timer = WaitTime;
                catStateController.ChangeState(CatState.Idle);
            }
        }

        if (IsWaiting)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0f)
            {
                IsWaiting = false;
                SetRandomDestination();
                catStateController.ChangeState(CatState.Walking);
            }
        }
    }

    private void SetRandomDestination()
    {
        int attemps = 0;
        bool destinationset = false;

        while (attemps < MaxDestinationAttemps && !destinationset)
        {
            Vector3 RandomDirection = UnityEngine.Random.insideUnitSphere * PatrolRadius;
            RandomDirection += InitialPosition;

            if (NavMesh.SamplePosition(RandomDirection, out NavMeshHit hit, PatrolRadius, NavMesh.AllAreas))
            {
                Vector3 FinalPosition = hit.position;

                if (!IsPositionBlocked(FinalPosition))
                {
                    CatAgent.SetDestination(FinalPosition);
                    destinationset = true;
                }
                else
                {
                    attemps++;
                }
            }
            attemps++;
        }
        if(!destinationset)
        {
            Debug.Log("Failed to find a valid destination!");
            IsWaiting = true;
            Timer = WaitTime * 2;
        }
    }

    private bool IsPositionBlocked(Vector3 position)
    {
        if (NavMesh.Raycast(transform.position, position, out NavMeshHit hit, NavMesh.AllAreas))
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = (InitialPosition != Vector3.zero) ? InitialPosition : transform.position;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, PatrolRadius);
    }
}
