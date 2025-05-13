using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float radius;
    public float degrees;
    public int spawncount;
    public float offset;
    
    public GameObject[] spawnObjects;
    void Start()
    {
        spawnObjects = new GameObject[spawncount];
    }

    public void spawnButton()
    {
        spawn360(prefab, spawncount, degrees);
        Debug.Log("Spawn");
    }
    void spawn360(GameObject obj, float count, float degrees)
    {
        for (int i = 0; i < count; i++)
        {
            float rad = Mathf.Deg2Rad * degrees * i;
            Vector3 pos = new Vector3(Mathf.Cos(rad) * offset, 0, Mathf.Sin(rad) * offset);
            GameObject newObj = Instantiate(prefab, pos, Quaternion.identity);
            Destroy(spawnObjects[i]);
            spawnObjects[i] = newObj;
        }
    }
    void Update()
    {
        
    }
}
