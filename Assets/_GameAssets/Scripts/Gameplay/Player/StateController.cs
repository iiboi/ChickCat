using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState CurrentPlayerState = PlayerState.Idle;

    private void Start()
    {
        ChangeState(PlayerState.Idle);
    }

    public void ChangeState(PlayerState NewPlayerState)
    {
        if (CurrentPlayerState == NewPlayerState)
        {
            return;
        }

        CurrentPlayerState = NewPlayerState;
    }

    public PlayerState GetCurrentState()
    {
        return CurrentPlayerState;
    }
}
