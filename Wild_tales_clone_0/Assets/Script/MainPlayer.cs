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


    public Text namePrefab;
    private Text nameLable;

    public NetworkString networkString = new NetworkString();
    public LoginManager loginManager;

    public PlayerInfo playerInfo;

    public void Start()
    {

        GameObject canvas = GameObject.FindWithTag("MainCanvas");
        nameLable = Instantiate(namePrefab, Vector3.zero, Quaternion.identity) as Text;
        nameLable.transform.SetParent(canvas.transform);
        rb = this.GetComponent<Rigidbody>();
        playerInfo = GetComponent<PlayerInfo>();


        if(IsOwner){
            
            loginManager = GameObject.FindObjectOfType<LoginManager>();
            if (loginManager != null)
            {
                networkString.SetDataCollect(loginManager.playerNameInputfield.text);
                UpdateClientNameServerRpc(networkString);
            }
        }
        else{
            networkString.SetDataCollect(loginManager.playerNameInputfield.text);
            return;
        }
           
        
    }

    [ServerRpc(RequireOwnership = false)] 
    public void UpdateClientNameServerRpc(NetworkString name)
    {           
        nameLable.text = name.PlayerName;
        UpdateClientNameClientRpc(name);
    }

    [ClientRpc]
    public void UpdateClientNameClientRpc(NetworkString name)
    {
        nameLable.text = name.PlayerName;
    }

    void SetPlayerName()
    {
        Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.5f, 0));
        UpdateClientNameServerRpc(networkString);
        nameLable.transform.position = nameLabelPos;
       
    }

    private void Update()
    {
        SetPlayerName();
  
    } 

    private void OnDestroy()
    {
        if(nameLable != null)
        {
            Destroy(nameLable.gameObject);
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