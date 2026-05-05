using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float left;
    private float top;
    private float width;
    private float height;
    private SpriteRenderer sr;
    void Start()
    {
        transform.parent = GameObject.Find("Walls").transform;
        sr = GetComponent<SpriteRenderer>();
        width = sr.bounds.size.x;
        height = sr.bounds.size.y;
        left = transform.position.x - width / 2;
        top = transform.position.y + height / 2; 
    }

    public Vector2 GetNearestPoint(Vector2 position)
    {
        Vector2 nearest = new Vector2();
        // figuring out the x value of nearest pt
        if (left > position.x)
        {
            nearest.x = left;
        }else if (left + width < position.x)
        {
            nearest.x = left + width;
        }
        else
        {
            nearest.x = position.x;
        }
        // figuring out the y value of nearest pt
        if (top < position.y)
        {
            nearest.y = top;
        }else if (top - height > position.y)
        {
            nearest.y = top - height;
        }
        else
        {
            nearest.y = position.y;
        }
        return nearest; // return nearest;
    }

    public Vector2 GetNormal(Vector2 position)
    {
        Vector2 nearest = GetNearestPoint(position);
        Vector2 normal = position - nearest;
        if (normal == Vector2.zero)
        {
            return normal;
        }

        float l = Mathf.Sqrt(normal.x * normal.x + normal.y * normal.y);
        normal /= l;
        return normal;
    } 
}
