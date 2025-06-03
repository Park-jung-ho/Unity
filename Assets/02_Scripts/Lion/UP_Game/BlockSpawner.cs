using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UP_Game
{
public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform root;
    public Transform player;
    public TMP_Text scoreText;
    public float YPosition;
    public float XPosition;
    [SerializeField] private Vector2 lastPos;
    [SerializeField] private int spawnCount;
    public List<bool> marked = new List<bool>();

    void Start()
    {

    }

    void Update()
    {
        scoreText.text = player.GetComponent<CharacterController>().count.ToString();
        if (lastPos.y - player.position.y <= 30)
        {
            spawnCount = 30;
        }

        if (spawnCount > 0)
        {
            spawnBlock(Random.Range(0, 2) == 0);
            spawnCount--;
        }
    }

    void spawnBlock(bool left)
    {
        float x = left ? -XPosition : XPosition;
        Vector3 pos = lastPos + new Vector2(x, YPosition);
        lastPos = pos;
        GameObject block = Instantiate(blockPrefab, pos, Quaternion.identity, root);
        marked.Add(left);
    }
}
}
