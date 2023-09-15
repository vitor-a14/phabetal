using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Animator menuPanelAnimator;

    public void StartNewGame() {
        StartCoroutine(StartNewGameCoroutine());
    }

    private IEnumerator StartNewGameCoroutine() {
        menuPanelAnimator.Play("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

}
