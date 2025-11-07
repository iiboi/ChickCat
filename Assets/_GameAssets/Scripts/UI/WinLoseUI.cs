using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject BlackBackgroundObject;
    [SerializeField] private GameObject WinPopup;
    [SerializeField] private GameObject LosePopup;

    [Header("Settings")]
    [SerializeField] private float AnimationDuration = 0.3f;

    private Image BlackBackgroundImage;
    private RectTransform WinPopupTransform;
    private RectTransform LosePopupTransform;

    private void Awake()
    {
        BlackBackgroundImage = BlackBackgroundObject.GetComponent<Image>();
        WinPopupTransform = WinPopup.GetComponent<RectTransform>();
        LosePopupTransform = LosePopup.GetComponent<RectTransform>();
    }

    public void OnGameWin()
    {
        BlackBackgroundObject.SetActive(true);
        WinPopup.SetActive(true);

        BlackBackgroundImage.DOFade(0.75f, AnimationDuration).SetEase(Ease.Linear);
        WinPopupTransform.DOScale(1.5f, AnimationDuration).SetEase(Ease.OutBack);
    }
    
    public void OnGameLose()
    {
        BlackBackgroundObject.SetActive(true);
        LosePopup.SetActive(true);

        BlackBackgroundImage.DOFade(0.75f, AnimationDuration).SetEase(Ease.Linear);
        LosePopupTransform.DOScale(1.5f, AnimationDuration).SetEase(Ease.OutBack);
    }
}
