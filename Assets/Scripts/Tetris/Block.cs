using System;
using System.Collections;
using System.Collections.Generic;
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
        rotLR(true);
    }

    void Update()
    {
        if (end)
        {
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
                break;
            }
            yield return null;
        }
        timerOn = false;
        if (checkNextMove(0, -1))
        {
            end = false;
            yield break;
        }
        spawner.mappingNewBlock(childblocks);
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
        int prevIdx = rotIdx;
        rotIdx += idx;
        if (rotIdx < 0)
        {
            rotIdx = 3;
        }
        if (rotIdx > 3)
        {
            rotIdx = 0;
        }
        
        rot();
        if (checkCanRot(prevIdx, rotIdx) == false)
        {
            rotIdx = prevIdx;
            rot();
        }
        
    }

    bool checkCanRot(int before, int after)
    {
        List<Vector2Int> data = spawner.getWallKicksData(before, after, BlockID == 0 ? true : false);
        foreach (var offset in data)
        {
            if (checkNextMove(offset.x, offset.y))
            {
                transform.position += new Vector3(offset.x, offset.y);
                return true;
            }
        }
        
        return false;
    }

    void rot()
    {
        string rotString = spawner.getRotationString(BlockID, rotIdx);
        if (rotString == null) return;
        
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
