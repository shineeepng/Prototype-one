using UnityEngine;

public class Fireball : MonoBehaviour
{

    [SerializeField] Transform visual;

    public float power;

    float collisionDelay = 1f;
    float timer;

    Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Bullet: BulletBase is missing a Rigidbody2D!", gameObject);
            return;
        }

        if (!gameObject.GetComponentInChildren<Collider2D>())
        {
            Debug.LogError("Bullet: Bullet is missing a collider!");
        }

        if (visual == null)
        {
            Debug.LogError("Bullet: Visual reference is missing!", gameObject);
        }

        rb.mass = 3f;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        if (!gameObject.CompareTag("Bullet"))
        {
            Debug.LogWarning("Bullet: BulletBase was not tagged 'Bullet'. Setting it automatically.", gameObject);
            gameObject.tag = "Bullet";
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce((Vector2)transform.right * power, ForceMode2D.Impulse);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        rb.mass += 1f * Time.fixedDeltaTime;

        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;

            visual.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {



        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (timer < collisionDelay)
            return;

        if (!collision.gameObject.CompareTag("Ground") && !collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Enemy"))
        {
            Debug.LogWarning("Bullet: Collision object is not tagged 'Ground'!");
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }

}