using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed, _gravity, _jumpHeight;

    private Vector3 _moveDir;
    private Vector3 _playerVelocity;
    private CharacterController _controller;
    private float _yVelocity;
    private Animator _anim;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.isGrounded)
        {
            _moveDir = new Vector3(0, 0, Input.GetAxisRaw("Horizontal"));
            _anim.SetFloat("Speed", Mathf.Abs(_moveDir.z));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = 0;
                _yVelocity += _jumpHeight;
                _anim.SetTrigger("Jump");
            }

            if (_moveDir.z != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _moveDir.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }
        }

        else if (!_controller.isGrounded)
        {
            _yVelocity -= _gravity;
        }

        _playerVelocity = _moveDir * _speed;
        _playerVelocity.y = _yVelocity;

        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}
