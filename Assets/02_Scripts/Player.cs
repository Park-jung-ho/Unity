using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    
    public float radius;
    public float degrees;
    public float moveSpeed;
    public float jumpForce;
    public float groundCheckLength;
    
    [SerializeField] private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) degrees -= moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) degrees += moveSpeed * Time.deltaTime;

        CheckGround();
        // Jump();
        Move();
    }

    void CheckGround()
    {
        Debug.DrawRay(transform.position, Vector3.down * groundCheckLength, Color.red);
        if (rb.linearVelocity.y < 0f &&
            Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckLength, LayerMask.GetMask("Block")))
        {
            Debug.Log(hit.collider.gameObject.name);
            isGrounded = true;
        }
    }
    void Move()
    {
        float rad = Mathf.Deg2Rad * degrees;
        Vector3 pos = new Vector3(Mathf.Cos(rad) * radius, transform.position.y, Mathf.Sin(rad) * radius);
        transform.position = pos;
        transform.LookAt(Vector3.zero);
    }
    void Jump()
    {
        Debug.Log("Jump");
        rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Block") && rb.linearVelocity.y <= 0f)
        {
            Jump();
        }
    }
}
