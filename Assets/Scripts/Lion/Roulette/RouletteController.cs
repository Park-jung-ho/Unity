using UnityEngine;

public class RouletteController : MonoBehaviour
{
    public float rotationSpeed;
    public float stopPercentage;
    public float delayTime;
    public bool isStop;
    public AnimationCurve curve;
    [SerializeField] private float speed;
    [SerializeField] private float delay;
    void Start()
    {
        speed = 0;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            isStop = false;
            speed = rotationSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isStop = true;
        }

        if (isStop)
        {
            delay += Time.deltaTime;
            if (delay > delayTime)
            {
                speed *= stopPercentage;
                delay = 0;
            }
            if (speed < 10)
            {
                speed = 0;
                isStop = false;
            }
        }
        
    }
}
