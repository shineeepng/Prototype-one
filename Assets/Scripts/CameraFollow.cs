using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTarget; // Ссылка на трансформ игрока
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);

    private Transform currentTarget;

    private void Start()
    {
        // При старте следим за игроком
        currentTarget = playerTarget;
    }

    private void LateUpdate()
    {
        // Если цель была уничтожена (патрон сломался), возвращаемся к игроку
        if (currentTarget == null)
        {
            currentTarget = playerTarget;
        }

        if (currentTarget != null)
        {
            Vector3 targetPosition = currentTarget.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }

    // Вызываем этот метод при выстреле, чтобы переключить камеру на патрон
    public void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }
}