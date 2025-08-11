using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    [SerializeField] private List<Sprite> spriteList;
    [SerializeField] private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer.sprite = spriteList[Random.Range(0, spriteList.Count)];
    }
}
