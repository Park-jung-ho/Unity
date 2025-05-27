using UnityEngine;

public class Transform_LoopMap : MonoBehaviour
{
    public Transform[] LoopObjects;
    public float moveSpeed;
    public float minPosition;
    public float maxPosition;

    void Update()
    {
        foreach (Transform loopObject in LoopObjects)
        {
            loopObject.position += Vector3.left * moveSpeed * Time.fixedDeltaTime;
            if (loopObject.position.x <= minPosition)
            {
                loopObject.position = new Vector2(maxPosition, loopObject.position.y);
            }
        }
    }
}
