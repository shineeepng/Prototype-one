using UnityEngine;

public class Fireball : MonoBehaviour
{

    [SerializeField] GameObject Aim;
    public float power = 20f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = transform.right * power;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
