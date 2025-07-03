using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MoveType { Horizontal, Vertical }
    public MoveType moveType;
    public float theta;
    public float speed;
    public float power;

    private Vector3 initPos;
    void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        theta += speed * Time.deltaTime;
        if (moveType == MoveType.Horizontal) transform.position = new Vector3(initPos.x + power * Mathf.Sin(theta),initPos.y,initPos.z);
        if (moveType == MoveType.Vertical) transform.position = new Vector3(initPos.x, initPos.y + power * Mathf.Sin(theta), initPos.z);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
