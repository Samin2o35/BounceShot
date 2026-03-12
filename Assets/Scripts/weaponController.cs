using UnityEngine;

public class weaponController : MonoBehaviour
{
    public float speed = 5f;
    [Range(0f, 45f)]
    public float directionOffsetDegrees = 15f;

    private Rigidbody2D rb;
    private Vector2 direction;
    private bool canMove = false;

    void OnEnable()
    {
        GameManager.onGameStart.AddListener(OnGameStart);
        GameManager.onGameEnd.AddListener(OnGameEnd);
    }

    void OnDisable()
    {
        GameManager.onGameStart.RemoveListener(OnGameStart);
        GameManager.onGameEnd.RemoveListener(OnGameEnd);
    }

    void OnGameStart()
    {
        float offset = Random.Range(-directionOffsetDegrees, directionOffsetDegrees);
        direction = Rotate(new Vector2(1f, 1f).normalized, offset);
        canMove = true;
        rb.linearVelocity = direction * speed;
    }

    void OnGameEnd()
    {
        canMove = false;
        rb.linearVelocity = Vector2.zero;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!canMove) return;

        if (other.gameObject.CompareTag("Wall"))
        {
            Vector2 normal = other.contacts[0].normal;
            direction = Vector2.Reflect(direction, normal);
            rb.linearVelocity = direction * speed;
        }
    }

    Vector2 Rotate(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );
    }
}