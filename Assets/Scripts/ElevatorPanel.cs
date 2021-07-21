using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _callBtn;
    [SerializeField]
    private Color _activatedColor;
    [SerializeField]
    private int _requiredCoins = 8;
    [SerializeField]
    private Elevator _elevator;

    private Material _material;
    private Color _originalColor;
    private bool _elevatorCalled;
    private bool _inElevatorArea;

    PlayerController player;

    private void Start()
    {
        _material = _callBtn.material;
        _originalColor = _material.color;
        _elevatorCalled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _inElevatorArea && player != null && player.GetCoins() >= _requiredCoins)
        {
            _elevator.CallElevator();
            _elevatorCalled = !_elevatorCalled;

            if (_elevatorCalled) { _material.color = _activatedColor; }
            else { _material.color = _originalColor; }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _inElevatorArea = true;
            player = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _inElevatorArea = false;
            player = null;
        }
    }
}
