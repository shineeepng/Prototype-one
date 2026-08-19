using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject GameObject;
    [SerializeField] GameObject Fireballtest;
    [SerializeField] GameObject Aim;

    public float Angle = 0f;
    public float sensitivity = 15;
    public float Power = 1f;
    bool readyToShoot = false;
    float speed = 50f;
    float direction;
    float acceleration = 20f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (readyToShoot == false)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                readyToShoot = true;
            }
            
            Angle = Mathf.Clamp(Angle + Input.GetAxisRaw("Vertical") * sensitivity * Time.deltaTime, -45, 80);

            GameObject.transform.rotation = Quaternion.Euler(0f, 0f, Angle);
        }
        
        if (readyToShoot)
        {
            Power += direction * speed * Time.deltaTime;
            speed += acceleration * Time.deltaTime;

            if (Power >= 100f)
            {
                Power = 100f;
                direction = -1f;
            }

            if (Power <= 1f)
            {
                Power = 1f;
                direction = 1f;
                speed = 5f;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                readyToShoot = false;
                Instantiate(Fireballtest, transform.position, Aim.transform.rotation);
                Fireballtest.GetComponent<Fireball>();
            }
        }

        

        
    }
}
