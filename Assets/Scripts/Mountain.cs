using UnityEngine;

public class Mountain : MonoBehaviour
{
    [SerializeField] private Transform bumpPrefab;
    [SerializeField][Range(0.5f, 10f)] private float minSize = 3f;
    [SerializeField][Range(0.5f, 10f)] private float maxSize = 10f;

    private float spacing;

    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spacing = minSize;

        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Box colliders using auto tiles match the size of the sprite renderer while keeping its own size at 1x1
        Vector2 halfSize = spriteRenderer.size * 0.5f;

        // Local-space corners
        Vector2 localTopLeft = new Vector2(-halfSize.x, halfSize.y) + boxCollider2D.offset;
        Vector2 localTopRight = new Vector2(halfSize.x, halfSize.y) + boxCollider2D.offset;

        // Convert to world-space
        Vector2 worldTopLeft = transform.TransformPoint(localTopLeft);
        Vector2 worldTopRight = transform.TransformPoint(localTopRight);

        float xWidth = worldTopRight.x - worldTopLeft.x;

        for (float i = 0; i < xWidth; i += spacing)
        {
            Transform bump = Instantiate(bumpPrefab, Vector3.Lerp(worldTopLeft, worldTopRight, i / xWidth), Quaternion.identity, transform); // parent to this ground so inspector is more organized

            bump.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));

            SpriteRenderer bumpSpriteRenderer = bump.GetComponent<SpriteRenderer>();

            bumpSpriteRenderer.color = spriteRenderer.color;
            bumpSpriteRenderer.size = new Vector2(Random.Range(minSize, maxSize), Random.Range(minSize, maxSize));
        }
    }
}
