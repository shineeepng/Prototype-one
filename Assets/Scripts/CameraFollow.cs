using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform defaultTarget;
    public Vector3 offset = new Vector3(0f, 2f, -10f);
    public float smoothSpeed = 8f;

    private Transform currentTarget;

    private void Start()
    {
        currentTarget = defaultTarget;
    }

    private void LateUpdate()
    {
        if (currentTarget == null)
        {
            currentTarget = defaultTarget;
            if (currentTarget == null) return;
        }

        Vector3 desiredPosition = currentTarget.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }
}