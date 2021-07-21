using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    [SerializeField]
    private float _requiredDistanceFromCenter = 0.2f;
    [SerializeField]
    private Color _activatedColor = Color.green;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Vector3.Distance(transform.position, other.transform.position) <= _requiredDistanceFromCenter)
        {
            Rigidbody objectRb = other.GetComponent<Rigidbody>();

            if (objectRb != null)
            {
                objectRb.isKinematic = true;
            }

            _meshRenderer.material.color = _activatedColor;
        }
    }
}
