using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChatManager : MonoBehaviour
{



   
    public PhotonView photonView;
    public GameObject BubbleSpeechobject;
    public Text UpdatedText;
    private InputField ChatInputField;

    private bool DisableSend;

    public bool chatSend = false;
    public bool typing = false;

    public bool chatButton;


    private void Awake()
    {
        ChatInputField = GameObject.Find("ChatInputField").GetComponent<InputField>();
        ChatInputField.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (photonView.isMine)
        {
            if (!DisableSend) //&& ChatInputField.isFocused
            {
                if (ChatInputField.text != "" && ChatInputField.text.Length > 0 && chatSend)
                {                
                    photonView.RPC("SendMessage", PhotonTargets.AllBuffered, ChatInputField.text);
                    BubbleSpeechobject.SetActive(true);

                    DisableSend = true;
                    chatSend = false;
                }
            }







            if (Input.GetKeyDown(KeyCode.Return))
            {
             
                if (!typing)
                {              
                    ChatInputField.gameObject.SetActive(true);
            
                    ChatInputField.ActivateInputField();
                    ChatInputField.Select();
                    chatSend = false;
                    typing = true;
                
                }
                else if (typing)
                {                
                    ChatInputField.gameObject.SetActive(false);
                    chatSend = true;
                    typing = false;
                 
                }            
            }
        }
    }

   
  


    public void chatButton_Pressed()
    {
        if (!chatButton)
        {        
            ChatInputField.gameObject.SetActive(true);
            ChatInputField.ActivateInputField();
            ChatInputField.Select();
            chatSend = false;
            chatButton = true;
        
        }
        else if (chatButton)
        {        
            ChatInputField.gameObject.SetActive(false);
            chatSend = true;
            chatButton = false;
            
        }
    }




    [PunRPC]
    private void SendMessage(string message)
    {   
        UpdatedText.text = message;
        ChatInputField.text = "";
        StartCoroutine("Remove");

    }
    IEnumerator Remove()
    {
        yield return new WaitForSeconds(4f);
        BubbleSpeechobject.SetActive(false);
        DisableSend = false;  
    }



    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
            stream.SendNext(BubbleSpeechobject.active);
        else if (stream.isReading)
            BubbleSpeechobject.SetActive((bool)stream.ReceiveNext());
    }
}
