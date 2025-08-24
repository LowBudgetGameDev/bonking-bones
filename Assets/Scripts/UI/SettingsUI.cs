using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("Tabs")]
    [SerializeField] private Button videoAudioTabButton;
    [SerializeField] private Button playerTabButton;

    [Header("Close Button")]
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        videoAudioTabButton.onClick.AddListener(() =>
        {
            videoAudioTabButton.gameObject.GetComponent<UITab>().Select();
            playerTabButton.gameObject.GetComponent<UITab>().Unselect();
            SoundManager.Instance.PlaySound(SoundManager.Sound.UIPress);
        });

        playerTabButton.onClick.AddListener(() =>
        {
            videoAudioTabButton.gameObject.GetComponent<UITab>().Unselect();
            playerTabButton.gameObject.GetComponent<UITab>().Select();
            SoundManager.Instance.PlaySound(SoundManager.Sound.UIPress);
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
            SoundManager.Instance.PlaySound(SoundManager.Sound.UIPress);
        });

        videoAudioTabButton.gameObject.GetComponent<UITab>().Select();
        playerTabButton.gameObject.GetComponent<UITab>().Unselect();

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        videoAudioTabButton.gameObject.GetComponent<UITab>().Select();
        playerTabButton.gameObject.GetComponent<UITab>().Unselect();
    }

    public void ShowPlayer()
    {
        gameObject.SetActive(true);

        videoAudioTabButton.gameObject.GetComponent<UITab>().Unselect();
        playerTabButton.gameObject.GetComponent<UITab>().Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
