using System;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    public event Action OnCatCatched;

    [Header("References")]
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private Transform PlayerTransform;

    [Header("Settings")]
    [SerializeField] private float DefaultSpeed = 5f;
    [SerializeField] private float ChaseSpeed = 8f;

    [Header("Navigation Settings")]
    [SerializeField] private float PatrolRadius = 10f;
    [SerializeField] private float WaitTime = 2f;
    [SerializeField] private int MaxDestinationAttemps = 10;
    [SerializeField] private float ChaseDistanceThreshold = 1.5f;
    [SerializeField] private float ChaseDistance = 2f;

    private NavMeshAgent CatAgent;
    private CatStateController catStateController;


    private float Timer;
    private bool IsWaiting;
    private bool IsChasing;
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
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play
        && GameManager.Instance.GetCurrentGameState() != GameState.Resume
        && GameManager.Instance.GetCurrentGameState() != GameState.CutScene)
        {
            CatAgent.speed = 0f;
            return;
        }

        if(PlayerController.CanCatChase())
        {
            SetChaseMovement();
        }
        else
        {
            SetPatrolMovement();
        }
    }
    
    private void SetChaseMovement()
    {
        IsChasing = true;
        Vector3 DirectionToPlayer = (PlayerTransform.position - transform.position).normalized;
        Vector3 OffsetPosition = PlayerTransform.position - DirectionToPlayer * ChaseDistanceThreshold;
        CatAgent.SetDestination(OffsetPosition);
        CatAgent.speed = ChaseSpeed;
        catStateController.ChangeState(CatState.Running);

        if (Vector3.Distance(transform.position, PlayerTransform.position) <= ChaseDistance && IsChasing)
        {
            OnCatCatched?.Invoke();
            catStateController.ChangeState(CatState.Attacking);
            IsChasing = false;
        }
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
