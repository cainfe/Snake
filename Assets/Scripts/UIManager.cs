using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject PauseMenuCanvas;
    public GameObject EndGameMenuCanvas;
    private Snake Snake;
    private HighScoreManager HighScoreManager;
    public TextMeshProUGUI ScoreCard;
    public TextMeshProUGUI HighScoreCard;
    public TextMeshProUGUI EndingScoreCard;
    bool IsPaused = false;
    bool IsOver = false;

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
        if (IsPaused) {
            this.ResumeGame();
        } else {
            this.PauseGame();
        }
    }

    public void ResumeGame() {
        if (IsOver) return;

        Time.timeScale = 1.0f;
        this.PauseMenuCanvas.SetActive(false);
        IsPaused = false;
    }

    public void PauseGame() {
        if (IsOver) return;

        Time.timeScale = 0.0f;
        this.PauseMenuCanvas.SetActive(true);
        IsPaused = true;
    }

    public void RestartGame() {
        IsOver = false;
        IsPaused = false;
        Snake.ResetState();
        this.PauseMenuCanvas.SetActive(false);
        this.EndGameMenuCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void EndGame() {
        IsOver = true;
        Time.timeScale = 0.0f;
        this.EndGameMenuCanvas.SetActive(true);
        this.UpdateHighScoreText();
        this.UpdateEndingScoreCard();
    }

    public void UpdateScoreCard(int newScore) {
        ScoreCard.SetText(newScore.ToString());
    }

    public void UpdateHighScoreText() {
        int highScore = HighScoreManager.GetHighScore();
        string highScoreText = "High Score: " + highScore;
        HighScoreCard.SetText(highScoreText);
    }

    private void UpdateEndingScoreCard() {
        int endingScore = Snake.GetScore();
        string endingScoreText = "Your Score: " + endingScore;
        EndingScoreCard.SetText(endingScoreText);
    }
}
