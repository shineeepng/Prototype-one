using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject GameObject;

    public float Angle = 0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Angle += Input.GetAxisRaw("Vertical") * Time.deltaTime;
    }
}
