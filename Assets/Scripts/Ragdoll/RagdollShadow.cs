using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RagdollShadow : MonoBehaviour
{
    [SerializeField] private int[] sortingLayers;

    private void Awake()
    {
        ShadowCaster2D shadowCaster = gameObject.AddComponent<ShadowCaster2D>();

        // Thank you Mr. Reddit Man https://www.reddit.com/r/Unity2D/comments/re3v26/setting_target_sorting_layers_of_shadowcaster2d/
        // And the Unity code (I just had a little "inspiration") (It was just for the layer ids)

        var fieldInfo = typeof(ShadowCaster2D).GetField("m_ApplyToSortingLayers", BindingFlags.Instance | BindingFlags.NonPublic);

        int[] layerIds = new int[sortingLayers.Length];

        for (int layerIndex = 0; layerIndex < sortingLayers.Length; layerIndex++)
        {
            layerIds[layerIndex] = SortingLayer.layers[sortingLayers[layerIndex]].id;
        }

        fieldInfo.SetValue(shadowCaster, layerIds);
    }
}
