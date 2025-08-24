using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingsUI : MonoBehaviour
{
    [SerializeField] private ColorPickerControl colorPicker;
    [SerializeField] private Image playerImage;
    [SerializeField] private UIDrawing drawing;
    [SerializeField] private RawImage faceImage;
    [SerializeField] private Button applyButton;

    private void Start()
    {
        colorPicker.OnColorChanged += (object sender, EventArgs e) =>
        {
            playerImage.color = colorPicker.GetColor();
        };

        drawing.OnDrawingChanged += (object sender, EventArgs e) =>
        {
            faceImage.texture = drawing.GetDrawing();

            faceImage.color = colorPicker.GetInverseColor();
        };

        applyButton.onClick.AddListener(() =>
        {
            SaveChanges();
        });
    }

    private void SaveChanges()
    {

    }
}
