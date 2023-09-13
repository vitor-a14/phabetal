using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
