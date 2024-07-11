using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private Snake Snake;

    void Start() {
        Snake = FindObjectOfType<Snake>();
        Snake.OnScoreChanged += HandleScoreChange;
    }

    private void HandleScoreChange(int newScore) {
        if (newScore > GetHighScore()) {
            SetHighScore(newScore);
        }
    }

    public int GetHighScore() {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    public void SetHighScore(int newScore) {
        PlayerPrefs.SetInt("HighScore", newScore);
    }
}
