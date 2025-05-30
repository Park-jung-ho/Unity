using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisGame
{
enum InputKey
{
    moveL,
    moveR,
    rotL,
    rotR,
    moveDown,
}
public class Block : MonoBehaviour
{
    public int BlockID;
    public Spawner spawner;
    public GameObject dropPointBlock;
    [SerializeField] private float initialDelay = 0.3f;
    [SerializeField] private float repeatRate = 0.1f;
    [SerializeField] private float moveTime;
    public bool end = false;
    public Transform[] childblocks;
    
    [SerializeField] private Vector3 currentPosition;
    [SerializeField] private int rotIdx;
    [SerializeField] private float stopTime;
    private float moveTimer;
    private bool timerOn;
    [SerializeField] private bool[] isPressed; // { moveL, moveR, rotL, rotR, moveDown }
    [SerializeField] private bool[] isPressedEndDelay; // { moveL, moveR, rotL, rotR, moveDown }
    [SerializeField] private float[] keyTimer; // { moveL, moveR, rotL, rotR, moveDown }
    [SerializeField] private KeyCode[] keys; // { moveL, moveR, rotL, rotR, moveDown }

    
    public void init()
    {
        isPressed = new bool[5] { false, false, false, false, false };
        isPressedEndDelay = new bool[5] { false, false, false, false, false };
        keyTimer = new float[5] { 0f, 0f, 0f, 0f, 0f };
        keys = new KeyCode[5] { KeyCode.A ,KeyCode.D ,KeyCode.Q ,KeyCode.E ,KeyCode.S };
        rotIdx = 0;
        stopTime = spawner.stopTime;
        end = false;
        dropPointBlock = spawner.DropPointBlock;
        dropPointBlock.SetActive(true);
        rot();
    }
    void Start()
    {
        init();
    }

    void Update()
    {
        if (end)
        {
            return;
        }

        checkInput();
        
        if (IsPressedEndDelay(InputKey.moveL)) moveLR(true);
        if (IsPressedEndDelay(InputKey.moveR)) moveLR(false);
        if (IsPressedEndDelay(InputKey.rotL)) rotLR(true);
        if (IsPressedEndDelay(InputKey.rotR)) rotLR(false);
        if (IsPressedEndDelay(InputKey.moveDown)) moveDown(true);
        if (Input.GetKeyDown(KeyCode.Space)) speedDown();

        moveDown(false);
        

        showDropPoint();
    }

    bool IsPressedEndDelay(InputKey key)
    {
        return isPressedEndDelay[(int)key];
    }

    void checkInput()
    {
        for (int i = 0; i < 5; i++)
        {
            isPressedEndDelay[i] = false;
            if (Input.GetKey(keys[i]))
            {
                if (!isPressed[i])
                {
                    isPressed[i] = true;
                    isPressedEndDelay[i] = true;
                    keyTimer[i] = initialDelay;
                }
                else
                {
                    keyTimer[i] -= Time.deltaTime;
                    if (keyTimer[i] <= 0f)
                    {
                        isPressedEndDelay[i] = true;
                        keyTimer[i] = repeatRate;
                    }
                }
            }
            else
            {
                isPressed[i] = false;
                keyTimer[i] = 0f;
            }
        }
        
    }
    
    IEnumerator stopTimer()
    {
        dropPointBlock.SetActive(false);
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
            dropPointBlock.SetActive(true);
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

    void showDropPoint()
    {
        dropPointBlock.transform.position = transform.position;
        bool canMove = true;
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                int x = (int)dropPointBlock.transform.GetChild(i).position.x;
                int y = (int)dropPointBlock.transform.GetChild(i).position.y - 1;
                
                if (spawner.canMove(x,y) == false)
                {
                    canMove = false;
                    break;
                }
            }

            if (canMove == false)
            {
                break;
            }
            
            dropPointBlock.transform.position += Vector3.down;
        }
    }
    void speedDown()
    {
        while (!timerOn)
        {
            moveDown(true);
        }

        stopTime = 0;
    }
    void moveDown(bool isSoftDrop)
    {
        if (checkNextMove(0, -1) == false)
        {
            if (!timerOn) StartCoroutine(stopTimer());
            
            return;
        }

        if (!isSoftDrop && checkMoveCoolTime() == false) return;

        if (timerOn)
        {
            StopAllCoroutines();
            timerOn = false;
        }
        
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
                    dropPointBlock.transform.GetChild(cnt).localPosition = new Vector3(x, -y, 0);
                    cnt++;
                }
            }
        }
    }
}
    
}
