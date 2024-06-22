using UnityEngine;

public class Pause : MonoBehaviour {
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

    private void TogglePauseState() {
        if (Paused) {
            Time.timeScale = 1.0f;
            this.Canvas.SetActive(false);
            Paused = false;
        } else {
            Time.timeScale = 0.0f;
            this.Canvas.SetActive(true);
            Paused = true;
        }
    }
}
