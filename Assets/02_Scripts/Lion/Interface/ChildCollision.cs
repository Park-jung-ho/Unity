using System;
using UnityEngine;
using IsometricAiming;
public class ChildCollision : MonoBehaviour
{
    [SerializeField] private Archery root;
    
    private void OnCollisionEnter(Collision other)
    {
        root.HandleChildCollisionEnter(other);
    }
}
