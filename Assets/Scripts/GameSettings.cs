using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public int targetFrameRate;

    private void Awake() {
        Application.targetFrameRate = targetFrameRate;
    }
}
