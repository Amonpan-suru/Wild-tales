using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerInfo : NetworkBehaviour
{
    public ChatManager chatManager ;
    public string username;


    public NetworkString networkString = new NetworkString();
    public LoginManager loginManager;

    public void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += playerconnect;
        if(!IsOwner ) return;   
        chatManager = FindObjectOfType<ChatManager>();     

        loginManager = GameObject.FindObjectOfType<LoginManager>();
        if (loginManager != null)
        {
            username = loginManager.playerNameInputfield.text;
            networkString.SetDataCollect(username);
            chatManager.username = username;

            System.Text.Encoding.ASCII.GetBytes(username);
            byte[] user = System.Text.Encoding.ASCII.GetBytes(username);
            UpdateClientNameServerRpc(user);

        }
       
              
    }

    private void playerconnect(ulong obj){
        if(IsLocalPlayer){
            System.Text.Encoding.ASCII.GetBytes(username);
            byte[] user = System.Text.Encoding.ASCII.GetBytes(username);
            UpdateClientNameServerRpc(user);

        }

    }

    [ServerRpc(RequireOwnership = false)] 
    public void UpdateClientNameServerRpc(byte[] name)
    {   
        UpdateClientNameClientRpc(name);
    }

    [ClientRpc]
    public void UpdateClientNameClientRpc(byte[] name)
    {
        string Approve = System.Text.Encoding.ASCII.GetString(name);
        username = Approve;
        this.gameObject.name = username;
        // Debug.Log("user : " + Approve);
    }


    private void Update()
    {
        if(!IsLocalPlayer && IsServer){
            System.Text.Encoding.ASCII.GetBytes(username);
            byte[] user = System.Text.Encoding.ASCII.GetBytes(username);
            UpdateClientNameServerRpc(user);

        }
    } 
}

