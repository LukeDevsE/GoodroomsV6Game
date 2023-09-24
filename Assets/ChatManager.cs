using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
public class ChatManager : MonoBehaviourPunCallbacks
{
    //public PlayerController plmove;
    //public PhotonView photonView;
    public GameObject BubbleSpeechObject;
    public TextMeshProUGUI UpdatedText;
    public TextMeshProUGUI _messages;
    private InputField ChatInputField;
    private bool DisableSend;
    // Start is called before the first frame update
    private void Awake()
    {
        //if (photonView.IsMine)
        //{
            GameObject.Find("Canvas").transform.Find("ChatInputField").gameObject.SetActive(true);
            ChatInputField = GameObject.Find("ChatInputField").GetComponent<InputField>();
            _messages = GameObject.Find("Chat").GetComponent<TextMeshProUGUI>();
            BubbleSpeechObject.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine) 
        {
            if (!DisableSend) 
            {
                if(ChatInputField.text != "" && ChatInputField.text.Length > 0 && Input.GetKeyDown(KeyCode.Return)) {
                    Debug.LogError("sent message");
                    photonView.RPC("SendMessage", RpcTarget.AllBuffered, ChatInputField.text, PhotonNetwork.NickName);
                    ChatInputField.text = "";
                }
                if (_messages.textInfo.lineCount > 12)
                {
                    int lastCharIndex = _messages.textInfo.lineInfo[0].lastCharacterIndex;
                    _messages.text = _messages.text.Substring(lastCharIndex + 1);
                }
            }
        }
    }

    [PunRPC()] 
    private void SendMessage(string message, string username) 
    {
        UpdatedText.text = message;
        if (username == "")
        {
            username = "Guest";
        }
        _messages.text += $"{username}: {message}\n";
        BubbleSpeechObject.SetActive(true);
        StartCoroutine("Remove");
        Debug.LogError("sent message");
    }
    IEnumerator Remove()
    {
        yield return new WaitForSeconds(4f);
        BubbleSpeechObject.SetActive(false);
        DisableSend = false;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(BubbleSpeechObject.active);
        }else if (stream.IsReading)
        {
            BubbleSpeechObject.SetActive((bool)stream.ReceiveNext());
        }
    }
}
