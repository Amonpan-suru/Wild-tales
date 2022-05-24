using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class ChatManager : NetworkBehaviour
{
    public string username;
    public GameObject chatpanel , textObject,chatboxcanvas;
    public InputField chatbox;

    public Color playerMessage ,info;
    public MainPlayer mainPlayer;
    // public GameObject Player;

    // public NetworkString networkString = new NetworkString();

    public int maxMessage = 25;
    [SerializeField]
    List<Message> messagesList = new List<Message>();

    public MessageText messageText = new MessageText();


    void Start(){
        chatboxcanvas = GameObject.Find("Chatbox");
        chatbox = chatboxcanvas.GetComponentInChildren<InputField>();
        chatpanel = GameObject.Find("Content");
        
    }

    void Update()
    {   
        // if (!IsLocalPlayer){
        //   return;  
        // } 

        // Player = GameObject.FindGameObjectWithTag("Player");
        // mainPlayer = GetComponent<MainPlayer>();
        // if(mainPlayer != null){
        //     if(mainPlayer.NetworkObject != IsLocalPlayer) return;
        // }

        if(chatbox.text != ""){
            if (Input.GetKeyDown(KeyCode.Return)){
                messageText.SetDataCollect(username +": " + chatbox.text);
                SendMessageTochatServerRpc(messageText,Message.MessageType.playerMessage);
                chatbox.text = "";
            }
        }
        else{
            if(!chatbox.isFocused&&Input.GetKeyDown(KeyCode.Return )){
                chatbox.ActivateInputField();
            }
        }

        // if(!chatbox.isFocused){
        //     if(Input.GetKeyDown(KeyCode.Space)){
        //     SendMessageTochatServerRpc("can use this mathon",Message.MessageType.info);
        //     }
        // }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SendMessageTochatServerRpc(MessageText text,Message.MessageType messageType){

        SendMessageTochatClientRpc(text,messageType);

    }

    [ClientRpc]
    public void SendMessageTochatClientRpc(MessageText text,Message.MessageType messageType){


        if(messagesList.Count >= maxMessage){

            Destroy(messagesList[0].textObject.gameObject);
            messagesList.Remove(messagesList[0]);

        }
        
        Message newMessage = new Message();
        newMessage.text = text.messagetext;
        GameObject newText  = Instantiate(textObject,chatpanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        newMessage.textObject.color = MessageColor(messageType);
        messagesList.Add(newMessage);
    }


    Color MessageColor(Message.MessageType messageType){
        Color color = info;

        switch(messageType){
            case Message.MessageType.playerMessage:
            color = playerMessage;
            break;
        }
        return color;
    }
}


[System.Serializable]
public class Message{
    public string text;
    public Text textObject;
    public MessageType messageType;

    public enum MessageType{
        playerMessage,
        info
    }
}

public struct MessageText : INetworkSerializable{
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref messagetext);
        
    }
     public string messagetext;

    public MessageText(string Messagetext)
    {
        messagetext = Messagetext;
    }

    public void SetDataCollect(string Messagetext)
    {
        messagetext = Messagetext;

    }

}
