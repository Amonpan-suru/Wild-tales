using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class ScoreScript : NetworkBehaviour
{
    TMP_Text p1Text;
    TMP_Text p2Text;
    MainPlayer mainPlayer;

    
    public NetworkVariable<int> score = new NetworkVariable<int>(5);
    void Start()
    {
        p1Text = GameObject.Find("P1").GetComponent<TMP_Text>();
        p2Text = GameObject.Find("P2").GetComponent<TMP_Text>();
        mainPlayer = GetComponent<MainPlayer>();
    }

    private void UpdatePlayerNameAndScore()
    {
        if (IsOwnedByServer)
        {
            p1Text.text = $"{mainPlayer.networkString.PlayerName} : {score.Value}";
        }
        else
        {
            p2Text.text = $"{mainPlayer.networkString.PlayerName} : {score.Value}";
        }
    }


    void Update()
    {
        UpdatePlayerNameAndScore();
       
    }

    [ServerRpc]
    void UpdateScoreServerRpc(int newScore)
    {
        score.Value += newScore;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!IsLocalPlayer) return;

        if(collision.gameObject.tag == "DeathZone")
        {
                UpdateScoreServerRpc(-1);
        }
    }
}
