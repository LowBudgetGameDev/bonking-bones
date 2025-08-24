using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button customizeButton;

    [Header("Menus")]
    [SerializeField] private SettingsUI settingsUI;

    [Header("Player Image")]
    [SerializeField] private Image playerImage;
    [SerializeField] private RawImage playerFaceImage;

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            GameSceneManager.ChangeScene(GameSceneManager.Scene.MainScene);
            SoundManager.Instance.PlaySound(SoundManager.Sound.UIPress);
        });

        settingsButton.onClick.AddListener(() =>
        {
            settingsUI.Show();
            SoundManager.Instance.PlaySound(SoundManager.Sound.UIPress);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
            SoundManager.Instance.PlaySound(SoundManager.Sound.UIPress);
        });

        customizeButton.onClick.AddListener(() =>
        {
            settingsUI.ShowPlayer();
            SoundManager.Instance.PlaySound(SoundManager.Sound.UIPress);
        });

        settingsUI.OnClose += (object sender, EventArgs e) =>
        {
            SetPlayerImage();
        };

        SetPlayerImage();
    }

    private void SetPlayerImage()
    {
        ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("PlayerColor", "FFFFFF"), out Color playerColor);

        playerImage.color = playerColor;

        Texture2D face = null;
        if (PlayerPrefs.HasKey("PlayerFace"))
        {
            face = UtilsClass.DecodeStringToTexture2D(PlayerPrefs.GetString("PlayerFace"));
        }

        playerFaceImage.texture = face;

        Color.RGBToHSV(playerColor, out float hue, out float saturation, out float value);

        playerFaceImage.color = Color.HSVToRGB(0f, 0f, 1 - value);
    }
}
