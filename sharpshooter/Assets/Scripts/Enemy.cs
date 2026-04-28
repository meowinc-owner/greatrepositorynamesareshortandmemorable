using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : Soldier
{
    public float minWanderTime = 0f;
    public float maxWanderTime = 2.5f;
    private float currentWanderTime = 0f;
    private float wanderTimer;

    public float IdleTime = 0.5f;
    private float IdleTimer = 0f;
    public float TurnSpeed = 15f;
    private float TurnTimer = 0f;
    private float TurnTime = 0f;
    private float OldAngle;
    private float NewAngle;
    private float targetAngle;
    public float visionRange = 5.6f;
    public Transform target;

    private enum EnemyState
    {
        WANDER = 0,
        IDLE,
        TURN,
        GETOVERHERE
    }

    private EnemyState state = EnemyState.WANDER;
    protected override void Start()
    {
        base.Start();
        currentWanderTime =  Random.Range(minWanderTime, maxWanderTime);
        targetAngle = Random.Range(0f, 360f);
        movementinput = new Vector2(Mathf.Cos(targetAngle * Mathf.Deg2Rad), Mathf.Sin(targetAngle * Mathf.Deg2Rad));
        aimPosition = (Vector2)transform.position + movementinput;
        target = GameObject.FindGameObjectWithTag("Player").transform; //change if we add co-op
        transform.parent = GameObject.Find("Enemies").transform;
    }

    // Update is called once per frame
    protected override void Update()
    {
        CheckVisionRange();
        switch (state)
        {
            case EnemyState.WANDER:
                WANDER();
                break;
            case EnemyState.IDLE:
                IDLE();
                break;
            case EnemyState.TURN:
                TURN();
                break;
            case EnemyState.GETOVERHERE:
                chase();
                break;
        }
        base.Update();
    }

    private void WANDER()
    {
        movementinput = new Vector2(Mathf.Cos(targetAngle * Mathf.Deg2Rad), Mathf.Sin(targetAngle * Mathf.Deg2Rad));
        aimPosition = (Vector2)transform.position + movementinput;
        wanderTimer += Time.deltaTime;
        if (wanderTimer > currentWanderTime)
        {
            wanderTimer = 0f;
            NewAngle = Random.Range(0f, 360f);
            OldAngle = targetAngle;
            float diff = Mathf.DeltaAngle(OldAngle, NewAngle);
            TurnTime = Mathf.Abs(diff) / TurnSpeed;
            movementinput = new Vector2(Mathf.Cos(targetAngle * Mathf.Deg2Rad), Mathf.Sin(targetAngle * Mathf.Deg2Rad));
            currentWanderTime =  Random.Range(minWanderTime, maxWanderTime);
            state = EnemyState.IDLE;
        }
    }
    
    private void IDLE()
    {
        movementinput = Vector2.zero;
        IdleTimer += Time.deltaTime;
        if (IdleTimer > IdleTime)
        {
            IdleTimer = 0f; 
            state = EnemyState.TURN;
        }
    }
    private void TURN()
    { 
        movementinput = Vector2.zero;
        aimPosition = (Vector2)transform.position + new Vector2(Mathf.Cos(targetAngle * Mathf.Deg2Rad), Mathf.Sin(targetAngle * Mathf.Deg2Rad));
        targetAngle = Mathf.LerpAngle(OldAngle, NewAngle, TurnTimer / TurnTime);
        TurnTimer += Time.deltaTime;
        if (TurnTimer > TurnTime)
        {
            TurnTimer = 0f; 
            state = EnemyState.WANDER;
        }
    }

    private void CheckVisionRange()
    {
        if (target == null)
        {
            return;
            
        }
        Vector3 toTarget = target.position - transform.position;
        toTarget.z = 0f;
        float distance = toTarget.magnitude;
        if (distance <= visionRange)
        {
            state = EnemyState.GETOVERHERE;
        }
        else if (state == EnemyState.GETOVERHERE)
        {
            isFiring = false;
            state = EnemyState.IDLE;
            TurnTimer = 0f;
            wanderTimer = 0f;
            IdleTimer = 0f;
            NewAngle = Random.Range(0f, 360f);
            OldAngle = angle;
            float diff = Mathf.DeltaAngle(OldAngle, NewAngle);
            TurnTime = Mathf.Abs(diff) / TurnSpeed;
            currentWanderTime =  Random.Range(minWanderTime, maxWanderTime);
            targetAngle = angle;
        }
    }

    private void chase()
    {
        Vector3 toTarget = target.position - transform.position;
        toTarget.z = 0f;
        float distance = toTarget.magnitude;
        aimPosition = (Vector2)transform.position + (Vector2)toTarget.normalized;
        isFiring = true;
        if (distance <= visionRange / 3)
        {
            movementinput = -(Vector2)toTarget.normalized;
        }
        else
        {
            movementinput = (Vector2)toTarget.normalized;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
