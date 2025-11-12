using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    [SerializeField] private Animator CatAnimator;


    private CatStateController CatStateController;

    private void Awake()
    {
        CatStateController = GetComponent<CatStateController>();
    }
    
    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play
        && GameManager.Instance.GetCurrentGameState() != GameState.Resume
        && GameManager.Instance.GetCurrentGameState() != GameState.CutScene
        && GameManager.Instance.GetCurrentGameState() != GameState.GameOver)
        {
            CatAnimator.enabled = false;
            return;
        }

        SetCatAnimations();
    }
    private void SetCatAnimations()
    {
        CatAnimator.enabled = true;
        var CurrentCatState = CatStateController.GetCurrentState();

        switch(CurrentCatState)
        {
            case CatState.Idle:
                CatAnimator.SetBool(Consts.CatAnimations.IS_IDLING, true);
                CatAnimator.SetBool(Consts.CatAnimations.IS_WALKING, false);
                CatAnimator.SetBool(Consts.CatAnimations.IS_RUNNING, false);
                break;
            case CatState.Walking:
                CatAnimator.SetBool(Consts.CatAnimations.IS_IDLING, false);
                CatAnimator.SetBool(Consts.CatAnimations.IS_WALKING, true);
                CatAnimator.SetBool(Consts.CatAnimations.IS_RUNNING, false);
                break;
            case CatState.Running:
                CatAnimator.SetBool(Consts.CatAnimations.IS_RUNNING, true);
                break;
            case CatState.Attacking:
                CatAnimator.SetBool(Consts.CatAnimations.IS_ATTACKING, true);
                break;
        }
    }
}
