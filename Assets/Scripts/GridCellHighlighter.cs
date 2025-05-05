using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridCellHighlighter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color highlightColor = Color.white;
    private Color originalColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
    //When the mouse enters the collider area
    void OnMouseEnter()
    {
        spriteRenderer.color = highlightColor;
    }
    //When the mouse exits the collider area
    void OnMouseExit()
    {
        spriteRenderer.color = originalColor;
    }
}