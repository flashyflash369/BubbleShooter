using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Properties
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f; // Speed of the enemy
    [SerializeField] private Transform[] patrolPoints; // Patrol points array
    private int currentPatrolIndex = 0; // Current patrol point index
    // private bool canMove = true; // Whether the enemy can move
    // private bool canPursue = false; // Whether the enemy can pursue

    private Transform targetPoint; // Current target patrol point
    private Transform chasePoint; // Target to chase

    [SerializeField] private float chaseTime = 0;
    [SerializeField] private bool chasing = true;

    private bool isCaptured = false;

    private EnemyState currentEnemyState;

    //Enum
    public enum EnemyState
    {
        canMove,
        canPursue,
        captured,
    }

    // Methods
    private void Start()
    {
        // Initialize patrol point if available
        if (patrolPoints.Length > 0)
        {
            SetTargetPatrolPoint();
        }
        
        //Initialize EnemyState @ beginning
        currentEnemyState = EnemyState.canMove;
    }

    void CountChase()
    {
        chasing = false;
    }

    private void Update()
    {
        ReturnToPatrol();

        if (currentEnemyState == EnemyState.canMove)
        {
            Patrol();
        }
        else if (currentEnemyState == EnemyState.canPursue)
        {
            Pursue(chasePoint);
        }
        else if (currentEnemyState == EnemyState.captured)
        {
            //Do nothing
        }

    }

    private void OnEnable()
    {
        // Subscribe to the event
        EventSystem.OnEnemyDetectTriggered += HandleEnemyTriggered;
        EventSystem.OnEnemyHit += HandleEnemyHitTrigger;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        EventSystem.OnEnemyDetectTriggered -= HandleEnemyTriggered;
        EventSystem.OnEnemyHit -= HandleEnemyHitTrigger;
    }

    private void HandleEnemyTriggered(Transform playerTransform)
    {
        Debug.Log("Player Detected: " + playerTransform);
        if(!isCaptured)
         {
           currentEnemyState = EnemyState.canPursue;
         }
        chasing = true;
        chasePoint = playerTransform; // Assign the player's transform as the chase target
    }

    //CAPTURED
    private void HandleEnemyHitTrigger()
    {
        currentEnemyState = EnemyState.captured;
        isCaptured = true;
    }

    // Move the enemy towards the current patrol point
    private void Patrol()
    {
        if (targetPoint == null) return;

        MoveTowardsTarget(targetPoint);

        // Check if the enemy has reached the patrol point
        if (HasReachedTarget())
        {
            UpdatePatrolPoint();
        }
    }

    //Chase the player on detect
    #region ChaseThePlayerCode
    private void Pursue(Transform chasePoint)
    {
        if (chasePoint == null) return;

        // Move towards the player's position
        transform.position = Vector3.MoveTowards(
            transform.position,
            chasePoint.position,
            moveSpeed * Time.deltaTime
        );

        //Timer for the chasetime to send it to zero
        Invoke("CountChase",chaseTime);
        //change chasing bool to false

        // // Optionally rotate towards the player
        // Vector3 direction = (chasePoint.position - transform.position).normalized;
        // Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
    }

    // Move towards the given target point
    private void MoveTowardsTarget(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    // Check if the enemy has reached the current target point
    private bool HasReachedTarget()
    {
        return Vector3.Distance(transform.position, targetPoint.position) < 0.1f;
    }

    // Update the current patrol point and set the next target
    private void UpdatePatrolPoint()
    {
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        SetTargetPatrolPoint();
    }

    // Set the target patrol point based on the current index
    private void SetTargetPatrolPoint()
    {
        targetPoint = patrolPoints[currentPatrolIndex];
    }
    #endregion

    //Stop chasing after someTime
    private void ReturnToPatrol()
    {
        //When chase time ends, return to patrol
        if (!chasing & !isCaptured)
        {
            currentEnemyState = EnemyState.canMove;
        }
    }


}
