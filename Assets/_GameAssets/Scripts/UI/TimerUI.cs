using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform TimerRotetableTransform;
    [SerializeField] private TMP_Text TimerText;

    [Header("Settings")]
    [SerializeField] private float RotationDuration;
    [SerializeField] private Ease RotationEase;

    private float ElapsedTime;

    private void Start()
    {
        PlayRotationAnimation();
        StartTimer();
    }


    private void PlayRotationAnimation()
    {
        TimerRotetableTransform.DORotate(new Vector3(0f, 0f, -360f), RotationDuration, RotateMode.FastBeyond360)
          .SetLoops(-1, LoopType.Restart)
          .SetEase(RotationEase);
    }

    private void StartTimer()
    {
        ElapsedTime = 0f;
        InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
    }

    private void UpdateTimerUI()
    {
        ElapsedTime += 1f;

        int minutes = Mathf.FloorToInt(ElapsedTime / 60f);
        int seconds = Mathf.FloorToInt(ElapsedTime % 60f);

        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
