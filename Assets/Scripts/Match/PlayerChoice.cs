using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Handles the input buttons for the player
public class PlayerChoice : MonoBehaviour {

    public GameObject letterInstance;
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
            //Instance
            GameObject letter = Instantiate(letterInstance, transform);
            letter.transform.SetParent(transform);

            //Set text
            letter.GetComponentInChildren<TMP_Text>().text = AlphabetManager.GetRandomLetter();
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
