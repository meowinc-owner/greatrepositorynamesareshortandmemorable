using UnityEngine;

public class Soldier : MonoBehaviour
{
    //       ⛧°。 ⋆༺♱༻⋆。 °⛧
    [Header("︻╦̵̵̿╤── VARIABLES")] protected Vector2 aimPosition;
    public float speed = 10f;
    public float angle = 0f;
    protected Rigidbody2D rb;
    protected bool isFiring = false;
    protected Vector2 movementinput;
    protected Vector2 targetVelocity = Vector2.zero;
    
    [Header("︻╦̵̵̿╤── Bullet this thing js shot ongod frfr")] 
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float shotCooldown = 1f;
    protected float shotTimer;
    
    
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        shotTimer += Time.deltaTime;
        if (isFiring)
        {
            Shoot();
        }
    }
    protected virtual void FixedUpdate()
    {
        Move();
        Aim();
    }
    
    protected virtual void Move()
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

    protected virtual void Aim()
    { 
        Vector2 Direction = (aimPosition - (Vector2) transform.position).normalized;
        angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
    }

    protected virtual void Shoot()
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
