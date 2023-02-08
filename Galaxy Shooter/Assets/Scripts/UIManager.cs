using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _highscoreText;
    [SerializeField]
    private TextMeshProUGUI _gameoverText;
    [SerializeField]
    private TextMeshProUGUI _restartText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _livesImg;    //the gameobject ui

    private Animator _pauseAnimator;

    [SerializeField]
    private GameObject _pauseMenu;
    

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _highscoreText.text = "HighScore: " + PlayerPrefs.GetInt("highscore");

        _gameoverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Gamemanager is null");
        }
        _pauseAnimator = _pauseMenu.GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;    //unscaled time because it needs to run despite the timescale = 0
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
        if (playerScore > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", playerScore);
            _highscoreText.text = "HighScore: " + PlayerPrefs.GetInt("highscore");
        }
        
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprites[currentLives];
    }
    public void showGameOver()
    {
        _gameoverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        //StartCoroutine(GameOverFlickerRoutine());
    }

    //alternative for flickering gameover flicker
    //we would call this in the showGameOver section
    IEnumerator GameOverFlickerRoutine()
    {
        _gameoverText.text = "GAME OVER";
        yield return new WaitForSeconds(0.5f);
        _gameoverText.text = "";
        yield return new WaitForSeconds(0.5f);
    }

    //pause menu actions
    public void showPauseMenu()
    {
        _pauseMenu.gameObject.SetActive(true);
        _pauseAnimator.SetBool("isPaused", true);
        //StartCoroutine(GameOverFlickerRoutine());
        //need a way to stop game functions
    }
    public void hidePauseMenu()
    {
        _pauseAnimator.SetBool("isPaused", false);
        _pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        //need a way to resume game functions
    }
}
