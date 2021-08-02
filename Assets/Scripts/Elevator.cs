using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private Transform _startPos;
    [SerializeField]
    private Transform _targetPos;

    private Transform _currentTarget;
    private bool _moving = false;

    private void Start()
    {
        _currentTarget = _startPos;
    }

    void FixedUpdate()
    {
        // Move our position a step closer to the target
        if (_moving)
        {
            float step = _speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, step);
        }
    }

    public void CallElevator()
    {
        // Swap the position of the cylinder.
        if (_currentTarget.name == _startPos.name) { _currentTarget = _targetPos; }
        else { _currentTarget = _startPos; }

        _moving = true;
        float step = _speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, step);
    }
}
