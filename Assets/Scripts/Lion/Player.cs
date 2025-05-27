using UnityEngine;

namespace LionStudy
{


public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;
    void Start()
    {

    }

    void Update()
    {
        transform.localPosition += Vector3.forward * moveSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + rotSpeed * Time.deltaTime, 0);
    }
}

}