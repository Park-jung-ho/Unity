using System;
using UnityEngine;

public class BoardCheckBlock : MonoBehaviour
{
    public Color[] colors;
    
    private MeshRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    public void changeColor(int idx)
    {
        renderer.material.color = colors[idx];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            renderer.material.color = colors[1];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            renderer.material.color = colors[0];
        }
    }
}
