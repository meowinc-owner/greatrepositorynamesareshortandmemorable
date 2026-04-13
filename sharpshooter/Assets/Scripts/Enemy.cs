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
        angle = Random.Range(0f, 360f);
        movementinput = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        aimPosition = (Vector2)transform.position - movementinput; // FIX LATER, CHANGE - TO +
    }

    // Update is called once per frame
    protected override void Update()
    {
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
                break;
        }
        base.Update();
    }

    private void WANDER()
    {
        movementinput = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        aimPosition = (Vector2)transform.position - movementinput; // FIX LATER, CHANGE - TO +
        wanderTimer += Time.deltaTime;
        if (wanderTimer > currentWanderTime)
        {
            wanderTimer = 0f;
            NewAngle = Random.Range(0f, 360f);
            OldAngle = angle;
            float diff = Mathf.DeltaAngle(OldAngle, NewAngle);
            TurnTime = TurnSpeed / Mathf.Abs(diff);
            movementinput = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            currentWanderTime =  Random.Range(minWanderTime, maxWanderTime);
            state = EnemyState.IDLE;
        }
    }
    
    private void IDLE()
    {
        movementinput = Vector2.zero;
        PauseTimer += Time.deltaTime;
        if (PauseTimer > PauseTime)
        {
            PauseTimer = 0f; 
            state = EnemyState.TURN;
        }
    }
    private void TURN()
    {
        movementinput = Vector2.zero;
        aimPosition = (Vector2)transform.position - new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        TurnTimer += Time.deltaTime;
        if (TurnTimer > TurnTime)
        {
            TurnTimer = 0f; 
            state = EnemyState.WANDER;
        }
    }
}
