using System;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public float jumpforce;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private int jumpCount;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            jumpCount--;
            rb.AddForceY(jumpforce, ForceMode2D.Impulse);
            animator.SetTrigger("jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 3;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            
        }
    }
}
