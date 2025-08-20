using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Decoration : MonoBehaviour
{
    [SerializeField] private List<Sprite> spriteList;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer.sprite = spriteList[Random.Range(0, spriteList.Count)];

        spriteRenderer.sortingOrder = Random.Range(-5, -100); // Makes it less likely that two trees next to each other have the same sorting order
    }
}
