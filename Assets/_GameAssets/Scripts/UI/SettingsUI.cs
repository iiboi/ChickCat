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

    [Header("Settings")]
    [SerializeField] private float AnimationDuration;

    private Image BlackBackgroundImage;

    private void Awake() 
    {
        BlackBackgroundImage = BlackBackgroundObject.GetComponent<Image>();
        SettingsPopupObject.transform.localScale = Vector3.zero;

        SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
        ResumeButton.onClick.AddListener(OnResumeButtonClicked);

        MainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);
        });
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
