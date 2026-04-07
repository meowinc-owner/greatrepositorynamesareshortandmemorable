using UnityEngine;

public class Soldier : MonoBehaviour
{
    //       ⛧°。 ⋆༺♱༻⋆。 °⛧
    [Header("︻╦̵̵̿╤── VARIABLES")] 
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
}
