using System;
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
    private bool IstimerRunning;
    private Tween RotationTween;
    private string FinalTime;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Play:
            PlayRotationAnimation();
            StartTimer();
                break;
            case GameState.Pause:
                //PAUSE TIMER
                StopTimer();
                break;

            case GameState.Resume:
                //RESUME TIMER
                ResumeTimer();
                break;
            case GameState.GameOver:
                Finishtimer();
                break;
        }
    }

    private void PlayRotationAnimation()
    {
        RotationTween = TimerRotetableTransform.DORotate(new Vector3(0f, 0f, -360f), RotationDuration, RotateMode.FastBeyond360)
          .SetLoops(-1, LoopType.Restart)
          .SetEase(RotationEase);
    }

    private void StartTimer()
    {
        IstimerRunning = true;
        ElapsedTime = 0f;
        InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
    }

    private void StopTimer()
    {
        IstimerRunning = false;
        CancelInvoke(nameof(UpdateTimerUI));
        RotationTween.Pause();
    }

    private void ResumeTimer()
    {
        if (!IstimerRunning)
        {
            IstimerRunning = true;
            InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
            RotationTween.Play();
        }
    }

    private void Finishtimer()
    {
        StopTimer();
        FinalTime = GetFormattedElapsedTime();
    }
    
    private string GetFormattedElapsedTime()
    {
        int minutes = Mathf.FloorToInt(ElapsedTime / 60f);
        int seconds = Mathf.FloorToInt(ElapsedTime % 60f);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateTimerUI()
    {
        if (!IstimerRunning) { return; }
        ElapsedTime += 1f;

        int minutes = Mathf.FloorToInt(ElapsedTime / 60f);
        int seconds = Mathf.FloorToInt(ElapsedTime % 60f);

        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    public string GetFinalTime()
    {
        return FinalTime;
    }
}
