using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public Transform Sun;
    public float fast;
    public float rotSpeed;
    public float revolutionSpeed;
    
    void Update()
    {
        transform.Rotate(Vector3.up * (rotSpeed * fast * Time.deltaTime));
        transform.RotateAround(Sun.position, Vector3.up, revolutionSpeed * fast * Time.deltaTime);
    }
}
