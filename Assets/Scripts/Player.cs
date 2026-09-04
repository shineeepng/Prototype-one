using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject FireballPrefab;
    public GameObject Aim;
    [SerializeField] LineRenderer Trajectory;
    [SerializeField] GameObject FireballSpawn;
    [SerializeField] GameManager GameManager;
    [SerializeField] GameObject Gun;


    public float Angle = 0f;
    public float sensitivity = 15;
    float Power = 1f;
    bool readyAngle = false;
    float speed = 20f;
    float direction = 1f;


    void Awake()
    {

        gameObject.tag = "Player";

        Aim.transform.localPosition = Vector3.zero;
        Gun.transform.localPosition = new Vector3 (0.4f, 0, 0);
        
        if (!gameObject.GetComponent<Collider2D>())  
        {
            Debug.LogError("Player: Collider is missing!");
        }
    }

    void Update()
    {
        
        if (!readyAngle)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                readyAngle = true;
                Trajectory.enabled = true;
            }
            
            Angle = Mathf.Clamp(Angle + Input.GetAxisRaw("Vertical") * sensitivity * Time.deltaTime, -45, 80);

            Aim.transform.rotation = Quaternion.Euler(0f, 0f, Angle);
        }
        
        else if (readyAngle)
        {
            float maxPower = 100f;
            float minPower = 1f;

            float t = (Power - minPower) / (maxPower - minPower);

            // Make it slow at both ends and fast in the middle
            float curve = Mathf.Sin(t * Mathf.PI);

            speed = Mathf.Lerp(7f, 100f, curve);

            Power += direction * speed * Time.deltaTime;

            if (Power >= maxPower)
            {
                Power = maxPower;
                direction = -1f;
            }

            if (Power <= minPower)
            {
                Power = minPower;
                direction = 1f;
            }

            DrawTrajectory();

            if (Input.GetKeyDown(KeyCode.F))
            {
                readyAngle = false;
                FireballPrefab.GetComponent<Fireball>().power = Power;
                Instantiate(FireballPrefab, FireballSpawn.transform.position, Aim.transform.rotation);
                Trajectory.enabled = false;

                Power = 1f;
                direction = 1f;

                GameManager.EndPlayerTurn();
            }
        }

        

        
    }
    void DrawTrajectory()
    {
        int points = 7;
        float timeStep = 0.04f;

        Trajectory.positionCount = points;

        Vector2 startPosition = FireballSpawn.transform.position;

        Rigidbody2D fireballRb = FireballPrefab.GetComponent<Rigidbody2D>();

        float mass = fireballRb.mass;

        Vector2 startVelocity = (Vector2)Aim.transform.right * (Power / mass);

        for (int i = 0; i < points; i++)
        {
            float time = i * timeStep;

            Vector2 point = startPosition + startVelocity * time + 0.5f * Physics2D.gravity * time * time;

            Trajectory.SetPosition(i, point);
        }
    }
}
