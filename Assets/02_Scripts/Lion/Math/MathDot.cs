using System;
using UnityEngine;

public class MathDot : MonoBehaviour
{
    public Vector3 vecA;
    public Vector3 vecB;
    void Start()
    {
        
    }

    private void Update()
    {
        resultDot();
    }

    public void resultDot()
    {
        var result = Vector3.Dot(vecA, vecB);
        var result2 = Vector3.Angle(vecA, vecB);
        var result3 = Vector3.Cross(vecA, vecB);
        var result4 = Vector3.Reflect(vecA, vecB);
        Debug.Log($"Vector3.Dot : {result}");
        Debug.Log($"Vector3.Angle : {result2}");
        Debug.Log($"Vector3.Cross : {result3}");
        Debug.Log($"Vector3.Reflect : {result4}");
    }
    
}
