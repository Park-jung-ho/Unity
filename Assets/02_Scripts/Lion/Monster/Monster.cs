using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float hp;
    public float moveSpeed;
    private Vector3 moveDirection;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    private void OnMouseDown()
    {
        Hit(1);
    }

    void Move()
    {
        if (transform.position.x > 8f)
        {
            moveDirection = Vector3.left;
            sr.flipX = true;
        }

        if (transform.position.x < -8f)
        {
            moveDirection = Vector3.right;
            sr.flipX = false;
        }
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        
    }

    void Hit(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log($"{name} Dead");
            Destroy(gameObject);
        }
    }
}
