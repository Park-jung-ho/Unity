using UnityEngine;

namespace UP_Game
{


public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    public BlockSpawner blockSpawner;
    public SpriteRenderer[] renderers;
    public Vector2 upPos;
    public float cool;
    private bool isGround;
    private int count;
    private bool isDead;

    void init()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        rb.bodyType = RigidbodyType2D.Static;
        count = 0;
        isDead = false;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            up(true);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            up(false);
        }
        else
        {
            renderers[0].gameObject.SetActive(true);
            renderers[1].gameObject.SetActive(false);
        }
    }

    void up(bool left)
    {
        renderers[0].flipX = left;
        renderers[1].flipX = left;
        renderers[0].gameObject.SetActive(false);
        renderers[1].gameObject.SetActive(true);
        
        float x = left ? -upPos.x : upPos.x;
        transform.position += new Vector3(x, upPos.y, 0);

        if (blockSpawner.marked[count] != left)
        {
            isDead = true;
            rb.gravityScale = 1;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.freezeRotation = false;
            rb.AddTorque(1f, ForceMode2D.Impulse);
            Invoke(nameof(init),1f);
            return;
        }

        count++;
    }
}

}
