using UnityEngine;

public class Fireball : MonoBehaviour
{

    [SerializeField] GameObject Aim;
    [SerializeField] Transform visual;

    public float power;
    float basePower = 20f;
    Rigidbody2D rb;

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

            visual.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }

        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }

    }

}