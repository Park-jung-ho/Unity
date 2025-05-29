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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.position += movement * moveSpeed * Time.deltaTime;
        if (movement != Vector3.zero) transform.rotation = Quaternion.LookRotation(movement);
    }
}

}