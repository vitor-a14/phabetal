using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour {

    public Animator menuPanelAnimator;
    public Animator tutorialPanelAnimator;
    public TMP_Text scoreText;

    public Transform tutorialPanel;
    private GameObject[] tutorialPanels;
    private int currentTutorialPanel;

    private void Awake() {
        Application.targetFrameRate = 60;
        tutorialPanels = new GameObject[tutorialPanel.childCount];

        for(int i = 0; i < tutorialPanel.childCount; i++) {
            tutorialPanels[i] = tutorialPanel.GetChild(i).gameObject;
            tutorialPanels[i].SetActive(false);
        }

        scoreText.text = (PlayerPrefs.GetInt("Score") == 0 ? "" : "Max Score \n" + PlayerPrefs.GetInt("Score").ToString()).ToString();
    }

    public void StartNewGame() {
        StartCoroutine(StartNewGameCoroutine());
    }

    private IEnumerator StartNewGameCoroutine() {
        menuPanelAnimator.gameObject.SetActive(true);
        menuPanelAnimator.Play("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    public void StartTutorial() {
        currentTutorialPanel = 0;
        StartCoroutine(StartTutorialCoroutine());
        tutorialPanelAnimator.Play("FadeIn");
    }

    private IEnumerator StartTutorialCoroutine() {
        menuPanelAnimator.Play("FadeIn");
        yield return new WaitForSeconds(1f);
        tutorialPanels[currentTutorialPanel].SetActive(true);
    }

    public void TutorialPannelButton() {
        StopCoroutine(TutorialPannelButtonCoroutine());
        StartCoroutine(TutorialPannelButtonCoroutine());
    }

    private IEnumerator TutorialPannelButtonCoroutine() {
        menuPanelAnimator.Play("FadeInAndOut");

        yield return new WaitForSeconds(0.25f);

        tutorialPanels[currentTutorialPanel].SetActive(false);
        currentTutorialPanel++;

        if(currentTutorialPanel > tutorialPanel.childCount - 1) {
            tutorialPanels[currentTutorialPanel - 1].SetActive(false);
            tutorialPanelAnimator.Play("FadeOut");
        } else {
            tutorialPanels[currentTutorialPanel].SetActive(true);
        }
    }

}
