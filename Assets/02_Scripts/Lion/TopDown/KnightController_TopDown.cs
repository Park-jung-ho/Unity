using System;
using UnityEngine;

public class KnightController_TopDown : MonoBehaviour
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
    [SerializeField] private float attackDamage; 
    
    
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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
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
    }
    
    void Move()
    {
        rb.linearVelocity = inputDirection * movementSpeed;
    }

    void Jump()
    {
        if (jumpCount <= 0) return;
        jumpCount -= 1;
        rb.linearVelocityY = 0;
        rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
    }

    void Attack()
    {
        if (!isAttacking)
        {
            attackDamage = 3f;
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
            attackDamage = 5f;
            animator.SetTrigger("Combo");
            isCombo = false;
        }
    }
    public void EndCombo()
    {
        isCombo = false;
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log($"Attack Damage : {attackDamage}");
            other.GetComponent<Rigidbody2D>().AddForceY(5f, ForceMode2D.Impulse);
        }
    }
}

// public enum InputType
// {
//     keyboard,
//     joystick,
// }
