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
    private int _lives = 3;
    private CharacterController _controller;
    private Vector3 _moveDir;
    private float _yVelocity;
    private bool _canDoubleJump;
    private int _coins;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _moveDir = new Vector3(0, 0, 0);
        _canDoubleJump = false;
    }

    // Update is called once per frame
    void Update()
    {

        _moveDir.x = Input.GetAxis("Horizontal");
        Vector3 velocity = _moveDir * _moveSpeed;

        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
            {
                _yVelocity += _jumpHeight;
                _canDoubleJump = false;
            }

            _yVelocity -= _gravityForce;
        }

        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);

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
}
