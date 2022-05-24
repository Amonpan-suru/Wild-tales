using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerAnimation : NetworkBehaviour
{
    Animator player_Animator;
    bool isRunning = false;
    bool isWalking = false;

    void Start()
    {
        if (!IsLocalPlayer) enabled = false;
        player_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer) enabled = false;
        isWalking = false;
        isRunning = false;
        player_Animator.SetBool("isWalking", isWalking);
        player_Animator.SetBool("isRunning", isRunning);

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            isWalking = true;
            player_Animator.SetBool("isWalking", isWalking);
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            player_Animator.SetBool("isRunning", isRunning);
        }

    }
}
