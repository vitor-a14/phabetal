using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Animator menuPanelAnimator;
    public Animator tutorialPanelAnimator;

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
    }

    public void StartNewGame() {
        StartCoroutine(StartNewGameCoroutine());
    }

    private IEnumerator StartNewGameCoroutine() {
        menuPanelAnimator.gameObject.SetActive(true);
        menuPanelAnimator.Play("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    public void StartTutorial() {
        currentTutorialPanel = 0;
        StartCoroutine(StartTutorialCoroutine());
        tutorialPanelAnimator.Play("FadeIn");
    }

    private IEnumerator StartTutorialCoroutine() {
        menuPanelAnimator.Play("FadeIn");
        yield return new WaitForSeconds(2f);
        tutorialPanels[currentTutorialPanel].SetActive(true);
    }

    public void TutorialPannelButton() {
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
