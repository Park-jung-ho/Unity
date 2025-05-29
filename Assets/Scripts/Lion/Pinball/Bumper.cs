using System;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        transform.localScale += new Vector3(0.2f, 0.2f);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        transform.localScale -= new Vector3(0.2f, 0.2f);
    }
}
