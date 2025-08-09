using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class SpriteColliderResizer : MonoBehaviour
{
    // This script will only work with box colliders because I can't change the size of a collider2d.

    private void Awake()
    {
        Vector2 spriteSize = GetComponent<SpriteRenderer>().size;

        GetComponent<BoxCollider2D>().size = spriteSize;
    }
}
