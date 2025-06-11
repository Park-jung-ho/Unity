using System;
using UnityEngine;
using CatGame;
using TMPro;

namespace CatGame
{


    public class CatController : MonoBehaviour
    {
        public SoundManager soundManager;
        public GameManager gameManager;
        public Transform nameUI;
        public float jumpforce;
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

        void init()
        {
            transform.position = startPos;
            rb.linearVelocity = Vector2.zero;
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
                soundManager.PlayClip(SoundManager.clipType.jumpcClip);
            }
            
            var catRotation = transform.eulerAngles;
            catRotation.z = rb.linearVelocityY * 2.5f;
            transform.eulerAngles = catRotation;
            
            nameUI.position = transform.position + new Vector3(0, 1, 0);
        }

        void gameover()
        {
            gameManager.GameOver();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("gameover") || 
                other.gameObject.CompareTag("pipe"))
            {
                gameover();
                Invoke("init", 2f);
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("apple"))
            {
                other.gameObject.SetActive(false);
                GameObject particle = other.transform.parent.GetChild(2).gameObject;
                particle.gameObject.SetActive(false);
                particle.gameObject.SetActive(true);
                soundManager.PlayClip(SoundManager.clipType.getcClip);
                gameManager.score++;
                gameManager.levelUpPoint--;
            }
        }
    }
}