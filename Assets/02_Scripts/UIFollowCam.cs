using System;
using UnityEngine;

public class UIFollowCam : MonoBehaviour
{
    private Camera _camera;
    public bool lockY;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 dir = _camera.transform.position - transform.position;
        if (lockY) dir.y = 0;
        if (dir != Vector3.zero) transform.rotation = Quaternion.LookRotation(-dir);
    }
}
