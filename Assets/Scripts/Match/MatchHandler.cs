using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handle the gameplay match until the end
public class MatchHandler : MonoBehaviour {

    public PlayerChoice player;
    public OutputChoice output;

    public void Start() {
        player.SetNewPlayerChoice();
        output.SetNewOutput();
    }

    public void Update() {
        
    }
}
