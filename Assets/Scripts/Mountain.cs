using UnityEngine;

public class Mountain : MonoBehaviour
{
    [SerializeField] private Transform bumpPrefab; // This bump prefab better not have a mountain script on it or this entire game going down
    [SerializeField] private float spacing = 5f;
    [SerializeField] private float minSize = 3f;
    [SerializeField] private float maxSize = 10f;

    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;

    // Make sure this runs after the collider resizing script
    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Vector2 halfSize = boxCollider2D.size * 0.5f;

        // Local-space corners
        Vector2 localTopLeft = new Vector2(-halfSize.x, halfSize.y) + boxCollider2D.offset;
        Vector2 localTopRight = new Vector2(halfSize.x, halfSize.y) + boxCollider2D.offset;

        // Convert to world-space
        Vector2 worldTopLeft = transform.TransformPoint(localTopLeft);
        Vector2 worldTopRight = transform.TransformPoint(localTopRight);

        for (float i = 0; i < boxCollider2D.size.x; i += spacing)
        {
            Transform bump = Instantiate(bumpPrefab, Vector3.Lerp(worldTopLeft, worldTopRight, i / boxCollider2D.size.x), Quaternion.identity);

            bump.localScale = new Vector3(Random.Range(minSize, maxSize), Random.Range(minSize, maxSize), 1f);
            bump.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));

            bump.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
        }
    }
}
