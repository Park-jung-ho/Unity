using System;
using UnityEngine;
using IsometricAiming;

namespace IsometricAiming
{
    public class CharacterControl : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotSpeed;
        [SerializeField] private Vector3 moveDirection;
        [SerializeField] private IDropItem currentItem;
        [SerializeField] private Transform grapPos;
        void Start()
        {
        
        }

        void Update()
        {
            if (!GameManager.instance.isStarted) return;
            Move();
            RotToMouse();
            Interaction();
        }

        void Move()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
        
            moveDirection = Quaternion.Euler(new Vector3(0,45,0)) * new Vector3(h, 0, v);
            moveDirection = moveDirection.normalized;
            if (moveDirection == Vector3.zero) return;
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
        }

        void RotToMouse()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.up);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 direction = ray.GetPoint(distance) - transform.position;
                transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            }
        }
        void Interaction()
        {
            if (currentItem == null)
            {
                Debug.Log("Nothing to interact");
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                currentItem.Use();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentItem.Drop();
                currentItem = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IDropItem>() != null)
            {
                var item = other.GetComponent<IDropItem>();
                currentItem = item;
                currentItem.Grab(grapPos);
            }
        }
    }
}