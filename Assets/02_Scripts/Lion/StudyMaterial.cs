using UnityEngine;

public class StudyMaterial : MonoBehaviour
{
    public Material mat;
    void Start()
    {
        // GetComponent<MeshRenderer>().material = mat;
        GetComponent<MeshRenderer>().material.color = Color.cyan;
    }

    void Update()
    {
        
    }
}
