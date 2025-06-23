using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private void OnMouseDown()
    {
        Instantiate(SpawnTurret.instance.turrets[SpawnTurret.instance.turretIndex], transform.position, Quaternion.identity);
    }
}
