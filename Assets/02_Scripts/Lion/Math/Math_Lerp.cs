using UnityEngine;

public class Math_Lerp : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    
    [SerializeField] private Vector3 startPos;
    [SerializeField] private float timer, percent, lerpTime;
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;
        percent = timer / lerpTime;
        transform.position = Vector3.Lerp(startPos, target.position, percent );    
    }
}
