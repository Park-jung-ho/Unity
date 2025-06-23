using System;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTurret : MonoBehaviour
{
    public static SpawnTurret instance;
    public GameObject[] turrets;
    public Button[] buttons;
    public int turretIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            int v = i;
            buttons[i].onClick.AddListener(()=> ChangeIdx(v));
        }
    }

    void ChangeIdx(int idx)
    {
        turretIndex = idx;
    }
}
