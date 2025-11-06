using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private EggCounterUI EggCounterUI;
    
    [Header("Settings")]
    [SerializeField] private int MaxEggCount = 5;

    private int CurrentEggCount;
    private GameState CurrentGameState;

    private void Awake()
    {
        Instance = this;
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
            Debug.Log("Game Win!");
            EggCounterUI.SettEggComplited();
            ChangeGameState(GameState.GameOver);
        }
        Debug.Log("Egg Count: " + CurrentEggCount);
    }

    public GameState GetCurrentGameState()
    {
        return CurrentGameState;
    }
}
