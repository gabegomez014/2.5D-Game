using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftSystem : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private Transform _startPos;
    [SerializeField]
    private Transform _targetPos;

    private Transform _currentTarget;
    private bool _moving = false;
    private bool _liftStarted = false;

    private void Start()
    {
        _currentTarget = _startPos;
        StartCoroutine(LiftDelay());
    }

    void FixedUpdate()
    {
        // Move our position a step closer to the target
        if (_moving)
        {
            float step = _speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, step);

            if (_moving && !_liftStarted && Vector3.Distance(transform.position, _currentTarget.position) <= 0.000001f)
            { 
                _moving = false;
                StartCoroutine(LiftDelay());
            }

        }
    }

    public void CallElevator()
    {
        if (_currentTarget.name == _startPos.name) { _currentTarget = _targetPos; }
        else { _currentTarget = _startPos; }
        _moving = true;
        float step = _speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, step);
    }

    IEnumerator LiftDelay()
    {
        yield return new WaitForSeconds(2);
        CallElevator();
        _liftStarted = true;
        yield return new WaitForSeconds(3);
        _liftStarted = false;
    }

}
