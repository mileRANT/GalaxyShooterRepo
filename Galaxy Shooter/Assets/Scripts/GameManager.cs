using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
