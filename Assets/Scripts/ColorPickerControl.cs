using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Code from https://www.youtube.com/watch?v=otDHGmncBQY

public class ColorPickerControl : MonoBehaviour
{
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
        CreateHueImage();
        CreateSVImage();

        hueSlider.onValueChanged.AddListener((float value) =>
        {
            UpdateSVImage(value);
        });
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
        currentHue = 0;

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
        currentSaturation = 0;
        currentValue = 0;

        svImage.texture = svTexture;
    }

    public void SetSV(float saturation, float value)
    {
        currentSaturation = saturation;
        currentValue = value;
    }

    public void UpdateSVImage(float value)
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
    }
}
