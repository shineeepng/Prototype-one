using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player playerScript;
    [SerializeField] Enemy enemyScript;
    [SerializeField] CameraFollow cameraFollow;

    public enum Turn
    {
        Player,
        Enemy
    }

    public Turn currentTurn;

    bool waitingForProjectile = false;

    void Start()
    {
        currentTurn = Turn.Player;

        playerScript.enabled = false;
        enemyScript.enabled = false;
    }

    void Update()
    {
        if (waitingForProjectile)
        {
            GameObject bullet = GameObject.FindGameObjectWithTag("Bullet");

            if (bullet == null)
            {
                waitingForProjectile = false;

                if (currentTurn == Turn.Player)
                {
                    StartEnemyTurn();
                }
                else
                {
                    StartPlayerTurn();
                }
            }
        }
    }

    public void EndPlayerTurn()
    {
        playerScript.enabled = false;


        cameraFollow.SetProjectile();

        waitingForProjectile = true;
    }

    public void EndEnemyTurn()
    {
        enemyScript.enabled = false;

        playerScript.Angle = 0f;
        playerScript.Aim.transform.rotation = Quaternion.Euler(0f, 0f, 0f);


        cameraFollow.SetProjectile();

        waitingForProjectile = true;
    }

    void StartEnemyTurn()
    {
        currentTurn = Turn.Enemy;

        enemyScript.enabled = true;

        cameraFollow.SetEnemy();

        enemyScript.TakeTurn();

        Debug.Log("ENEMY TURN STARTED");
    }

    void StartPlayerTurn()
    {
        currentTurn = Turn.Player;

        playerScript.enabled = true;

        cameraFollow.SetPlayer();

        Debug.Log("PLAYER TURN STARTED");
    }

    public void StartGame()
    {
        playerScript.enabled = true;

        Debug.Log("GAME STARTED");
    }
}