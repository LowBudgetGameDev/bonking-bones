using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

// Code from https://www.youtube.com/watch?v=otDHGmncBQY

public class ColorPickerControl : MonoBehaviour
{
    public event EventHandler OnColorChanged;

    [SerializeField] private RawImage hueImage;
    [SerializeField] private RawImage svImage;

    [SerializeField] private Slider hueSlider;

    [SerializeField] private TMP_InputField hexInputField;

    private Texture2D hueTexture;
    private Texture2D svTexture;

    private float currentHue;
    private float currentSaturation;
    private float currentValue;

    private void Start()
    {
        SetDefaultValues();
        CreateHueImage();
        CreateSVImage();

        UpdateColor();

        hueSlider.onValueChanged.AddListener((float value) =>
        {
            UpdateSVImage(value);
        });

        hexInputField.onEndEdit.AddListener((string value) =>
        {
            OnTextInput(value);
        });
    }

    private void SetDefaultValues()
    {
        ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("PlayerColor", "FFFFFF"), out Color currentColor);
        Color.RGBToHSV(currentColor, out currentHue, out currentSaturation, out currentValue);
    }

    private void CreateHueImage()
    {
        hueTexture = new Texture2D(1, 16);
        hueTexture.wrapMode = TextureWrapMode.Clamp;
        hueTexture.name = "HueTexture";

        for (int i = 0; i < hueTexture.height; i++)
        {
            hueTexture.SetPixel(0, i, Color.HSVToRGB((float) i / hueTexture.height, 1f, 1f));
        }

        hueTexture.Apply();

        hueImage.texture = hueTexture;
    }

    private void CreateSVImage()
    {
        svTexture = new Texture2D(16, 16);
        svTexture.wrapMode = TextureWrapMode.Clamp;
        svTexture.name = "SVTexture";

        for (int y = 0; y < svTexture.height; y++)
        {
            for (int x = 0; x < svTexture.width; x++)
            {
                svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, (float) x / svTexture.width, (float) y / svTexture.height));
            }
        }

        svTexture.Apply();

        svImage.texture = svTexture;
    }

    public void SetSV(float saturation, float value)
    {
        currentSaturation = saturation;
        currentValue = value;

        UpdateColor();
    }

    private void UpdateSVImage(float value)
    {
        currentHue = value;

        for (int y = 0; y < svTexture.height; y++)
        {
            for (int x = 0; x < svTexture.width; x++)
            {
                svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, (float)x / svTexture.width, (float)y / svTexture.height));
            }
        }

        svTexture.Apply();

        UpdateColor();
    }

    private void OnTextInput(string value)
    {
        if (value.Length < 6) return;

        Color colorFromInput;

        if (ColorUtility.TryParseHtmlString("#" + value, out colorFromInput))
        {
            Color.RGBToHSV(colorFromInput, out currentHue, out currentSaturation, out currentValue);
        }

        hueSlider.value = currentHue;

        hexInputField.text = "";

        UpdateColor();
    }

    private void UpdateColor()
    {
        hexInputField.text = ColorUtility.ToHtmlStringRGB(GetColor());

        OnColorChanged?.Invoke(this, EventArgs.Empty);
    }

    public Color GetColor()
    {
        return Color.HSVToRGB(currentHue, currentSaturation, currentValue);
    }

    // A gray scale color that is opposite in value so it is visible when drawing with
    public Color GetInverseColor()
    {
        return Color.HSVToRGB(0f, 0f, 1 - currentValue);
    }
}
