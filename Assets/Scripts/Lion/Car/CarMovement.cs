using System;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpforce;
    
    private Rigidbody2D rb;
    private float _horizontal;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        // transform.position += Vector3.right * _horizontal * moveSpeed * Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = _horizontal * moveSpeed;
    }
}
