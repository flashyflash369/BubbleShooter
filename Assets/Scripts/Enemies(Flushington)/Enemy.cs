using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Patrol Settings")]
    [SerializeField] private Transform[] patrolPoints;

    [Header("Chase Settings")]
    [SerializeField] private float chaseTime = 3f;


    #region Variables

    private int currentPatrolIndex = 0;
    private Transform currentPatrolTarget;
    private Transform chaseTarget;

    private float chaseTimer;

    private EnemyState currentState;

    public enum EnemyState
    {
        Patrol,
        Chase,
        Captured
    }

    #endregion

    #region UnityLifecycle

    private void Start()
    {
        // Initialize patrol target safely
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            currentPatrolTarget = patrolPoints[currentPatrolIndex];
        }

        ChangeState(EnemyState.Patrol);
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                HandlePatrol();
                break;

            case EnemyState.Chase:
                HandleChase();
                break;

            case EnemyState.Captured:
                // Do nothing when captured
                break;
        }
    }

    private void OnEnable()
    {
        //EventSystem.OnEnemyDetectTriggered += OnPlayerDetected;
       // EventSystem.OnEnemyHit += OnCaptured;
    }

    private void OnDisable()
    {
        //EventSystem.OnEnemyDetectTriggered -= OnPlayerDetected;
        //EventSystem.OnEnemyHit -= OnCaptured;
    }

    #endregion

    #region StateManagement

    /// <summary>
    /// Changes enemy state and handles enter-state logic.
    /// </summary>
    private void ChangeState(EnemyState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case EnemyState.Patrol:
                break;

            case EnemyState.Chase:
                chaseTimer = chaseTime; // Reset chase timer
                break;

            case EnemyState.Captured:
                break;
        }
    }

     /// <summary>
    /// Called when enemy is captured.
    /// Stops all behavior.
    /// </summary>
    private void OnCaptured()
    {
        ChangeState(EnemyState.Captured);
    }

    #endregion

    #region PatrolLogic

    private void HandlePatrol()
    {
        if (currentPatrolTarget == null)
            return;

        MoveTowards(currentPatrolTarget.position);

        // If reached patrol point, go to next
        if (Vector3.Distance(transform.position, currentPatrolTarget.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            currentPatrolTarget = patrolPoints[currentPatrolIndex];
        }
    }

    #endregion

    #region ChaseLogic

    private void HandleChase()
    {
        if (chaseTarget == null)
        {
            ChangeState(EnemyState.Patrol);
            return;
        }

        MoveTowards(chaseTarget.position);

        // Countdown chase timer
        chaseTimer -= Time.deltaTime;

        if (chaseTimer <= 0f)
        {
            ChangeState(EnemyState.Patrol);
        }
    }

    /// <summary>
    /// Moves enemy toward a world position.
    /// </summary>
    private void MoveTowards(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    #endregion

    #region CallableFunctions

    /// <summary>
    /// Called when player is detected.
    /// Switches enemy to Chase state.
    /// </summary>
    public void OnPlayerDetected(Transform playerTransform)
    {
        if (currentState == EnemyState.Captured)
            return;

        chaseTarget = playerTransform;
        ChangeState(EnemyState.Chase);
    }

    /// <summary>
    /// Called when player is hit by enemy.
    /// </summary>
    public void Capture()
    {
        if (currentState == EnemyState.Captured)
            return;

        OnCaptured();
        Debug.Log("Player hit by enemy!");
    }
    #endregion
}
