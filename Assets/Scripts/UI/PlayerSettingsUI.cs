using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingsUI : MonoBehaviour
{
    [SerializeField] private ColorPickerControl colorPicker;
    [SerializeField] private Image playerImage;

    private void Start()
    {
        colorPicker.OnColorChanged += (object sender, EventArgs e) =>
        {
            playerImage.color = colorPicker.GetColor();
        };
    }
}
