using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject DoorLock;
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (DoorLock != null)
            {
                DoorLock.SetActive(true);
                DoorLock.transform.parent.GetComponent<NumberKeyPad>().doorAnim = animator;
            }
            // animator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Close");

        }
    }
}
