using DG.Tweening;
using TMPro;
using UnityEngine;

public class EggCounterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text EggCounterText;

    [Header("Settings")]
    [SerializeField] private Color EggCounterColor;
    [SerializeField] private float ColorDuration;
    [SerializeField] private float ScaleDuration;
    private RectTransform EggCounterRectTransform;

    private void Awake()
    {
        EggCounterRectTransform = EggCounterText.gameObject.GetComponent<RectTransform>();
    }

    public void SetEggCounterText(int counter, int max)
    {
        EggCounterText.text = counter.ToString() + "/" + max.ToString();
    }
    
    public void SettEggComplited()
    {
        EggCounterText.DOColor(EggCounterColor, ColorDuration);

        EggCounterRectTransform.DOScale(1.2f, ScaleDuration).SetEase(Ease.OutBack);
    }
}
