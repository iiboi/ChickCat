using UnityEngine;
using UnityEngine.UI;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO WheatDesignSO;
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private PlayerStateUI PlayerStateUI;

    private RectTransform PlayerBoosterTransform;
    private Image PlayerBoosterImage;

    void Awake()
    {
        PlayerBoosterTransform = PlayerStateUI.GetBoosterSlowTransform;
        PlayerBoosterImage = PlayerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    {
        PlayerController.SetMovementSpeed(WheatDesignSO.IncreaseDecreaseMultiplier, WheatDesignSO.ResetBoostDuration);
          PlayerStateUI.PlayBoosterUIAnimations(PlayerBoosterTransform, PlayerBoosterImage, PlayerStateUI.GetRottenBoosterWheatImage,
          WheatDesignSO.ActiveSprite, WheatDesignSO.PassiveSprite, WheatDesignSO.ActiveWheatSprite, WheatDesignSO.PassiveWheatSprite, WheatDesignSO.ResetBoostDuration);

        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
        AudioManager.Instance.Play(SoundType.PickupBadSound);
        
        Destroy(gameObject);
    }
}
