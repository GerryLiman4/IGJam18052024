using System;
using UnityEngine;
using UnityEngine.UI;

public class M_GameOverUIManager : MonoBehaviour
{
    [SerializeField] public Canvas gameOverCanvas;
    [SerializeField] public Button restartButton;
    [SerializeField] public Button mainMenuButton;
    [SerializeField] public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        SignalManager.GameOver += OnGameOver;
        SignalManager.RestartGame += OnRestart;
        restartButton.onClick.AddListener(OnRestart);
        mainMenuButton.onClick.AddListener(OnMainMenu);
        exitButton.onClick.AddListener(OnExit);
    }

    private void OnExit()
    {
        Application.Quit();
    }

    private void OnMainMenu()
    {
       
    }

    private void OnRestart()
    {
       
    }

    private void OnGameOver(bool isLost)
    {
        gameOverCanvas.enabled = true;
    }

}
