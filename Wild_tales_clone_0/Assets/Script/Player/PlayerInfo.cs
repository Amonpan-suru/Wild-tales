using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerInfo : NetworkBehaviour
{
    MainPlayer mainPlayer;
    public NetworkString networkString;
    public string playernameinfo;
    void Start()
    {
        mainPlayer = GetComponent<MainPlayer>();
        networkString = mainPlayer.networkString;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsOwner){
            NameInfoServerRpc(mainPlayer.networkString);
        }
        else {NameInfoClientRpc(mainPlayer.networkString);}
    }

    [ServerRpc]
    void NameInfoServerRpc(NetworkString name){
        playernameinfo = mainPlayer.networkString.PlayerName;
    }
    
    [ClientRpc]
    void NameInfoClientRpc(NetworkString name){
        playernameinfo = mainPlayer.networkString.PlayerName;
    }
}
