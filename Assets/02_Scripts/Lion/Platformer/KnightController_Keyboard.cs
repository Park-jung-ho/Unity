using System;
using UnityEngine;

public class KnightController_Keyboard : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    
    [SerializeField] private JoystickController joystickController;
    [SerializeField] private InputType inputType;
    [SerializeField] private Vector3 inputDirection;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int maxJumpCount;
    [SerializeField] private int jumpCount;
    [SerializeField] private float groundCheckDistance;
    
    
    private bool isGrounded;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isCombo;
    
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        joystickController.jumpButton.onClick.AddListener(Jump);
        joystickController.attackButton.onClick.AddListener(Attack);
    }

    void Update()
    {
        GetInput();
        IsGrounded();
        setAnimation();
    }

    void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        switch (inputType)
        {
            case InputType.keyboard:
                InputKeyboard();
                break;
            case InputType.joystick:
                InputJoystick();
                break;
        }
    }
    void InputJoystick()
    {
        inputDirection = joystickController.direction;
        if (inputDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (inputDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        
    }
    void InputKeyboard()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (horizontal != 0 || vertical != 0)
        {
            inputDirection = new Vector3(horizontal, vertical, 0);
        }
        else
        {
            inputDirection = Vector3.zero;
        }
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }
    }

    void setAnimation()
    {
        animator.SetFloat("JoystickX", inputDirection.x);
        animator.SetFloat("JoystickY", inputDirection.y);
        // animator.SetBool("Run", inputDirection.x != 0);
        animator.SetBool("IsGround",isGrounded);
    }
    void IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, groundCheckDistance);
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            if (!isGrounded) Debug.Log($"[Ray] IsGround: {Time.time}");
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    void Move()
    {
        rb.linearVelocityX = inputDirection.x * movementSpeed;
    }

    void Jump()
    {
        if (jumpCount <= 0 && animator.GetBool("IsGround") == false) return;
        jumpCount -= 1;
        rb.linearVelocityY = 0;
        rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
    }

    void Attack()
    {
        if (!isAttacking)
        {
            animator.SetTrigger("Attack");
            isAttacking = true;
        }
        else
        {
            isCombo = true;
        }
    }

    public void CheckCombo()
    {
        if (isCombo)
        {
            animator.SetTrigger("Combo");
            isCombo = false;
        }
    }
    public void EndCombo()
    {
        isCombo = false;
        isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // animator.SetBool("IsGround",true);
            Debug.Log($"[Collision] IsGround: {Time.time}");
            jumpCount = maxJumpCount;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // animator.SetBool("IsGround",false);
        }
    }
}

public enum InputType
{
    keyboard,
    joystick,
}
