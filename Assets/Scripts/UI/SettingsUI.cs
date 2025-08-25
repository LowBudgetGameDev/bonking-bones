using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("Tabs")]
    [SerializeField] private Button videoAudioTabButton;
    [SerializeField] private Button playerTabButton;

    [Header("Close Button")]
    [SerializeField] private Button closeButton;

    [Header("Main Menu Button")]
    [SerializeField] private Button mainMenuButton;

    [Header("Optional")]
    [SerializeField] private Button openButton;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (openButton != null)
        {
            openButton.onClick.AddListener(() =>
            {
                Show();
                SoundManager.Instance.PlaySound(SoundManager.Sound.UIPress);
                openButton.gameObject.SetActive(false);
            });
        }

        mainMenuButton.onClick.AddListener(() =>
        {
            GameSceneManager.ChangeScene(GameSceneManager.Scene.MainMenuScene);
            SoundManager.Instance.PlaySound(SoundManager.Sound.UIPress);
        });

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

        animator.SetBool("IsClosed", false);

        videoAudioTabButton.gameObject.GetComponent<UITab>().Select();
        playerTabButton.gameObject.GetComponent<UITab>().Unselect();
    }

    public void ShowPlayer()
    {
        gameObject.SetActive(true);

        animator.SetBool("IsClosed", false);

        videoAudioTabButton.gameObject.GetComponent<UITab>().Unselect();
        playerTabButton.gameObject.GetComponent<UITab>().Select();
    }

    public void Hide()
    {
        animator.SetBool("IsClosed", true);

        FunctionTimer.Create(() =>
        {
            gameObject.SetActive(false);
            openButton?.gameObject.SetActive(true);
        }, 0.5f);
    }
}
