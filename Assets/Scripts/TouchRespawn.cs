using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reset the position of this object to (0, 0, 0)
            collision.transform.localPosition = new Vector3(0f, 3f, 0f);
            Debug.Log("Touched");
            
        }
    }
}
