using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    //ID for powerups
    //0 = tripleshot
    //1 = speed
    //2 = shields
    [SerializeField]
    private int _powerupID;
    // Start is called before the first frame update
    void Start()
    {
        
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
