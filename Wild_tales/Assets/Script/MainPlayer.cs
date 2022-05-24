using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class MainPlayer : NetworkBehaviour
{
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

    public float speed = 5.0f;
    public float rotationSpeed = 10f;
    Rigidbody rb;
    // public GameObject chatbox;

    public ChatManager chatManager ;
    public string username;


    public NetworkString networkString = new NetworkString();
    public LoginManager loginManager;

    public void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += playerconnect;
        if(!IsOwner ) return;
        GameObject canvas = GameObject.FindWithTag("MainCanvas");      
        rb = this.GetComponent<Rigidbody>();
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
        Debug.Log("user : " + Approve);
    }


    private void Update()
    {
         if(!IsLocalPlayer && IsServer){
            System.Text.Encoding.ASCII.GetBytes(username);
            byte[] user = System.Text.Encoding.ASCII.GetBytes(username);
            UpdateClientNameServerRpc(user);

        }
    } 

    private void FixedUpdate()
    {
        if (IsClient && IsOwner)
        {
            float translation = Input.GetAxis("Vertical") * speed;
            translation *= Time.deltaTime;
            rb.MovePosition(rb.position + this.transform.forward * translation);

            float rotation = Input.GetAxis("Horizontal");
            if (rotation != 0)
            {
                rotation *= rotationSpeed;
                Quaternion turn = Quaternion.Euler(0f, rotation, 0f);
                rb.MoveRotation(rb.rotation * turn);
            }
            else
            {
                // rb.angularVelocity = Vector3.zero;
            }

        }
    }


    public void Move()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            var randomPosition = GetRandomPositionOnPlane();
            transform.position = new Vector3(4.12f, 1f, -3.8f);
            Position.Value = new Vector3(4.12f, 1f, -3.8f);
        }
        else
        {
            SubmitPositionRequestServerRpc();
        }
    }

    [ServerRpc]
    void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
    {
        Position.Value = GetRandomPositionOnPlane();
    }

    static Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    }

}