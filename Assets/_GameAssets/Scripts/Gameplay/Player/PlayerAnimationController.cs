using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
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
    private void Start()
    {
        PlayerController.OnPlayerJumped += PlayerController_OnPlayerJumped;
    }

    private void Update()
    {
        SetPlayerAnimations();
    }
    private void PlayerController_OnPlayerJumped()
    {
        PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
        Invoke(nameof(ResetJumping), 0.5f);
    }
    private void ResetJumping()
    {
        PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
    }

    private void SetPlayerAnimations()
    {
        var CurrentState = StateController.GetCurrentState();

        switch (CurrentState)
        {
            case PlayerState.Idle:
                PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_DASHING, false);
                PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                break;

            case PlayerState.Move:
                PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_DASHING, false);
                PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                break;

            case PlayerState.DashIdle:
                PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_DASHING, true);
                PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_DASHING_ACTIVE, false);
                break;

            case PlayerState.Dash:
                PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_DASHING, true);
                PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_DASHING_ACTIVE, true);
                break;

        }
    }
}
