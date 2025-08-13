using UnityEngine;

public class Mountain : MonoBehaviour
{
    [Header("Bump Info")]
    [SerializeField] private Transform bumpPrefab;
    [SerializeField][Range(0.5f, 10f)] private float minSize = 3f;
    [SerializeField][Range(0.5f, 10f)] private float maxSize = 10f;

    [Header("Decoration Info")]
    [SerializeField] private Transform decorationPrefab;
    [SerializeField][Range(0.5f, 3f)] private float decorMinSize = 0.75f;
    [SerializeField][Range(0.5f, 3f)] private float decorMaxSize = 1.5f;
    [SerializeField][Range(0.01f, 1f)] private float decorChance = 0.1f;

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
            bumpSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder;

            float randomFloat = Random.Range(0f, 1f);

            if (randomFloat < decorChance)
            {
                float bumpX = bump.position.x;
                float bumpY = bump.position.y;

                int layerMask = 1 << bump.gameObject.layer;
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(bumpX, bumpY + 50f), Vector2.down, 50f, layerMask); // Raise up the start some amount so we know its above

                // Minus some part in the y so the decoration doesn't appear to be floating
                Transform decor = Instantiate(decorationPrefab, hit.point + Vector2.down * 0.75f, Quaternion.identity, transform);

                decor.localScale = Vector3.one * Random.Range(decorMinSize, decorMaxSize);
            }
        }
    }
}
