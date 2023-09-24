using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LeftSetGroundedIfTouching : MonoBehaviour
{
    // Start is called before the first frame update

    void OnTriggerEnter(Collider collision)
    {
        // Check if the character is grounded
        if (collision.gameObject.CompareTag("Ground") && transform.parent.parent.gameObject.GetComponent<PhotonView>().IsMine)
        {
            transform.parent.gameObject.GetComponent<PlayerController>().leftGrounded = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        // Check if the character is no longer grounded
        if (collision.gameObject.CompareTag("Ground") && transform.parent.parent.gameObject.GetComponent<PhotonView>().IsMine)
        {
            transform.parent.gameObject.GetComponent<PlayerController>().leftGrounded = false;
        }
    }
}
