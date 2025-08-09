using UnityEngine;

public class Mountain : MonoBehaviour
{
    [SerializeField] private Transform groundPrefab;
    [SerializeField] private float spacing = 5f;
    [SerializeField] private float minSize = 3f;
    [SerializeField] private float maxSize = 10f;

    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        for (float i = 0; i < boxCollider2D.size.x; i += spacing)
        {
            Transform bump = Instantiate(groundPrefab, Vector3.Lerp(new Vector3(boxCollider2D.bounds.min.x, boxCollider2D.bounds.max.y), new Vector3(boxCollider2D.bounds.max.x, boxCollider2D.bounds.max.y), i / boxCollider2D.size.x), Quaternion.identity);

            bump.localScale = new Vector3(Random.Range(minSize, maxSize), Random.Range(minSize, maxSize), 1f);
            bump.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));

            bump.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
        }
    }
}
