using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Handles the input buttons for the player
public class PlayerChoice : MonoBehaviour {

    public GameObject letterInstance;
    public OutputChoice output;
    public int choiceAmount;
    public List<GameObject> letters = new List<GameObject>();

    public void Awake() {
        ClearLetters();
    }

    public void SetNewPlayerChoice() {
        ClearLetters();

        //Instantiate new letters
        int count = 0;
        while(count < choiceAmount) {
            //Get current letters

            char[] currentPlayerLetters = new char[count];
            for(int i = 0; i < currentPlayerLetters.Length; i++) {
                currentPlayerLetters[i] = char.Parse(letters[i].GetComponentInChildren<TMP_Text>().text);
            }

            char[] outputLetters = new char[output.letters.Count];
            for(int i = 0; i < outputLetters.Length; i++) {
                outputLetters[i] = char.Parse(output.letters[i].GetComponentInChildren<TMP_Text>().text);
            }

            char[] allCurrentLetters = new char[currentPlayerLetters.Length + outputLetters.Length];
            Array.Copy(currentPlayerLetters, allCurrentLetters, currentPlayerLetters.Length);
            Array.Copy(outputLetters, 0, allCurrentLetters, currentPlayerLetters.Length, outputLetters.Length);

            //Instance
            GameObject letter = Instantiate(letterInstance, transform);
            letter.transform.SetParent(transform);

            //Set text
            letter.GetComponentInChildren<TMP_Text>().text = AlphabetManager.GetRandomLetter(allCurrentLetters);
            letters.Add(letter);

            //Increase loop counter
            count++;
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
