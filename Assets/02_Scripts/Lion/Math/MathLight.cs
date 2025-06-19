using System;
using UnityEngine;

public class MathLight : MonoBehaviour
{
    public Light light;
    public float theta;
    public float power;
    public float speed;

    private void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        theta += Time.deltaTime * speed;
        light.intensity = Mathf.Sin(theta) * power;
    }
}
