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
    private TextMeshProUGUI _gameoverText;
    [SerializeField]
    private TextMeshProUGUI _restartText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _livesImg;    //the gameobject ui

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameoverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Gamemanager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
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
}
