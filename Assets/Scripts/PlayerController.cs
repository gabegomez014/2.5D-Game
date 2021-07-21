using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 3;
    [SerializeField]
    private float _gravityForce = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15;
    [SerializeField]
    private float _horizontalWallJumpForce = 10;
    [SerializeField]
    private float _verticalWallJumpForce = 5;
    [SerializeField]
    private float _pushPower = 2;
    [SerializeField]
    private int _lives = 3;

    private CharacterController _controller;
    private Vector3 _moveDir, _velocity, _wallNormal;
    private float _yVelocity;
    private bool _canDoubleJump;
    private bool _canWallJump;
    private int _coins;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _moveDir = new Vector3(0, 0, 0);
        _canDoubleJump = false;
        _canWallJump = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (_controller.isGrounded)
        {
            _canWallJump = false;
            _moveDir.x = Input.GetAxis("Horizontal");
            _velocity = _moveDir * _moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_canWallJump)
                {
                    _canWallJump = false;
                    _velocity += _wallNormal * _horizontalWallJumpForce;
                    _yVelocity += _verticalWallJumpForce;
                }

                else if (_canDoubleJump)
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
            }

            _yVelocity -= _gravityForce;
        }

        _velocity.y = _yVelocity;

        _controller.Move(_velocity * Time.deltaTime);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Movable")
        {
            Rigidbody movableObject = hit.transform.GetComponent<Rigidbody>();

            if (movableObject != null)
            {
                Vector3 pushDirection = _moveDir;

                movableObject.velocity = pushDirection * _pushPower;
            }
        }

        if (!_controller.isGrounded && hit.transform.tag == "Wall")
        {
            Debug.DrawRay(hit.point, hit.normal, Color.white);
            _canWallJump = true;
            _wallNormal = hit.normal;
        }
    }

    public void AddCoin()
    {
        _coins += 1;
        UIManager.Instance.UpdateCoinsDisplay(_coins);
    }

    public void Damage()
    {
        _lives -= 1;
        UIManager.Instance.UpdateLivesDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    public int GetCoins()
    {
        return _coins;
    }
}
