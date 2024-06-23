using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject Canvas;
    bool Paused = false;

    void Start() {
        this.Canvas.SetActive(false);
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
        this.Canvas.SetActive(false);
        Paused = false;
    }

    public void PauseGame() {
        Time.timeScale = 0.0f;
        this.Canvas.SetActive(true);
        Paused = true;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
