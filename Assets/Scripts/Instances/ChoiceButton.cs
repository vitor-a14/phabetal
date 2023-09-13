using TMPro;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    public MatchHandler matchHandler;

    public void Start() {
        matchHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<MatchHandler>();
    }

    public void MakeChoice() {
        string buttonText = GetComponentInChildren<TMP_Text>().text;
        matchHandler.HandlePlayerChoice(char.Parse(buttonText));
    }
}
