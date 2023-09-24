using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class NameTag : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI nameText;
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine) {return;}

        SetName();
    }

    private void SetName() 
    {
        if (photonView.Owner.NickName == "")
        {
            nameText.text = "Guest";
        }
        else
        {
            nameText.text = photonView.Owner.NickName;
        }
    }
}
