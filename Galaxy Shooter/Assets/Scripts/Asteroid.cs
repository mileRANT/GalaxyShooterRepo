using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _rotSpeed = 5.0f;

    private Player _player;
    private int _asteroidLife = 3;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnm;
    private GameManager _gameManager;
    //private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GM is null");
        }

        if (_gameManager.isCoopMode)
        {
            _player = GameObject.Find("Player_1").GetComponent<Player>();
        } else
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }

        _spawnm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnm == null)
        {
            Debug.LogError("SpawnManager is null");
        }

        //_animator = GetComponent<Animator>();
        //if (_animator == null)
        //{
        //    Debug.LogError("Animator is null");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, _rotSpeed);
        transform.Rotate(Vector3.forward * _rotSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            //            Player player = GameObject.Find("Player").GetComponent<Player>();

            Destroy(other.gameObject);

            _asteroidLife--;
            //if (_asteroidLife < 1)
            if (_asteroidLife == 0)
            {
                if (_player != null)
                {
                    _player.AddScore(50);
                }
                //_animator.SetTrigger("OnEnemyDeath");
                //_animator.SetTrigger("ExplodeAsteroid");
                //instead of animating asteroid explosion as an animation, instantiate it as a prefab and overlay it on the asteroid
                GameObject _explosion = Instantiate(_explosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                _spawnm.StartSpawnRoutines();
                Destroy(this.gameObject, 0.4f);
                //Destroy(_explosion, 2.3f); explosion is destroyed automatically in its own script
                
            }

        }
        else if (other.tag == "Player")
        {
            other.GetComponent<Player>().Damage();
            //_animator.SetTrigger("OnEnemyDeath");
            _asteroidLife--;
            if (_asteroidLife < 1)
            {
                //_animator.SetTrigger("ExplodeAsteroid");
                GameObject _explosion = Instantiate(_explosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                _spawnm.StartSpawnRoutines();
                Destroy(this.gameObject, 0.4f);
                //Destroy(_explosion, 2.3f);

            }
        }
    }


}
