using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private Transform _respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.Damage();
            }

            CharacterController cc = other.GetComponent<CharacterController>();

            if (cc != null)
            {
                cc.enabled = false;
                StartCoroutine(CCEnableRoutine(cc));
            }

            other.transform.position = _respawnPoint.transform.position;
        }
    }

    IEnumerator CCEnableRoutine(CharacterController controller)
    {
        yield return new WaitForSeconds(0.5f);
        controller.enabled = true;
    }
}
