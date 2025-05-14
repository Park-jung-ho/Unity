using UnityEngine;

public class Player : MonoBehaviour
{
    public float radius;
    public float moveSpeed;
    public float degrees;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) degrees -= moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) degrees += moveSpeed * Time.deltaTime;
        float rad = Mathf.Deg2Rad * degrees;
        Vector3 pos = new Vector3(Mathf.Cos(rad) * radius, 0, Mathf.Sin(rad) * radius);
        transform.position = pos;
        transform.LookAt(Vector3.zero);
    }
}
