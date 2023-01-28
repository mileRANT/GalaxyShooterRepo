using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            float randomX = Random.Range(-10, 10);
            Vector3 newPos = new Vector3(randomX, 8f, 0);
            transform.position = newPos;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            Destroy(this.gameObject);
            Destroy(other);
        }
        else if (other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
