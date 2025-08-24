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
    }
}
