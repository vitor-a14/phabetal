using System;
using TMPro;
using UnityEngine;

// Handle the gameplay match until the end
public class MatchHandler : MonoBehaviour {

    public PlayerChoice player;
    public OutputChoice output;
    public ScoreManager score;

    public int currentTurn = 1;
    public int scoreGainPerTurn = 5;

    //if true, the player need to select a letter bigger than the output
    //if false the player need to select a letter smaller than the output
    public bool isGreaterThan = true;

    public void Start() {
        player.SetNewPlayerChoice();
        output.SetNewOutput();
        score.SetScore(0);
    }

    public void HandlePlayerChoice(char letter) {
        foreach(GameObject letterOutput in output.letters) {
            char outputText = char.Parse(letterOutput.GetComponentInChildren<TMP_Text>().text);

            if(isGreaterThan && !AlphabetManager.CompareLetters(letter, outputText)) EndGame(); //check if input is bigger than the outputs
            else if(!AlphabetManager.CompareLetters(outputText, letter)) EndGame(); //otherwise, check if input is smaller than outputs
        }   

        //if none of the tests above trigger, the player can get to the next turn
        NextTurn();
    }

    private void NextTurn() {
        score.ChangeScore(scoreGainPerTurn);
        currentTurn++;

        player.SetNewPlayerChoice();
        output.SetNewOutput();
    }

    private void EndGame() {
        score.SetScore(0);
        Debug.Log("game over");
    }
}
