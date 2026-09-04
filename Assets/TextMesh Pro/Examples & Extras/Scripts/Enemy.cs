using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] GameObject FireballPrefab;
    public GameObject Aim;
    [SerializeField] GameObject FireballSpawn;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject Gun;



    float thinkingTime = 2f;
    float angleDeviation = 2f;
    float powerDeviation = 2f;
    float angleLockSpeed = 50f;


    float minAngle = 0f;
    float maxAngle = 70f;
    int minPower = 1;
    int maxPower = 100;

    float bestAngle;
    float bestPower;
    float bestDistance;

    float targetAngle;
    float targetPower;

    public float currentAngle = 0f;
    float currentPower = 1f;

    float powerDirection = 1f;

    float calculationTimer = 0f;

    enum State
    {
        Thinking,
        LockingAngle,
        ChargingPower
    }

    State state;

    void Awake()
    {
        gameObject.tag = "Enemy";

        Aim.transform.localPosition = Vector3.zero;
        Aim.transform.localEulerAngles = new Vector3(0, 0, 180f);

        Gun.transform.localPosition = new Vector3(0.4f, 0, 0);

        if (!gameObject.GetComponent<Collider2D>())
        {
            Debug.LogError("Enemy: No collider detected!");
        }
    }

    void Start()
    {
        state = State.Thinking;
    }


    void Update()
    {
        if (state == State.Thinking)
        {
            Think();
        }
        else if (state == State.LockingAngle)
        {
            LockAngle();
        }
        else if (state == State.ChargingPower)
        {
            ChargePower();
        }
    }


    void Think()
    {
        calculationTimer += Time.deltaTime;

        if (calculationTimer >= thinkingTime)
        {
            CalculateBestShot();

            // Add slight randomness AFTER finding the optimal shot
            targetAngle = bestAngle + Random.Range(-angleDeviation, angleDeviation);
            targetPower = bestPower + Random.Range(-powerDeviation, powerDeviation);

            targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);
            targetPower = Mathf.Clamp(targetPower, minPower, maxPower);

            Debug.Log(
                "Optimal: Angle = " + bestAngle +
                " Power = " + bestPower +
                " Distance = " + bestDistance +
                " | Final: Angle = " + targetAngle +
                " Power = " + targetPower
            );

            state = State.LockingAngle;
        }
    }


    void LockAngle()
    {
        currentAngle = Mathf.MoveTowards(
            currentAngle,
            targetAngle,
            angleLockSpeed * Time.deltaTime
        );

        Aim.transform.rotation = Quaternion.Euler(
            0f,
            0f,
            180f - currentAngle
        );

        if (Mathf.Approximately(currentAngle, targetAngle))
        {
            currentPower = minPower;
            powerDirection = 1f;

            state = State.ChargingPower;
        }
    }


    void ChargePower()
    {
        float max = 100f;
        float min = 1f;

        float t = (currentPower - min) / (max - min);

        float curve = Mathf.Sin(t * Mathf.PI);

        float speed = Mathf.Lerp(7f, 100f, curve);

        currentPower += powerDirection * speed * Time.deltaTime;

        if (currentPower >= max)
        {
            currentPower = max;
            powerDirection = -1f;
        }

        if (currentPower <= min)
        {
            currentPower = min;
            powerDirection = 1f;
        }

        // Only fire while power is increasing.
        if (powerDirection > 0f && currentPower >= targetPower)
        {
            currentPower = targetPower;

            Shoot();
        }
    }


    void Shoot()
    {
        Aim.transform.rotation = Quaternion.Euler(
            0f,
            0f,
            180f - targetAngle
        );

        Fireball fireballScript = FireballPrefab.GetComponent<Fireball>();

        fireballScript.power = targetPower;

        Instantiate(
            FireballPrefab,
            FireballSpawn.transform.position,
            Aim.transform.rotation
        );


        gameManager.EndEnemyTurn();
    }


    void CalculateBestShot()
    {
        Rigidbody2D fireballRb = FireballPrefab.GetComponent<Rigidbody2D>();

        float mass = fireballRb.mass;

        Vector2 startPosition = FireballSpawn.transform.position;
        Vector2 targetPosition = Player.position;

        bestDistance = Mathf.Infinity;
        bestAngle = 0f;
        bestPower = 1f;

        float timeStep = 0.04f;
        float maxTime = 4f;

        /*
         * Test every integer angle.
         */
        for (int angle = (int)minAngle; angle <= (int)maxAngle; angle++)
        {
            /*
             * Test every integer power.
             */
            for (int power = minPower; power <= maxPower; power++)
            {
                float radians = angle * Mathf.Deg2Rad;

                /*
                 * Enemy shoots LEFT.
                 *
                 * 0 degrees = left
                 * Positive angle = up-left
                 */
                Vector2 direction = new Vector2(
                    -Mathf.Cos(radians),
                    Mathf.Sin(radians)
                );

                /*
                 * Same velocity calculation as your
                 * Player DrawTrajectory().
                 */
                Vector2 startVelocity = direction * (power / mass);

                /*
                 * Simulate the projectile.
                 */
                for (float time = 0f; time <= maxTime; time += timeStep)
                {
                    Vector2 point =
                        startPosition +
                        startVelocity * time +
                        0.5f * Physics2D.gravity * time * time;

                    /*
                     * Find the actual distance between the
                     * simulated projectile and the player.
                     */
                    float distance = Vector2.Distance(
                        point,
                        targetPosition
                    );

                    /*
                     * This shot is better than the previous
                     * best shot.
                     */
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        bestAngle = angle;
                        bestPower = power;
                    }

                    /*
                     * Once the projectile has gone well below
                     * the player, there's no point simulating it.
                     */
                    if (point.y < targetPosition.y - 10f &&
                        time > 0.5f)
                    {
                        break;
                    }
                }
            }
        }
    }

    public void TakeTurn()
    {
        calculationTimer = 0f;
        state = State.Thinking;
    }
}