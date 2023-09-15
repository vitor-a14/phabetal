using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// Handles the input buttons for the player
public class PlayerChoice : MonoBehaviour {

    public GameObject letterInstance;
    public MatchHandler match;
    public OutputChoice output;
    public int choiceAmount;
    public List<GameObject> letters = new List<GameObject>();

    public void Awake() {
        ClearLetters();
    }

    public void SetNewPlayerChoice() {
        ClearLetters();

        // Instantiate new letters
        int count = 0;
        System.Random random = new System.Random();
        int choosenAnswerIndex = random.Next(0, choiceAmount);

        while (count < choiceAmount) {
            // Get current letters
            char[] currentPlayerLetters = letters
                .Select(letter => char.Parse(letter.GetComponentInChildren<TMP_Text>().text))
                .ToArray();

            char[] outputLetters = output.letters
                .Select(letter => char.Parse(letter.GetComponentInChildren<TMP_Text>().text))
                .ToArray();

            char[] allCurrentLetters = currentPlayerLetters.Concat(outputLetters).ToArray();

            // Instantiate
            GameObject letter = Instantiate(letterInstance, transform);
            letter.transform.SetParent(transform);

            // Set text
            bool correctAnswer = count == choosenAnswerIndex;
            string choice = AlphabetManager.GetPlayerChoice(allCurrentLetters, outputLetters, correctAnswer, match.isGreaterThan);
            
            if (!string.IsNullOrEmpty(choice)) {
                letter.GetComponentInChildren<TMP_Text>().text = choice;

                //if (correctAnswer) letter.GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Bold;

                letters.Add(letter);
                count++;
            } else {
                // Handle the case where AlphabetManager.GetPlayerChoice returns an empty string.
                // You can log an error or take appropriate action.
                Debug.LogError("AlphabetManager.GetPlayerChoice returned an empty string.");
            }
        }
    }

    //Remove previous output
    private void ClearLetters() {
        letters.Clear();

        for(int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
