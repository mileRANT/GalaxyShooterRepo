using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    //private AudioSource _audioSource;
    //or we could serialize this
    private AudioClip _audioClip;

    //ID for powerups
    //0 = tripleshot
    //1 = speed
    //2 = shields
    [SerializeField]
    private int _powerupID;
    // Start is called before the first frame update
    void Start()
    {
        //_audioSource = this.gameObject.GetComponent<AudioSource>();
        //if (_audioSource == null)
        _audioClip = this.gameObject.GetComponent<AudioSource>().clip;
        {
            Debug.LogError("No audio clip; is null");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (this.transform.position.y < -6f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            //note that this does not work because we destroy the object after
            //therefore we use an audioclip instead of an audio source. we then play the clip from a point in space
            //_audioSource.Play();
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);

            if (player != null)
            {
                switch(_powerupID)
                {
                    case 0: //
                        player.PowerUpTriple();
                        break;
                    case 1:
                        player.PowerUpSpeed();
                        break;
                    case 2:
                        player.PowerUpShield();
                        break;
                    default:
                        Debug.LogError("Power up ID not found.");
                        break;

                }

            }
            
            Destroy(this.gameObject);
        }
    }


}
