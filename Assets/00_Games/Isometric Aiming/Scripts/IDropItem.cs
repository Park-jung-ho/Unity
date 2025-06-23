using UnityEngine;
using IsometricAiming;

namespace MyNamespace
{
    public interface IDropItem
    {
        public void Grab(Transform grapPos);
        public void Use();
        public void Drop();
    
    }
}