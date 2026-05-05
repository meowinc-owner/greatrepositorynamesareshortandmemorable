using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public bool isEquipped = false;
    public string OwnerTag;
    public float angle;
    public SpriteRenderer sr;

    [Header("︻╦̵̵̿╤── Bullet this thing js shot ongod frfr")] 
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float shotCooldown = 1f;
    protected float shotTimer;
    public float damageMult = 1f;

    protected virtual void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
    }

    public virtual void Fire()
    {
        if (shotTimer < shotCooldown)
        {
            return;
        }

        shotTimer = 0;
        CreateBullet();
        /*
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Bullet b = newBullet.GetComponent<Bullet>();
        b.Init(angle, gameObject.tag);
        */
    }

    public abstract void CreateBullet();

    protected virtual void Update()
    {
        shotTimer += Time.deltaTime;
    }
}
