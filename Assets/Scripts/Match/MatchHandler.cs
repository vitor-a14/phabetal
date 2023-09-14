using System;
using TMPro;
using UnityEngine;

// Handle the gameplay match until the end
public class MatchHandler : MonoBehaviour {

    [Header("Dependencies")]
    public PlayerChoice player;
    public OutputChoice output;
    public ScoreManager score;

    [Header("Objects References")]
    public TMP_Text indicator;
    public Camera mainCamera;

    [Header("Graphics Settings")]
    public Color greaterLettersColorMode;
    public Color lowerLettersColorMode;

    [Header("Match Settings")]
    public int scoreGainPerTurn = 5;
    public int difficulty = 1;

    //if true, the player need to select a letter bigger than the output
    //if false the player need to select a letter smaller than the output
    private bool isGreaterThan = true;
    private int currentTurn = 1;

    public void Start() {
        output.SetNewOutput();
        player.SetNewPlayerChoice();
        score.SetScore(0);
    }

    //Handle color change
    public void LateUpdate() {
        Color newColor = isGreaterThan ? greaterLettersColorMode : lowerLettersColorMode;
        Color newBackgroundColor = isGreaterThan ? lowerLettersColorMode : greaterLettersColorMode;

        mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, newBackgroundColor, 5 * Time.deltaTime);
        score.scoreText.color = Color.Lerp(score.scoreText.color, newColor, 5 * Time.deltaTime);
        indicator.color = Color.Lerp(indicator.color, newColor, 5 * Time.deltaTime);

        foreach(GameObject letter in player.letters) {
            TMP_Text text = letter.GetComponentInChildren<TMP_Text>();
            text.color = Color.Lerp(text.color, newColor, 5 * Time.deltaTime);
        }

        foreach(GameObject letter in output.letters) {
            TMP_Text text = letter.GetComponentInChildren<TMP_Text>();
            text.color = Color.Lerp(text.color, newColor, 5 * Time.deltaTime);
        }
    }

    public void HandlePlayerChoice(char letter) {
        foreach(GameObject letterOutput in output.letters) {
            char outputText = char.Parse(letterOutput.GetComponentInChildren<TMP_Text>().text);

            if(isGreaterThan && !AlphabetManager.CompareLetters(letter, outputText)) EndGame(); //check if input is bigger than the outputs
            else if(!isGreaterThan && !AlphabetManager.CompareLetters(outputText, letter)) EndGame(); //otherwise, check if input is smaller than outputs
        }   

        //if none of the tests above trigger, the player can get to the next turn
        NextTurn();
    }

    private void NextTurn() {
        score.ChangeScore(scoreGainPerTurn);
        currentTurn++;

        isGreaterThan = !isGreaterThan;
        indicator.text = isGreaterThan ? ">" : "<";

        output.SetNewOutput();
        player.SetNewPlayerChoice();
    }

    private void EndGame() {
        score.SetScore(0);
        currentTurn = 1;
        Debug.Log("game over");
    }
}
