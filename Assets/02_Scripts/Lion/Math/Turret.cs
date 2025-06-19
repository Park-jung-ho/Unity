using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform turretHead;
    public Transform target;
    private float theta;
    public float speed;
    public float rotRange;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (target == null) TurretIdle();
        else TurretFire();
    }

    void TurretIdle()
    {
        theta += Time.deltaTime * speed;
        float angle = Mathf.Sin(theta) * rotRange;
        turretHead.localRotation = Quaternion.Euler(0, angle, 0);
    }

    void TurretFire()
    {
        turretHead.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
        }
    }
}
