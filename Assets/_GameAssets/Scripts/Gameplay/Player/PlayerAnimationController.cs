using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator PlayerAnimator;

    private PlayerController PlayerController;

    private StateController StateController;

    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        StateController = GetComponent<StateController>();

    }

    private void SetPlayerAnimations()
    {
        var CurrentState = StateController.GetCurrentState();

        switch (CurrentState)
        {
            case PlayerState.Idle:
                PlayerAnimator.SetBool("IsDashing", false);
                PlayerAnimator.SetBool("IsMoving", false);
                break;
        }
    }
}
