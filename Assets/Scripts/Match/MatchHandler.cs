using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

// Handle the gameplay match until the end
public class MatchHandler : MonoBehaviour {

    [Header("Dependencies")]
    public PlayerChoice player;
    public OutputChoice output;
    public ScoreManager score;
    public TimerManager timer;

    [Header("Objects References")]
    public TMP_Text indicator;
    public TMP_Text gameOverScore;
    public Camera mainCamera;
    public Animator panelAnimator;
    public Animator gameOverAnimator;

    [Header("Graphics Settings")]
    public Color greaterLettersColorMode;
    public Color lowerLettersColorMode;

    [Header("Match Settings")]
    public float turnDuration = 5;
    public int scoreGainPerTurn = 5;
    [SerializeField] public List<DifficultSetting> difficulties = new List<DifficultSetting>();

    //if true, the player need to select a letter bigger than the output
    //if false the player need to select a letter smaller than the output
    [HideInInspector] public bool isGreaterThan = true;
    private int currentTurn = 1;
    private bool gameOver = false;

    public void Start() {
        SetDifficulty();

        output.SetNewOutput();
        player.SetNewPlayerChoice();
        score.SetScore(0);
        timer.SetTurnTimer(turnDuration);
    }

    //Handle color change
    public void Update() {
        Color newColor = isGreaterThan ? greaterLettersColorMode : lowerLettersColorMode;
        Color newBackgroundColor = isGreaterThan ? lowerLettersColorMode : greaterLettersColorMode;

        mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, newBackgroundColor, 5 * Time.deltaTime);
        score.scoreText.color = Color.Lerp(score.scoreText.color, newColor, 5 * Time.deltaTime);
        indicator.color = Color.Lerp(indicator.color, newColor, 5 * Time.deltaTime);
        timer.fill.color = Color.Lerp(timer.fill.color, newColor, 5 * Time.deltaTime);

        foreach(GameObject letter in player.letters) {
            TMP_Text text = letter.GetComponentInChildren<TMP_Text>();
            text.color = Color.Lerp(text.color, newColor, 5 * Time.deltaTime);
        }

        foreach(GameObject letter in output.letters) {
            TMP_Text text = letter.GetComponentInChildren<TMP_Text>();
            text.color = Color.Lerp(text.color, newColor, 5 * Time.deltaTime);
        }

        if(timer.timeIsOver) {
            EndGame();
        }
    }

    public void HandlePlayerChoice(char letter) {
        if(gameOver) return;

        foreach(GameObject letterOutput in output.letters) {
            char outputText = char.Parse(letterOutput.GetComponentInChildren<TMP_Text>().text);

            //check if input is bigger than the outputs
            //otherwise, check if input is smaller than outputs
            if(isGreaterThan && !AlphabetManager.CompareLetters(letter, outputText)) {
                EndGame(); 
                return;
            } else if(!isGreaterThan && !AlphabetManager.CompareLetters(outputText, letter)) {
                EndGame(); 
                return;
            }
        }   

        //if none of the tests above trigger, the player can get to the next turn
        NextTurn();
    }

    private void NextTurn() {
        if(gameOver) return;

        //handle match stats
        score.ChangeScore(scoreGainPerTurn);
        currentTurn++;

        SetDifficulty();

        //handle indicator
        isGreaterThan = !isGreaterThan;
        indicator.text = isGreaterThan ? ">" : "<";

        //handle match
        output.SetNewOutput();
        player.SetNewPlayerChoice();
        timer.SetTurnTimer(turnDuration);
    }

    private void EndGame() {
        StartCoroutine(EndGameCoroutine());
    }

    private IEnumerator EndGameCoroutine() {
        currentTurn = 1;
        Random random = new Random();

        foreach(GameObject letter in player.letters) {
            int randomForce = random.Next(0, 10);

            Rigidbody2D rigid = letter.GetComponent<Rigidbody2D>();
            rigid.bodyType = RigidbodyType2D.Dynamic;
            rigid.AddForce(Vector2.up * 185 * (randomForce * 0.1f), ForceMode2D.Impulse);
            rigid.AddTorque(25 * (randomForce * 0.1f) * (randomForce % 2 == 0 ? 1 : -1), ForceMode2D.Impulse);
        }

        foreach(GameObject letter in output.letters) {
            int randomForce = random.Next(0, 10);

            Rigidbody2D rigid = letter.GetComponent<Rigidbody2D>();
            rigid.bodyType = RigidbodyType2D.Dynamic;
            rigid.AddForce(Vector2.up * 185 * (randomForce * 0.1f), ForceMode2D.Impulse);
            rigid.AddTorque(25 * (randomForce * 0.1f) * (randomForce % 2 == 0 ? 1 : -1), ForceMode2D.Impulse);
        }

        gameOver = true;
        gameOverScore.text = score.currentScore.ToString();
        panelAnimator.transform.GetComponentInChildren<UnityEngine.UI.Image>().color = isGreaterThan ? greaterLettersColorMode : lowerLettersColorMode;
        panelAnimator.Play("FadeOut");

        yield return new WaitForSeconds(2f);

        gameOverAnimator.Play("FadeIn");

        yield return new WaitForSeconds(5f);
    }

    public void LoadToMainMenu() {
        StartCoroutine(LoadToMainMenuCoroutine());
    }

    private IEnumerator LoadToMainMenuCoroutine() {
        gameOverAnimator.Play("FadeOut");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(0);
    }

    private void SetDifficulty() {
        foreach(DifficultSetting setting in difficulties) {
            if(setting.turnTrigger == currentTurn) {
                turnDuration = setting.turnDuration;
                player.choiceAmount = setting.playerChoiceAmount;
            }
        }
    }
}
