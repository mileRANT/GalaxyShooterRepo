using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4f;
    private Player _player;
    private Animator _animator;

    private AudioSource _audioSource;
    // Start is called before the first frame update

    [SerializeField]
    private GameObject _laserPrefab;

    private float _fireRate = 3.0f;
    private float _canFire = -1;

    void Start()
    {
         _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("No player found");
        }

        _animator = this.gameObject.GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("No animator component found");
        }
        _audioSource = this.gameObject.GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("No _audioSource component found");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            //Debug.Break(); //for debugging will pause game automatically
            LaserShoot[] lasers = enemyLaser.GetComponentsInChildren<LaserShoot>();
            for (int i = 0; i< lasers.Length; i++)
            {
                lasers[i].AssignEnemy();
            }
        }
        
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            float randomX = Random.Range(-10, 10);
            Vector3 newPos = new Vector3(randomX, 8f, 0);
            transform.position = newPos;

        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
//            Player player = GameObject.Find("Player").GetComponent<Player>();

            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _animator.SetTrigger("OnEnemyDeath");
            _speed = _speed / 2;
            _audioSource.Play();
            //Destroy(GetComponent<Collider2D>()); //so that shooting explosion doesnt trigger again
            Destroy(this.gameObject,2.6f);
            
        }
        else if (other.tag == "Player")
        {
            other.GetComponent<Player>().Damage();
            _animator.SetTrigger("OnEnemyDeath");
            _speed = _speed / 2;
            _audioSource.Play();
            Destroy(this.gameObject, 2.6f);
        }
    }
}
