using System;
using UnityEngine;

public class VisualBlock : MonoBehaviour
{
    public Color[] colors;
    public MeshRenderer renderer;

    private void Awake()
    {
    }

    public void changeColor(int idx)
    {
        renderer.material.color = colors[idx];
    }
    public void changeMat(Material mat)
    {
        renderer.material = mat;
    }
    
}
