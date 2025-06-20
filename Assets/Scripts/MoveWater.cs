using UnityEngine;

public class MoveWater : MonoBehaviour
{

    [Header("Scroll speed (X = horizontal, Y = vertical)")]
    public Vector2 scrollSpeed = new Vector2(0.1f, 0.1f);

    private Renderer rend;
    private Vector2 offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
        offset = Vector2.zero;
    }

    void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        rend.material.mainTextureOffset = offset;
    }
}
