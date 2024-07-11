using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject PauseMenuCanvas;
    public GameObject EndGameMenuCanvas;
    private Snake Snake;
    private HighScoreManager HighScoreManager;
    public TextMeshProUGUI ScoreCard;
    bool Paused = false;

    void Start() {
        Snake = FindObjectOfType<Snake>();
        Snake.OnScoreChanged += UpdateScoreCard;
        HighScoreManager = FindObjectOfType<HighScoreManager>();
        this.RestartGame();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            this.TogglePauseState();
        }
    }

    public void TogglePauseState() {
        if (Paused) {
            this.ResumeGame();
        } else {
            this.PauseGame();
        }
    }

    public void ResumeGame() {
        Time.timeScale = 1.0f;
        this.PauseMenuCanvas.SetActive(false);
        Paused = false;
    }

    public void PauseGame() {
        Time.timeScale = 0.0f;
        this.PauseMenuCanvas.SetActive(true);
        Paused = true;
    }

    public void RestartGame() {
        Snake.ResetState();
        this.PauseMenuCanvas.SetActive(false);
        this.EndGameMenuCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void EndGame() {
        Time.timeScale = 0.0f;
        this.EndGameMenuCanvas.SetActive(true);
    }

    public void UpdateScoreCard(int newScore) {
        ScoreCard.SetText(newScore.ToString());
    }
}
