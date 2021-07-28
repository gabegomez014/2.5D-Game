using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField]
    private Transform _grabLocation;
    [SerializeField]
    private Transform _standUpPos;
    [SerializeField]
    private Transform _climbStartPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ledge_Grab_Checker")
        {
            Player player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                player.GrabLedge(_grabLocation.position, this);
            }
        }
    }

    public Vector3 GetStandUpPosition()
    {
        return _standUpPos.position;
    }

    public Vector3 ClimbStartPosition()
    {
        return _climbStartPos.position;
    }
}
