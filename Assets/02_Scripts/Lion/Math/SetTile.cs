using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SetTile : MonoBehaviour
{
    public GameObject tilePrefab;
    public Material[] tileMats;
    public int rows, cols;
    public GameObject[] tiles;
    public int[] tileIdxList;
    public float tileFallTime;
    private IEnumerator Start()
    {
        tiles = new GameObject[rows*cols];
        tileIdxList = new int[rows*cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Vector3 tilePos = new Vector3(i, 0, j);
                int idx = i * rows + j;
                tiles[idx] = Instantiate(tilePrefab, tilePos, Quaternion.identity);
                tiles[idx].GetComponent<Renderer>().material = (i+j) % 2 == 0 ? tileMats[0] : tileMats[1];
                tiles[idx].SetActive(false);
                tileIdxList[idx] = idx;
                // yield return null;
            }
        }
        int RandMax = tiles.Length;
        
        for (int i = 0; i < tiles.Length; i++)
        {
            int randNum = Random.Range(0, RandMax);
            int idx = tileIdxList[randNum];
            tileIdxList[randNum] = tileIdxList[RandMax-1];
            tileIdxList[RandMax-1] = idx;
            RandMax--;
            tiles[idx].SetActive(true);
            StartCoroutine(tileSpawnAnimation(tiles[idx]));
            yield return new WaitForSeconds(0.005f);
        }
    }

    IEnumerator tileSpawnAnimation(GameObject tile)
    {
        Vector3 targetPos = tile.transform.position;
        Vector3 startPos = tile.transform.position;
        startPos.y += 10f;
        tile.transform.position = startPos;
        float timer = 0, percent = 0, lerpTime = tileFallTime;
        while (tile.transform.position.y > 0)
        {
            timer += Time.deltaTime;
            percent = timer / lerpTime;
            tile.transform.position = Vector3.Lerp(startPos, targetPos, percent); 
            yield return null;
        }
    }
}
