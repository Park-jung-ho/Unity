using System;
using System.Collections;
using UnityEngine;

namespace TetrisGame
{
public class Block : MonoBehaviour
{
    public int BlockID;
    public Spawner spawner;
    public float moveTime;
    public bool end = false;
    public Transform[] childblocks = new Transform[4];
    
    [SerializeField] private Vector3 currentPosition;
    [SerializeField] private int rotIdx;
    [SerializeField] private float stopTime;
    private float moveTimer;
    private bool timerOn;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childblocks[i] = transform.GetChild(i);
        }
    }

    void Start()
    {
        rotIdx = 1;
        stopTime = spawner.stopTime;
        rotLR(false);
    }

    void Update()
    {
        if (end)
        {
            spawner.mappingNewBlock(childblocks);
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.A)) moveLR(true);
        if (Input.GetKeyDown(KeyCode.D)) moveLR(false);
        if (Input.GetKeyDown(KeyCode.Q)) rotLR(true);
        if (Input.GetKeyDown(KeyCode.E)) rotLR(false);
        if (Input.GetKeyDown(KeyCode.S)) moveDown();
        if (Input.GetKeyDown(KeyCode.Space)) speedDown();
        if (checkMoveCoolTime())
        {
            moveDown();
        }
    }

    IEnumerator stopTimer()
    {
        timerOn = true;
        stopTime = spawner.stopTime;
        while (true)
        {
            stopTime -= Time.deltaTime;
            if (stopTime <= 0)
            {
                end = true;
                timerOn = false;
                break;
            }
            yield return null;
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

    bool checkNextMove(int mx, int my)
    {
        foreach (var childblock in childblocks)
        {
            int x = mx + (int)childblock.position.x;
            int y = my + (int)childblock.position.y;
            if (spawner.canMove(x,y) == false)
            {
                return false;
            }
        }
        return true;
    }

    void speedDown()
    {
        while (!timerOn)
        {
            moveDown();
        }

        stopTime = 0;
    }
    void moveDown()
    {
        if (checkNextMove(0, -1) == false)
        {
            if (timerOn) stopTime = spawner.stopTime;
            else StartCoroutine(stopTimer());
            
            return;
        }

        StopAllCoroutines();
        
        currentPosition = transform.position;
        currentPosition.y -= 1;
        transform.position = currentPosition;
    }

    void moveLR(bool left)
    {
        int v = left == true ? -1 : 1;
        if (checkNextMove(v, 0) == false)
        {
            return;
        }

        if (timerOn) stopTime = spawner.stopTime;
        
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
        int idx = left == true ? -1 : 1;
        rotIdx += idx;
        if (rotIdx < 0)
        {
            rotIdx = 3;
        }
        if (rotIdx > 3)
        {
            rotIdx = 0;
        }

        string rotString = spawner.getRotationString(BlockID, rotIdx);
        if (rotString != null)
        {
            int cnt = 0;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (rotString[(y*4)+x] == '1')
                    {
                        childblocks[cnt].transform.localPosition = new Vector3(x, -y, 0);
                        cnt++;
                    }
                }
            }
        }
    }
    
}
    
}
