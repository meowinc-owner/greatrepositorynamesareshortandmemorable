using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isEquipped = false;
    public string OwnerTag = "";
    public float angle;
    public SpriteRenderer sr;

    [Header("︻╦̵̵̿╤── Bullet this thing js shot ongod frfr")]
    public GameObject bulletPrefab;

    public Transform bulletSpawn;
    public float shotCooldown = 1f;
    protected float shotTimer;
    public float damageMult = 1f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

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
    }

    public void CreateBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Bullet b = newBullet.GetComponent<Bullet>();
        b.damage *= damageMult;
        b.Init(angle, OwnerTag);
    }

    protected virtual void Update()
    {
        shotTimer += Time.deltaTime;
    }
    
    private void OnDestroy()
    {
        GameManager.weapons.Remove(gameObject);
    }
}