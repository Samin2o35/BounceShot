using UnityEngine;

public class weaponController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 direction;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(1f, 1f).normalized;
        rb.linearVelocity = direction * speed;
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            Vector2 normal = other.contacts[0].normal;
            direction = Vector2.Reflect(direction, normal);
            rb.linearVelocity = direction * speed;
        }
    }
}
