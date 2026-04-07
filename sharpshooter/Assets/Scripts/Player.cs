using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Soldier
{ 
    //       ⛧°。 ⋆༺♱༻⋆。 °⛧
    [Header("︻╦̵̵̿╤── VARIABLES")] 
    private Camera cam;
    

    [Header("—⟪=====> Input")] 
    
    public InputActionAsset actions;

    private InputAction moveAction;
    private InputAction aimAction;
    private InputAction shootAction;
    private Vector2 mousePos;

    
    // ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘ START FUNCTION ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘
    protected override void Start()
    {
        base.Start();
        moveAction = actions.FindAction("Player/Move");
        aimAction = actions.FindAction("Player/Aim");
        shootAction = actions.FindAction("Player/Attack");
        cam = Camera.main;
        
    }
    
    // ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘ UPDATE ⫘⫘⫘⫘⫘⫘⫘⫘⫘⫘
    protected override void Update()
    {
        mousePos = aimAction.ReadValue<Vector2>();
        movementinput = moveAction.ReadValue<Vector2>();
        isFiring = shootAction.inProgress;
        base.Update();
    }

    void FixedUpdate()
    {
        Move();
        Aim();
    }

    void Move()
    {
        Vector2 desiredVelocity = speed * movementinput;
        float moveDirectionDot = Vector2.Dot(targetVelocity.normalized, desiredVelocity.normalized);
        float remapAcceleration = MathUtils.SmoothStepFromValue(1, 2, MathUtils.Remap(-1, 0, 2, 1, moveDirectionDot, 1, 2));
        float acceleration = 200*remapAcceleration;
        targetVelocity = Vector2.MoveTowards(targetVelocity, desiredVelocity, acceleration*Time.fixedDeltaTime);

        Vector2 ExistingVelocity = rb.linearVelocity;
        Vector2 ForceRequired =  (targetVelocity - ExistingVelocity)/Time.fixedDeltaTime;
        float max = 150*remapAcceleration;
        ForceRequired =  Vector2.ClampMagnitude(ForceRequired, max);
        rb.AddForce(ForceRequired*rb.mass);
    }

    void Aim()
    {
        Vector3 ScreenPosition = cam.WorldToScreenPoint(transform.position);
        Vector2 Direction = (mousePos - (Vector2) ScreenPosition).normalized;
        angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
    }

    private void Shoot()
    {
        if (shotTimer < shotCooldown)
        {
            return;
        }

        shotTimer = 0;
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Bullet b = newBullet.GetComponent<Bullet>();
        b.Init(angle);
    }

    
}
