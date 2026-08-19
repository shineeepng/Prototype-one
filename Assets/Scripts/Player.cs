using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject GameObject;
    [SerializeField] GameObject Fireballtest;
    [SerializeField] GameObject Aim;

    public float Angle = 0f;
    public float sensitivity = 15;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Angle = Mathf.Clamp(Angle + Input.GetAxisRaw("Vertical") * sensitivity * Time.deltaTime, -45, 80);

        GameObject.transform.rotation = Quaternion.Euler(0f, 0f, Angle);

        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(Fireballtest, transform.position, Aim.transform.rotation);
        }
        
    }
}
