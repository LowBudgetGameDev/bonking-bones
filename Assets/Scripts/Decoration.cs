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

        ShadowCaster2D shadowCaster = spriteRenderer.gameObject.AddComponent<ShadowCaster2D>(); // Create shadow caster in code because attaching to prefabs makes it stop working when instantiated
    }
}
