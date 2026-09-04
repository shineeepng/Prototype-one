using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    [Header("Intro")]

    Camera cam;

    [SerializeField] GameObject startPosition;
    [SerializeField] GameManager gameManager;
    [SerializeField] float startProjection = 15f;
    [SerializeField] float endProjection = 6f;
    [SerializeField] float introSpeed = 2f;

    Transform player;
    Transform enemy;
    Transform target;

    bool introFinished = false;
    bool goingToPlayer = false;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");

        player = playerObject.transform;
        enemy = enemyObject.transform;

        cam = GetComponent<Camera>();

        // Intro setup
        transform.position = new Vector3(startPosition.transform.position.x, startPosition.transform.position.y, transform.position.z);
        cam.orthographicSize = startProjection;

        target = enemy;
        moveSpeed = introSpeed;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        // Intro
        if (!introFinished)
        {
            if (!goingToPlayer)
            {
                cam.orthographicSize = Mathf.Lerp(
                    cam.orthographicSize,
                    endProjection,
                    introSpeed * Time.deltaTime
                );

                if (Mathf.Abs(cam.orthographicSize - endProjection) < 0.05f)
                {
                    cam.orthographicSize = endProjection;

                    target = player;
                    goingToPlayer = true;
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, player.position) < 0.05f)
                {
                    target = player;
                    moveSpeed = 10f;

                    introFinished = true;

                    gameManager.StartGame();
                }
            }
        }
    }

    public void SetPlayer()
    {
        target = player;
    }

    public void SetEnemy()
    {
        target = enemy;
    }

    public void SetProjectile()
    {
        GameObject bulletObject = GameObject.FindGameObjectWithTag("Bullet");

        if (bulletObject != null)
        {
            target = bulletObject.transform;
        }
    }
}