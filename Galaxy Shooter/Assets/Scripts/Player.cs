using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    public float horizontalInput;
    public float verticalInput;
    
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleLaser;
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score = 0;

    private SpawnManager _spawnManager;

    [SerializeField] //for testing purposes
    private bool _isTripleShotActive;
    [SerializeField] //for testing purposes
    private bool _isSpeedupActive;
    [SerializeField] //for testing purposes
    private bool _isShieldActive;
    [SerializeField]
    private GameObject shieldObject;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        //starting position. the current position is the start position
        transform.position = new Vector3(0, -3, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        //check if spawnmanager was successfully grabbed
        if (_spawnManager == null)
        {
            Debug.LogError("Spawnmanager is null");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UImanager is null");
        }
        _uiManager.UpdateLives(this._lives);
    }

    // Update is called once per frame
    void Update()
    {
        cleanMove();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            //_canFire = Time.time + _fireRate;     //reassignment done in the method
            shootLaser();
        }
    }

    void moveHorizontal()
    {
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
    }
    void moveVertical()
    {
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
    }

    void cleanMove()
    {
        //        transform.Translate(Vector3.right * speed * Time.deltaTime);

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);


        //the below does wrap around. if we wanted to limit movement, we could simplify to a mathf.clamp
        //player boundary vertical
        if (transform.position.y > 7.5)
        {
            transform.position = new Vector3(transform.position.x, -5.6f, 0);
        }
        else if (transform.position.y < -5.6f)
        {
            transform.position = new Vector3(transform.position.x, 7.5f, 0);
        }
        //player boundary horizontal
        if (transform.position.x > 11.3)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void shootLaser()
    {
        _canFire = Time.time + _fireRate;
        Vector3 laserPos = new Vector3(transform.position.x, transform.position.y + 0.85f, transform.position.z);
        //Instantiate(_laser, transform.position, Quaternion.identity);

        if (_isTripleShotActive)
        {
            Instantiate(_tripleLaser, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laser, laserPos, Quaternion.identity);
        }
        
    }
    
    public void Damage()
    {
        if (_isShieldActive)
        {
            shieldObject.SetActive(false);
            _isShieldActive = false;
            return;
        }

        _lives--;
        _uiManager.UpdateLives(this._lives);

        //check if dead
        if (_lives < 1)
        {
            Destroy(this.gameObject);
            _uiManager.showGameOver();
            //communicate with spawnmanager to stop spawning more
            _spawnManager.onPlayerDeath(); 
        }
    }

    public void PowerUpTriple()
    {
        _isTripleShotActive = true;
        //start a power down coroutine
        StartCoroutine("StopTripleShot");
    }

    public void PowerUpSpeed()
    {
        _isSpeedupActive = true;
        _speed += 5f;
        //start a power down coroutine
        StartCoroutine("StopSpeedUp");
    }

    public void PowerUpShield()
    {
        _isShieldActive = true;
        shieldObject.SetActive(true);
        //start a power down coroutine
        //StartCoroutine("StopTripleShot");
    }


    //tripleshot powerdown coroutine to remove triple shot in 5 seconds.
    IEnumerator StopTripleShot()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    //speed up powerdown coroutine to remove speed boost in 5 seconds.
    IEnumerator StopSpeedUp()
    {
        yield return new WaitForSeconds(5.0f);
        _speed -= 5f;
        _isSpeedupActive = false;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.updateScore(_score);
    }
}
