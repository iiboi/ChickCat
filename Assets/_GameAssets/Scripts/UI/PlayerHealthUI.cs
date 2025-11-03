using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] PlayerHealthImages;

    [Header("Sprites")]
    [SerializeField] private Sprite PlayerHealtySprite;
    [SerializeField] private Sprite PlayerUnhealtySprite;
    [Header("Settings")]
    [SerializeField] private float ScaleDuration;

    private RectTransform[] PlayerHealthTransforms;

    private void Awake()
    {
        PlayerHealthTransforms = new RectTransform[PlayerHealthImages.Length];

        for (int i = 0; i < PlayerHealthImages.Length; i++)
        {
            PlayerHealthTransforms[i] = PlayerHealthImages[i].gameObject.GetComponent<RectTransform>();
        }
    }

    //FOR TESTING

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AnimateDamage();
        }
        
        if(Input.GetKeyDown(KeyCode.L))
        {
            AnimateDamageForAll();
        }
    }

    public void AnimateDamage()
    {
        for (int i = 0; i < PlayerHealthImages.Length; i++)
        {
            if (PlayerHealthImages[i].sprite == PlayerHealtySprite)
            {
                AnimateDamageSprite(PlayerHealthImages[i], PlayerHealthTransforms[i]);
                break;
            }
        }
    }
    
    public void AnimateDamageForAll()
    {
        for(int i = 0; i < PlayerHealthImages.Length; i++)
        {
            AnimateDamageSprite(PlayerHealthImages[i], PlayerHealthTransforms[i]);
        }
    }
    
    private void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, ScaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = PlayerUnhealtySprite;
            activeImageTransform.DOScale(1f, ScaleDuration).SetEase(Ease.OutBack);
        });
    }
}
