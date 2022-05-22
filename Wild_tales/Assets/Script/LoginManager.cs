using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Text;
using UnityEngine.UI;
using System;

public class LoginManager : MonoBehaviour
{
    public Text playerNameInputfield;
    public Text passwordInputfield;
    public GameObject loginUI;
    public GameObject leaveButton;
    public GameObject scorePanel;

    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += HandleServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnected;
        SetVisibility(false);
    }

    private void SetVisibility(bool isUserLogin)
    {
        if (isUserLogin)
        {
            loginUI.SetActive(false);
            leaveButton.SetActive(true);
            scorePanel.SetActive(true);
        }
        else
        {
            loginUI.SetActive(true);
            leaveButton.SetActive(false);
            scorePanel.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton == null) { return; }
        NetworkManager.Singleton.OnServerStarted -= HandleServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnected;
    }

    private void HandleClientConnected(ulong clientId)
    {
        Debug.Log("client id" + clientId);
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            SetVisibility(true);
        }
    }

    private void HandleClientDisconnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            SetVisibility(true);
        }
    }

    private void HandleServerStarted()
    {
        //throw new NotImplementedException();
        Debug.Log("");
    }

    public bool IsHost { get; }

    public void Leave()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.Shutdown();
            NetworkManager.Singleton.ConnectionApprovalCallback -= ApprovalCheck;
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.Shutdown();
        }
        SetVisibility(false);
    }

    public void Host()
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionData =
           System.Text.Encoding.ASCII.GetBytes(playerNameInputfield.text + "_" + passwordInputfield.text);
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost();

    }

    private void ApprovalCheck(byte[] connectionData, ulong clientId,
        NetworkManager.ConnectionApprovedDelegate callback)
    {
        //string playerName = Encoding.ASCII.GetString(connectionData);

        string Approve = System.Text.Encoding.ASCII.GetString(connectionData);
        string[] Room = Approve.Split("_");
        bool approve1 = Room[0] != playerNameInputfield.text;
        bool approve2 = Room[1] == passwordInputfield.text;
        //bool approveConnection = playerName != playerNameInputfield.text;

        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;

        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            spawnPos = new Vector3(4.12f, 1f, -3.8f);
        }
        else
        {
            switch (NetworkManager.Singleton.ConnectedClients.Count % 4)
            {
                case 0:
                    spawnPos = new Vector3(4.12f, 1f, -3.8f);
                    break;
                case 1:
                    spawnPos = new Vector3(4.12f, 1f, 3.63f);
                    break;
                case 2:
                    spawnPos = new Vector3(-4.22f, 1f, 3.63f);
                    break;
                case 3:
                    spawnPos = new Vector3(-4.22f, 1f, -3.63f);
                    break;
            }
        }

        NetworkLog.LogInfoServer("Spawn Pos of" + clientId + "is" + spawnPos.ToString());
        bool createPlayerObject = true;
        callback(createPlayerObject, null, approve1 && approve2, spawnPos, null);
    }

    public void Client()
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionData =
            System.Text.Encoding.ASCII.GetBytes(playerNameInputfield.text + "_" + passwordInputfield.text);

        NetworkManager.Singleton.StartClient();
    }
}