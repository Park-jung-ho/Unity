using System;
using UnityEngine;
using CatGame;

namespace CatGame
{


    public class CatController : MonoBehaviour
    {
        public float jumpforce;
        public SoundManager soundManager;
        public GameManager gameManager;
        private Rigidbody2D rb;
        private Animator animator;
        private Vector2 startPos;
        [SerializeField] private int jumpCount;

        void Start()
        {
            startPos = transform.position;
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
            {
                jumpCount--;
                rb.AddForceY(jumpforce, ForceMode2D.Impulse);
                if (rb.linearVelocityY > jumpforce + 2f) rb.linearVelocityY = jumpforce + 2f;
                animator.SetBool("ground", false);
                animator.SetTrigger("jump");
                soundManager.OnJumpSound();
            }
            
            Debug.Log(rb.linearVelocityY);
            var catRotation = transform.eulerAngles;
            catRotation.z = rb.linearVelocityY * 2.5f;
            transform.eulerAngles = catRotation;
        }

        void gameover()
        {
            gameManager.GameOver();
            transform.position = startPos;
            rb.linearVelocity = Vector2.zero;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("gameover"))
            {
                gameover();
                return;
            }

            if (other.gameObject.CompareTag("Ground"))
            {
                jumpCount = 3;
                animator.SetBool("ground", true);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                animator.SetBool("ground", false);
            }
        }
    }
}