using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public int currentScore;
    public TMP_Text scoreText;

    public void SetScore(int newValue) {
        currentScore = newValue;
        currentScore = Mathf.Clamp(currentScore, 0, 999999);
        scoreText.text = currentScore.ToString();
    }

    public void ChangeScore(int value) {
        currentScore += value;
        currentScore = Mathf.Clamp(currentScore, 0, 999999);
        scoreText.text = currentScore.ToString();
    }

}
