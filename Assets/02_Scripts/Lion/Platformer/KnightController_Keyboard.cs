using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KnightController_Keyboard : MonoBehaviour, IDamageable
{
    // 0 0.5
    // 0.8 1
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    
    [SerializeField] private JoystickController joystickController;
    [SerializeField] private InputType inputType;
    [SerializeField] private Vector3 inputDirection;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int maxJumpCount;
    [SerializeField] private int jumpCount;
    [SerializeField] private float hp; 
    [SerializeField] private float currHp; 
    [SerializeField] private float attackDamage; 
    [SerializeField] private float KnockbackPower;
    
    [Header("UI")]
    [SerializeField] private Image hpBar;
    [SerializeField] private TMP_Text hpText;
    
    
    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private Vector2 checkBoxSize;
    [SerializeField] private float groundCheckDistance;
    
    
    [Header("Collider Size")]
    [SerializeField] private Vector2 idleSize;
    [SerializeField] private Vector2 idleOffset;
    [SerializeField] private Vector2 crouchSize;
    [SerializeField] private Vector2 crouchOffset;
    
    
    private bool isLadder;
    [SerializeField]private bool isGrounded;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isCombo;
    
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        currHp = hp;
        hpText.text = $"{currHp:F0}/{hp}";
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

        ChangeColliderSize();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }
    }

    void ChangeColliderSize()
    {
        if (inputDirection.y < 0)
        {
            col.size = crouchSize;
            col.offset = crouchOffset;
        }
        else
        {
            col.size = idleSize;
            col.offset = idleOffset;
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
        var hit = Physics2D.OverlapBox(transform.position - groundCheckOffset, checkBoxSize,0, groundLayer);
        if (hit != null)
        {
            isGrounded = true;
            jumpCount = maxJumpCount;
        }
        else
        {
            isGrounded = false;
        }
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - groundCheckOffset, checkBoxSize);
    }
    void Move()
    {
        if (inputDirection.x != 0) rb.linearVelocityX = inputDirection.x * movementSpeed;
        if (isLadder && inputDirection.y != 0)
        {
            rb.linearVelocityY = inputDirection.y * movementSpeed;
        }
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // animator.SetBool("IsGround",true);
            // Debug.Log($"[Collision] IsGround: {Time.time}");
            // jumpCount = maxJumpCount;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // animator.SetBool("IsGround",false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log($"Attack Damage : {attackDamage}");
            other.GetComponent<IDamageable>()?.TakeDamage(attackDamage);
            var forceDir = (other.transform.position - transform.position).normalized;
            forceDir += Vector3.up/2;
            other.GetComponent<Rigidbody2D>().AddForce(forceDir * KnockbackPower, ForceMode2D.Impulse);
        }
        if (other.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
            rb.gravityScale = 2f;
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");
        currHp -= damage;
        hpBar.fillAmount = currHp / hp;
        hpText.text = $"{currHp:F0}/{hp}";
        
        if (currHp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        animator.SetTrigger("Death");
        col.enabled = false;
        rb.gravityScale = 0;
    }
}

public enum InputType
{
    keyboard,
    joystick,
}
