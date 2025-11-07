using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerUI TimerUI;
    [SerializeField] private Button OneMoreButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private TMP_Text TimerText;

    private void OnEnable() 
    {
        TimerText.text = TimerUI.GetFinalTime();

        OneMoreButton.onClick.AddListener(OneMoreButtonClicked);
    }

    private void OneMoreButtonClicked()
    {
        SceneManager.LoadScene(Consts.SceneNames.GAME_SCENE);
    }
}
