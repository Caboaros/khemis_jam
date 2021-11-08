using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private Button returnToMenuWinButton;
    [SerializeField] private Button newGameButton;
    [Space] [SerializeField] private CanvasGroup winPanelCanvasGroup;
    [SerializeField] private CanvasGroup defeatPanelCanvasGroup;

    private static bool _win;

    public static void LoadScene(bool win)
    {
        _win = win;
        SceneManager.LoadScene("GameOver");
    }

    private void Start()
    {
        returnToMenuButton.onClick.AddListener(ReturnToMenu);
        returnToMenuWinButton.onClick.AddListener(ReturnToMenu);
        newGameButton.onClick.AddListener(NewGameButton);

        winPanelCanvasGroup.alpha = _win ? 1 : 0;
        defeatPanelCanvasGroup.alpha = _win ? 0 : 1;
        winPanelCanvasGroup.blocksRaycasts = _win;
        defeatPanelCanvasGroup.blocksRaycasts = !_win;
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void NewGameButton()
    {
        SceneManager.LoadScene("Altar");
    }
}
