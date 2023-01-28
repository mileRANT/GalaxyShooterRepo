using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    public float horizontalInput;
    public float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        //starting position. the current position is the start position
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
//        transform.Translate(Vector3.right * speed * Time.deltaTime);

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //if (horizontalInput != 0)
        //{
        //    moveHorizontal();
        //}
        //if (verticalInput != 0)
        //{
        //    moveVertical();
        //}
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

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

    void moveHorizontal()
    {
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
    }
    void moveVertical()
    {
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
    }
}
