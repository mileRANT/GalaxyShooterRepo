using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    public bool isCoopMode = false;

    [SerializeField]
    private UIManager _uiManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            loadMainMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.P) && !_isGameOver)
        {
            _uiManager.showPauseMenu();
            Time.timeScale = 0f;
        }

    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("Main_menu_updated");
    }
}
