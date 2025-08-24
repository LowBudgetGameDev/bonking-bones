using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDrawing : MonoBehaviour
{
    public event EventHandler OnDrawingChanged;

    [SerializeField] private Button resetButton;
    [SerializeField] private int textureWidth = 256;
    [SerializeField] private int textureHeight = 256;
    [SerializeField] private int brushSize = 4;

    private RawImage drawingSurface;

    private Color drawColor = Color.white;
    private Color visibleColor = Color.black; // This is the color that is seen in the drawing

    private Texture2D drawTexture;

    private void Start()
    {
        drawingSurface = GetComponent<RawImage>();

        drawingSurface.color = visibleColor;

        drawTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);

        if (SavedData.HasKey(SavedData.Data.PlayerFace))
        {
            drawTexture = UtilsClass.DecodeStringToTexture2D(SavedData.GetString(SavedData.Data.PlayerFace));

            OnDrawingChanged?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            ClearTexture();
        }

        drawTexture.wrapMode = TextureWrapMode.Clamp;

        drawingSurface.texture = drawTexture;

        resetButton.onClick.AddListener(() =>
        {
            ClearTexture();
        });
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                drawingSurface.rectTransform,
                Input.mousePosition,
                null,
                out localPos
            );

            // Convert to texture space
            Rect rect = drawingSurface.rectTransform.rect;
            float x = (localPos.x - rect.x) / rect.width * textureWidth;
            float y = (localPos.y - rect.y) / rect.height * textureHeight;

            DrawCircle((int)x, (int)y);
        }
    }

    private void DrawCircle(int cx, int cy)
    {
        for (int x = -brushSize; x <= brushSize; x++)
        {
            for (int y = -brushSize; y <= brushSize; y++)
            {
                if (x * x + y * y <= brushSize * brushSize)
                {
                    int px = cx + x;
                    int py = cy + y;
                    if (px >= 0 && px < textureWidth && py >= 0 && py < textureHeight)
                        drawTexture.SetPixel(px, py, drawColor);
                }
            }
        }
        drawTexture.Apply();

        OnDrawingChanged?.Invoke(this, EventArgs.Empty);
    }


    public void ClearTexture()
    {
        Color[] clearColors = new Color[textureWidth * textureHeight];
        for (int i = 0; i < clearColors.Length; i++) clearColors[i] = Color.clear;
        drawTexture.SetPixels(clearColors);
        drawTexture.Apply();
    }

    public Texture2D GetDrawing()
    {
        return drawTexture;
    }
}
