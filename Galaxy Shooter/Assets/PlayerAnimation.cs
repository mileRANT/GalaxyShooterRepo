using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _player = GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {

        if (_player.isPlayerOne)
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                _anim.SetBool("isTurnLeft", true);
                _anim.SetBool("isTurnRight", false);

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                _anim.SetBool("isTurnLeft", false);
                _anim.SetBool("isTurnRight", true);

            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                _anim.SetBool("isTurnLeft", false);
                _anim.SetBool("isTurnRight", false);

            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                _anim.SetBool("isTurnLeft", false);
                _anim.SetBool("isTurnRight", false);

            }
        } else //isplayertwo
        {
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                _anim.SetBool("isTurnLeft", true);
                _anim.SetBool("isTurnRight", false);

            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                _anim.SetBool("isTurnLeft", false);
                _anim.SetBool("isTurnRight", true);

            }

            if (Input.GetKeyUp(KeyCode.Keypad4))
            {
                _anim.SetBool("isTurnLeft", false);
                _anim.SetBool("isTurnRight", false);

            }
            else if (Input.GetKeyUp(KeyCode.Keypad6))
            {
                _anim.SetBool("isTurnLeft", false);
                _anim.SetBool("isTurnRight", false);

            }

        }
    }
}
