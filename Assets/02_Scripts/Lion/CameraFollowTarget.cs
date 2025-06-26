using System;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public enum type
    {
        update,
        lateUpdate,
        fixedUpdate,
    }
    [SerializeField] private Transform target;
    public type followType;
    public Vector3 offset;
    public float smoothSpeed;
    
    [SerializeField] private Vector2 minBound;
    [SerializeField] private Vector2 maxBound;

    void Update()
    {
        if (followType == type.update)
        {
            transform.position = target.position + offset;
        }
    }

    private void FixedUpdate()
    {
        if (followType == type.fixedUpdate)
        {
            transform.position = target.position + offset;
        }
    }

    private void LateUpdate()
    {
        if (followType == type.lateUpdate)
        {
            Vector3 destination = target.position + offset;
            Vector3 smoothPos = Vector3.Lerp(transform.position, destination, Time.deltaTime * smoothSpeed);
            smoothPos.x = Mathf.Clamp(smoothPos.x, minBound.x, maxBound.x);
            smoothPos.y = Mathf.Clamp(smoothPos.y, minBound.y, maxBound.y);
            transform.position = smoothPos;
        }
    }
}
