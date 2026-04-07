using UnityEngine;

public class Bullet : MonoBehaviour
{
    // tickling variables
    public float speed = 10f;
    public float angle;
    private Rigidbody2D rb;
    public float lifetime = 2f;

    public void Init(float angle)
    {
        this.angle = angle;
        float moveX = speed * Mathf.Cos(Mathf.Deg2Rad * angle);
        float moveY = speed * Mathf.Sin(Mathf.Deg2Rad * angle);
        rb.linearVelocity = new Vector2(moveX, moveY);
        transform.parent = GameObject.Find("Bullets").transform;
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
}
