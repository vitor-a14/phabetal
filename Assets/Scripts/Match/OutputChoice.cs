using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Renders and process what output the player will receive
public class OutputChoice : MonoBehaviour {

    public GameObject letterInstance;
    public int amount;
    public List<GameObject> letters = new List<GameObject>();

    public void Awake() {
        ClearLetters();
    }

    public void SetNewOutput() {
        ClearLetters();

        //Instantiate new letters
        int count = 0;
        while(count < amount) {
            //Get current letters
            char[] currentLetters = new char[count];
            for(int i = 0; i < currentLetters.Length; i++) {
                currentLetters[i] = char.Parse(letters[i].GetComponentInChildren<TMP_Text>().text);
            }

            //Instance
            GameObject letter = Instantiate(letterInstance, transform);
            letter.GetComponent<Button>().interactable = false;
            letter.transform.SetParent(transform);

            //Set text
            letter.GetComponentInChildren<TMP_Text>().text = AlphabetManager.GetRandomLetter(currentLetters);
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
