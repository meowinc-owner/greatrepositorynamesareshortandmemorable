using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // tickling variables
    public float speed = 10f;
    public float angle;
    private Rigidbody2D rb;
    public float lifetime = 2f;
    private string OwnerTag;
    public float damage = 1f;

    public void Init(float angle, string OwnerTag)
    {
        this.angle = angle;
        float moveX = speed * Mathf.Cos(Mathf.Deg2Rad * angle);
        float moveY = speed * Mathf.Sin(Mathf.Deg2Rad * angle);
        rb.linearVelocity = new Vector2(moveX, moveY);
        transform.parent = GameObject.Find("Bullets").transform;
        this.OwnerTag = OwnerTag;
    }   
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLifetime();
    }

    void CheckLifetime()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject hitObject = other.gameObject;
        if (OwnerTag == "Player" && hitObject.CompareTag("Emememy"))
        {
            Soldier S = hitObject.GetComponent<Enemy>();
            S.takeDamage(damage);
            Destroy(gameObject);
        }else if (OwnerTag == "Emememy" && hitObject.CompareTag("Player"))
        {
            Soldier S = hitObject.GetComponent<Player>();
            S.takeDamage(damage);
            Destroy(gameObject);
        }
        
    }
}
