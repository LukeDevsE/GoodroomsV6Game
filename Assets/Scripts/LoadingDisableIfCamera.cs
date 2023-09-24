using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingDisableIfCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        transform.Find("LoadingScreen").gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null) {
            transform.Find("LoadingScreen").gameObject.SetActive(false);
            transform.Find("MenuButton").gameObject.SetActive(true);
            transform.Find("ChatInputField").gameObject.SetActive(true);
        }
    }
}
