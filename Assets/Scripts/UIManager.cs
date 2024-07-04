using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject PauseMenuCanvas;
    public GameObject Snake;
    public TextMeshProUGUI ScoreCard;
    bool Paused = false;

    void Start() {
        this.PauseMenuCanvas.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            this.TogglePauseState();
        }

        this.UpdateScoreCard();
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

    public void QuitGame() {
        Application.Quit();
    }

    public void UpdateScoreCard() {
        ScoreCard.SetText(Snake.GetComponent<Snake>().GetScore().ToString());
    }
}
