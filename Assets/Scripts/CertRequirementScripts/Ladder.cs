using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField]
    private Transform bottomLadderPosition;
    [SerializeField]
    private Transform topLadderPosition;


    private void OnTriggerStay(Collider other)
    {
        // Check to see if the player is closer to the bottom or top of the ladder when this function is called
        // When the player hits E, mount the ladder and start climbing

        float bottomLadderDistance = Vector3.Distance(other.transform.position, bottomLadderPosition.position);

        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Player" && bottomLadderDistance <= 1)
        {
            Player player = other.GetComponent<Player>();
            player.StartClimbing(this);
        }
    }
}
