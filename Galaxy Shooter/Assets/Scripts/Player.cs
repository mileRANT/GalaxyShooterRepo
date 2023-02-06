using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    public float horizontalInput;
    public float verticalInput;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;
    
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleLaser;
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    //[SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score = 0;

    private SpawnManager _spawnManager;

    //[SerializeField] //for testing purposes
    private bool _isTripleShotActive;
    //[SerializeField] //for testing purposes
    private bool _isSpeedupActive;
    //[SerializeField] //for testing purposes
    private bool _isShieldActive;

    [SerializeField]
    private GameObject shieldObject;
    [SerializeField]
    private GameObject _rightHurtObject;
    [SerializeField]
    private GameObject _leftHurtObject;

    private UIManager _uiManager;
    private GameManager _gameManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {

        //only do this if this is singleplayer mode
        //starting position. the current position is the start position
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Gamemanager is null");
        }

        if (_gameManager.isCoopMode != true)
        {
            transform.position = new Vector3(0, -3, 0);
        }
        
        
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

        _audioSource = this.gameObject.GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is null");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOne)
        {

            cleanMove();

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            {
                //_canFire = Time.time + _fireRate;     //reassignment done in the method
                shootLaser();
            }
        }
        else if (isPlayerTwo)
        {
            cleanMove_P2();

            if (Input.GetKeyDown(KeyCode.Keypad0) && Time.time > _canFire)
            {
                //_canFire = Time.time + _fireRate;     //reassignment done in the method
                shootLaser_P2();
            }
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

    void cleanMove_P2()
    {
        //        transform.Translate(Vector3.right * speed * Time.deltaTime);

        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");

        //if hit 8, move up
        if (Input.GetKey(KeyCode.Keypad8))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        //if hit 6, move right
        if (Input.GetKey(KeyCode.Keypad6))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        //if hit 2, move down
        if (Input.GetKey(KeyCode.Keypad2))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        //if hit 4, move left
        if (Input.GetKey(KeyCode.Keypad4))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }

        //Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        //transform.Translate(direction * _speed * Time.deltaTime);


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

        _audioSource.Play();
        if (_isTripleShotActive)
        {
            Instantiate(_tripleLaser, transform.position, Quaternion.identity);
            
        }
        else
        {
            Instantiate(_laser, laserPos, Quaternion.identity);
        }
        
    }

    void shootLaser_P2()
    {
        _canFire = Time.time + _fireRate;
        Vector3 laserPos = new Vector3(transform.position.x, transform.position.y + 0.85f, transform.position.z);
        //Instantiate(_laser, transform.position, Quaternion.identity);

        _audioSource.Play();
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
        if (_lives == 2)
        {
            _rightHurtObject.SetActive(true);
        }
        if (_lives == 1)
        {
            _leftHurtObject.SetActive(true);
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
