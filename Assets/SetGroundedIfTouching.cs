using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGroundedIfTouching : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.tag);
        // Check if the character is grounded
        if (collision.gameObject.CompareTag("Ground") && transform.parent.parent.gameObject.GetComponent<PhotonView>().IsMine)
        {
            transform.parent.gameObject.GetComponent<PlayerController>().rightGrounded = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Debug.Log(collision.gameObject.tag);
        // Check if the character is no longer grounded
        if (collision.gameObject.CompareTag("Ground") && transform.parent.parent.gameObject.GetComponent<PhotonView>().IsMine)
        {
            transform.parent.gameObject.GetComponent<PlayerController>().rightGrounded = false;
        }
    }
}
