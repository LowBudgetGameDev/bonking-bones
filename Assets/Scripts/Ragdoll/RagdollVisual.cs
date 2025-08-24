using System.Collections.Generic;
using UnityEngine;

public class RagdollVisual : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> bodySpriteRendererList;
    [SerializeField] private SpriteRenderer faceSpriteRenderer;

    private void Awake()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("PlayerColor", "FFFFFF"), out Color playerColor);

        foreach (SpriteRenderer spriteRenderer in bodySpriteRendererList)
        {
            spriteRenderer.color = playerColor;
        }

        Texture2D face = null;
        if (PlayerPrefs.HasKey("PlayerFace"))
        {
            face = UtilsClass.DecodeStringToTexture2D(PlayerPrefs.GetString("PlayerFace"));
        }

        float pixelsPerUnit = face.width / 1f; // This makes the face (which is a square texture) fit into the face of size 0.5 x 0.5 units^2. Don't ask why its dividing by 1 idk either.

        Sprite faceSprite = Sprite.Create(
            face,
            new Rect(0, 0, face.width, face.height),
            new Vector2(0.5f, 0.5f),
            pixelsPerUnit
        );

        faceSpriteRenderer.sprite = faceSprite;

        Color.RGBToHSV(playerColor, out float hue, out float saturation, out float value);

        faceSpriteRenderer.color = Color.HSVToRGB(0f, 0f, 1 - value);
    }
}
