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
                    // Input.GetKeyDown(KeyCode.Period)

                    photonView.RPC("SendMessage", PhotonTargets.AllBuffered, ChatInputField.text);
                    BubbleSpeechobject.SetActive(true);


                    DisableSend = true;

                    chatSend = false;

                }
            }







            if (Input.GetKeyDown(KeyCode.Return))
            {
                //  ChatInputField.gameObject.SetActive(true);
                Debug.Log(" outer loop ");

                if (!typing)
                {
                    Debug.Log(" select and set true input field ");
                    ChatInputField.gameObject.SetActive(true);
            
                    ChatInputField.ActivateInputField();
                    ChatInputField.Select();
                    chatSend = false;
                    typing = true;

                }
                else if (typing)
                {
                    Debug.Log("input field false, ");
                    ChatInputField.gameObject.SetActive(false);
                    chatSend = true;
                    typing = false;
                }



                /*
                if (ChatInputField.text.Length > 0)
                {
                    typing = true;
                }
                if (ChatInputField.text.Length <= 0)
                {
               //     ChatInputField.Select();
                    typing = false;

                }
                 */


                //Detect when the Return key is pressed down
            }

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
        yield return new WaitForSeconds(3f);
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
