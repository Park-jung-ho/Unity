using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public SpriteRenderer[] renderers;
    public float moveSpeed;
    public float jumpforce;
    public Vector2 upPos;
    [SerializeField] private float moveHorizontal;
    private bool isGround;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        jump();
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     up(true);
        // }
        // else if (Input.GetKeyDown(KeyCode.D))
        // {
        //     up(false);
        // }
        // else
        // {
        //     renderers[0].gameObject.SetActive(true);
        //     renderers[1].gameObject.SetActive(false);
        // }
    }
    
    private void FixedUpdate()
    {
        move();
    }
    
    void move()
    {
        if (!isGround) return;
        
        if (moveHorizontal != 0)
        {
            renderers[0].flipX = moveHorizontal < 0;
            renderers[1].flipX = moveHorizontal < 0;
            renderers[0].gameObject.SetActive(false);
            renderers[1].gameObject.SetActive(true);
            rb.linearVelocityX = moveHorizontal * moveSpeed;
        }
        else
        {
            renderers[0].gameObject.SetActive(true);
            renderers[1].gameObject.SetActive(false);
        }
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            renderers[0].gameObject.SetActive(false);
            renderers[1].gameObject.SetActive(false);
            renderers[2].gameObject.SetActive(true);
            rb.AddForceY(jumpforce, ForceMode2D.Impulse);
        }
    }

    void up(bool left)
    {
        if (left)
        {
            renderers[0].flipX = true;
            renderers[1].flipX = true;
            renderers[0].gameObject.SetActive(false);
            renderers[1].gameObject.SetActive(true);
        }
        else
        {
            renderers[0].flipX = false;
            renderers[1].flipX = false;
            renderers[0].gameObject.SetActive(true);
            renderers[1].gameObject.SetActive(false);
        }
        float x = left ? -upPos.x : upPos.x;
        transform.position += new Vector3(x, upPos.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isGround = true;
        renderers[2].gameObject.SetActive(false);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isGround = false;
        renderers[2].flipX = renderers[1].flipX;
        renderers[0].gameObject.SetActive(false);
        renderers[1].gameObject.SetActive(false);
    }
}
