using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _targetA;
    [SerializeField]
    private Transform _targetB;
    [SerializeField]
    private float _speed;

    private Transform _currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        _currentTarget = _targetB;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move our position a step closer to the target.
        float step = _speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, _currentTarget.position) < 0.001f)
        {
            // Swap the position of the cylinder.
            if (_currentTarget == _targetA) { _currentTarget = _targetB; }
            else { _currentTarget = _targetA; }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
