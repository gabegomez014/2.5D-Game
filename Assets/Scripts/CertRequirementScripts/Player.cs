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
    private bool _jumping = false;
    private bool _onLedge = false;
    private Ledge _activeLedge;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.E) && _onLedge)
        {
            _anim.SetTrigger("Climbing");
        }
    }

    void CalculateMovement()
    {
        if (_controller.isGrounded)
        {
            _moveDir = new Vector3(0, 0, Input.GetAxisRaw("Horizontal"));
            _anim.SetFloat("Speed", Mathf.Abs(_moveDir.z));

            if (_jumping)
            {
                _jumping = false;
                _anim.SetBool("Jumping", _jumping);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = 0;
                _yVelocity += _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jumping", _jumping);
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

    public void GrabLedge(Vector3 grabLocation, Ledge currentLedge)
    {
        _controller.enabled = false;
        _anim.SetBool("GrabLedge", true);
        _anim.SetBool("Jumping", false);
        _anim.SetFloat("Speed", 0);
        transform.position = grabLocation;
        _onLedge = true;
        _activeLedge = currentLedge;
    }

    public void ClimbUpStart()
    {
        transform.position = _activeLedge.ClimbStartPosition();
    }

    public void ClimbUpComplete()
    {
        transform.position = _activeLedge.GetStandUpPosition();
        _anim.SetBool("GrabLedge", false);
        _controller.enabled = true;
    }
}
