using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;}
    [SerializeField] private int MaxEggCount = 5;

    private int CurrentEggCount;

    private void Awake() 
    {
        Instance = this;
    }

    public void OnEggCollected()
    {
        CurrentEggCount++;

        if (CurrentEggCount == MaxEggCount)
        {
            //WIN GAME
            Debug.Log("Game Win!");
        }
        Debug.Log("Egg Count: " + CurrentEggCount);
    }
}
