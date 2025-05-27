using UnityEngine;

public class Material_LoopMap : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float offsetSpeed;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Vector2 offset = Vector2.right * (offsetSpeed * Time.fixedDeltaTime);
        meshRenderer.material.SetTextureOffset("_MainTex", meshRenderer.material.mainTextureOffset + offset);
    }
}
