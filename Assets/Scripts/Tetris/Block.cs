using UnityEngine;

namespace TetrisGame
{
public class Block : MonoBehaviour
{
    public float moveTime;
    public bool canMove = true;
    
    [SerializeField] private Vector3 currentPosition;
    private float moveTimer;
    
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) moveLR(true);
        if (Input.GetKeyDown(KeyCode.D)) moveLR(false);
        if (Input.GetKeyDown(KeyCode.Q)) rotLR(true);
        if (Input.GetKeyDown(KeyCode.E)) rotLR(false);
        if (checkMoveCoolTime() && canMove)
        {
            moveDown();
        }
        
    }

    bool checkMoveCoolTime()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0f)
        {
            moveTimer = moveTime;
            return true;
        }
        return false;
    }
    void moveDown()
    {
        currentPosition = transform.position;
        currentPosition.y -= 1;
        transform.position = currentPosition;
    }

    void moveLR(bool left)
    {
        if (left)
        {
            transform.position += Vector3.left;
        }
        else
        {
            transform.position += Vector3.right;
        }
    }

    void rotLR(bool left)
    {
        if (left)
        {
            transform.Rotate(new Vector3(0, 0, 90f), Space.Self);
            // transform.rotation *= Quaternion.Euler(Vector3.forward * 90f);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -90f), Space.Self);
            // transform.rotation *= Quaternion.Euler(Vector3.back * 90f);
        }
    }
    
}
    
}
