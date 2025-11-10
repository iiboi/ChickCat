using UnityEngine;

public class CatStateController : MonoBehaviour
{
    [SerializeField] private CatState CurrentCatState = CatState.Walking;

    private void Start() 
    {
        ChangeState(CatState.Walking);
    }

    public void ChangeState(CatState newState)
    {
        if (CurrentCatState == newState) { return; }

        CurrentCatState = newState;
    }

    public CatState GetCurrentState()
    {
        return CurrentCatState;
    }

}
