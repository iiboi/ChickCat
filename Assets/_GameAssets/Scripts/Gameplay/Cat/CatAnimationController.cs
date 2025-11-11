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
        SetCatAnimations();
    }
    private void SetCatAnimations()
    {
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
