using UnityEngine;

public class WhileLoop : MonoBehaviour
{
    public int count;
    void Start()
    {
        do
        {
            Debug.Log(++count);
        } while (count < 7);
    }

    
}
