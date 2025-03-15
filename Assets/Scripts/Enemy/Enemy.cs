using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private float speed;

    private NavMeshAgent agent;
    private Animator animator;
    private Rigidbody2D rb;
    private EnemyTypeHolder enemyTypeHolder;
    private EnemiesSO enemyType;
    private HealthSystem healthSystem;
    private SpriteRenderer spriteRenderer;

    private float targetMaxRadius = 10f;
    private Building buildingType;
    private Transform targetTransform;

    private float timerLookMax = 0.2f;
    private float timer;

    private float timerSlashing;
    private float timerSlashingMax = 0.25f;

    private IState currentState;

    [HideInInspector] public bool IsDead;
    [HideInInspector] public bool IsMoving = true;
    [HideInInspector] public bool IsSlashing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        enemyTypeHolder = GetComponent<EnemyTypeHolder>();
        healthSystem = GetComponent<HealthSystem>();
    }
    private void Start()
    {
        enemyType = enemyTypeHolder.enemyType;
        speed = enemyType.speed;

        targetTransform = BuildingManager.Instance.GetBuildingHQ();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentState = new RunningState();
        currentState.EnterState(this, animator);

        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.SetHealthMax(enemyTypeHolder.enemyType, true);
    }

    private void HealthSystem_OnDied()
    {
        IsDead = true;
        IsMoving = false;
        SummaryManager.Instance.AddDiededEnemy(enemyType, 1);
        StartCoroutine(DeadTime());
    }
    public void ChangeState(IState newState)
    {
        currentState = newState;
        currentState.EnterState(this, animator);
    }

    private void Update()
    {
        currentState.UpdateState(this, animator);

        HandleTargeting();
        HandleMovement();
        SetDirection();
    }
    private void HandleTargeting()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            LookForTargets();
            timer += timerLookMax;
        }
    }
    private void HandleMovement()
    {
        if (targetTransform != null && IsMoving)
        {
            //Vector3 moveDir = (targetTransform.position - transform.position).normalized;
            //rb.linearVelocity = moveDir * speed;
            agent.SetDestination(targetTransform.position);
        }
        else
        {
            IsSlashing = false;
        }
    }

    public bool CanSpawn(Vector3 position)
    {
        Collider2D collider = Physics2D.OverlapCircle(position, 3f);

        if (collider == null)
            return true;

        return false;
    }

    private void LookForTargets()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider in collider2DArray)
        {
            buildingType = collider.gameObject.GetComponent<Building>();

            if (buildingType != null)
            {
                // Is a building!
                if (targetTransform == null)
                    targetTransform = buildingType.transform;
                else
                    if (Vector3.Distance(transform.position, buildingType.transform.position) < Vector3.Distance(transform.position, targetTransform.position))
                    targetTransform = buildingType.transform;


                IsMoving = true;
            }
        }
        if (buildingType == null)
        {
            targetTransform = BuildingManager.Instance.GetBuildingHQ();
            IsMoving = true;

            if (targetTransform == null)
                IsMoving = false;

        }

    }
    private void SetDirection()
    {
        if (agent.velocity.x > 0)
            spriteRenderer.flipX = false;
        else if (agent.velocity.x < 0)
            spriteRenderer.flipX = true;
    }
    private IEnumerator DeadTime()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();

        if (building != null)
        {
            timerSlashing -= Time.deltaTime;
            if (timerSlashing < 0)
            {
                IsMoving = false;
                IsSlashing = true;
                HealthSystem healthSystem = building.GetComponent<HealthSystem>();
                healthSystem.Damage(enemyTypeHolder.enemyType.damage);
                timerSlashing += timerSlashingMax;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, targetMaxRadius);
    }
}
