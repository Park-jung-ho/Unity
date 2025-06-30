using System;
using UnityEngine;

public class PushPlatform : MonoBehaviour
{
    public float power;
    private Animator animator;
    private Rigidbody2D targetRb;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PushPlayer()
    {
        targetRb.AddForceY(power, ForceMode2D.Impulse);
        animator.SetTrigger("Push");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetRb = other.GetComponent<Rigidbody2D>();
            Invoke("PushPlayer",1f);
        }
    }
}
