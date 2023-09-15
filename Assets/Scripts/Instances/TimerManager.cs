using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [HideInInspector] public bool timeIsOver;
    [HideInInspector] public Image fill;

    private float currentDuration;
    private Slider slider;

    public void Start() {
        slider = GetComponent<Slider>();
        fill = GetComponentInChildren<Image>();
        timeIsOver = false;
    }

    public void Update() {
        if(!timeIsOver) {
            currentDuration -= Time.deltaTime;
            currentDuration = Mathf.Clamp(currentDuration, 0, 9999);
            slider.value = currentDuration;
        }

        if(currentDuration <= 0) {
            timeIsOver = true;
        }
    }

    public void SetTurnTimer(float duration) {
        if(timeIsOver) return;

        slider.maxValue = duration;
        currentDuration = duration;
    }
}
