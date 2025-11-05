using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private EggCounterUI EggCounterUI;
    
    [Header("Settings")]
    [SerializeField] private int MaxEggCount = 5;

    private int CurrentEggCount;

    private void Awake() 
    {
        Instance = this;
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
        }
        Debug.Log("Egg Count: " + CurrentEggCount);
    }
}
