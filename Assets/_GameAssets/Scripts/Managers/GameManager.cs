using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private EggCounterUI EggCounterUI;
    [SerializeField] private WinLoseUI WinLoseUI;

    [Header("Settings")]
    [SerializeField] private int MaxEggCount = 5;
    [SerializeField] private float Delay = 0.5f;

    private int CurrentEggCount;
    private GameState CurrentGameState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start() 
    {
        HealthManager.Instance.OnPlayerDeath += HealthManager_OnPlayerDeath;
    }

    private void HealthManager_OnPlayerDeath()
    {
        StartCoroutine(OneGameOver());
    }

    private void OnEnable()
    {
        ChangeGameState(GameState.Play);
    }

    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        CurrentGameState = gameState;
        Debug.Log("Game State = " + gameState);
    }

    public void OnEggCollected()
    {
        CurrentEggCount++;
        EggCounterUI.SetEggCounterText(CurrentEggCount, MaxEggCount);

        if (CurrentEggCount == MaxEggCount)
        {
            //WIN GAME
            EggCounterUI.SettEggComplited();
            ChangeGameState(GameState.GameOver);
            WinLoseUI.OnGameWin();
        }
    }

    private IEnumerator OneGameOver()
    {
        yield return new WaitForSeconds(Delay);
        ChangeGameState(GameState.GameOver);
        WinLoseUI.OnGameLose();
    }

    public GameState GetCurrentGameState()
    {
        return CurrentGameState;
    }
}
