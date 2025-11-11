using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerUI TimerUI;
    [SerializeField] private Button TryAgainButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private TMP_Text TimerText;

    private void OnEnable() 
    {
        TimerText.text = TimerUI.GetFinalTime();

        TryAgainButton.onClick.AddListener(OneTryAgainButtonClicked);

        MainMenuButton.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);
        });
    }

    private void OneTryAgainButtonClicked()
    {
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);
    }
}
