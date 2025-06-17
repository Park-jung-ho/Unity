using UnityEngine;

public class FlashLight : MonoBehaviour, IDropItem
{
    public GameObject flashLight;
    
    public void Grab(Transform grapPos)
    {
        transform.SetParent(grapPos);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        Debug.Log("손전등을 주웠다.");
    }

    public void Use()
    {
        flashLight.SetActive(!flashLight.activeSelf);
        string t = flashLight.activeSelf ? "켠다" : "끈다";
        Debug.Log($"손전등을 {t}.");
    }

    public void Drop()
    {
        transform.SetParent(null);
        float rx = Random.Range(-10.0f, 10.0f);
        float rz = Random.Range(-10.0f, 10.0f);
        transform.position = new Vector3(rx, 1, rz);
        Debug.Log("손전등을 버렸다.");
    }
}