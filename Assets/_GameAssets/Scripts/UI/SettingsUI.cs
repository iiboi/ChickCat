using System;
using DG.Tweening;
using MaskTransitions;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject SettingsPopupObject;
    [SerializeField] private GameObject BlackBackgroundObject;

    [Header("Buttons")]
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button MusicButton;
    [SerializeField] private Button SoundButton;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button MainMenuButton;

    [Header("Sprites")]
    [SerializeField] private Sprite MusicActiveSprite;
    [SerializeField] private Sprite MusicPassiveSprite;
    [SerializeField] private Sprite SoundActiveSprite;
    [SerializeField] private Sprite SoundPassiveSprite;

    [Header("Settings")]
    [SerializeField] private float AnimationDuration;

    private Image BlackBackgroundImage;

    [SerializeField] private bool IsMusicActive;
    [SerializeField] private bool IsSoundActive;

    private void Awake() 
    {
        BlackBackgroundImage = BlackBackgroundObject.GetComponent<Image>();
        SettingsPopupObject.transform.localScale = Vector3.zero;

        IsMusicActive = true;
        IsSoundActive = true;

        SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
        ResumeButton.onClick.AddListener(OnResumeButtonClicked);

        MainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);
        });

        MusicButton.onClick.AddListener(OnMusicButtonClicked);
        SoundButton.onClick.AddListener(OnSoundButtonClicked);
    }

    private void OnSoundButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        IsSoundActive = !IsSoundActive;
        SoundButton.image.sprite = IsSoundActive ? SoundActiveSprite : SoundPassiveSprite;
        AudioManager.Instance.SetSoundEffectsMute(!IsSoundActive);
    }

    private void OnMusicButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        IsMusicActive = !IsMusicActive;
        MusicButton.image.sprite = IsMusicActive ? MusicActiveSprite : MusicPassiveSprite;
        BackgroundMusic.Instance.SetMusicMute(!IsMusicActive);
    }

    private void OnSettingsButtonClicked()
    {
        GameManager.Instance.ChangeGameState(GameState.Pause);
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        BlackBackgroundObject.SetActive(true);
        SettingsPopupObject.SetActive(true);

        BlackBackgroundImage.DOFade(0.75f, AnimationDuration).SetEase(Ease.Linear);
        SettingsPopupObject.transform.DOScale(1.5f, AnimationDuration).SetEase(Ease.OutBack);
    }

    private void OnResumeButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        BlackBackgroundImage.DOFade(0f, AnimationDuration).SetEase(Ease.Linear);
        SettingsPopupObject.transform.DOScale(0f, AnimationDuration).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            GameManager.Instance.ChangeGameState(GameState.Resume);
            BlackBackgroundObject.SetActive(false);
            SettingsPopupObject.SetActive(false);
        });
    }
}