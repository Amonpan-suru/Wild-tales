using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerAnimation : NetworkBehaviour
{
    Animator player_Animator;
    bool isRunning = false;
    bool isWalking = false;
    bool isGreeting = false;
    bool isWaving = false;
    bool isHappy = false;
    bool isDance = false;
    bool isClapping = false;
    bool isAngry = false;
    bool isSitting = false;
    bool isSad = false;

    int num = 1;

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
        num = 0;
        player_Animator.SetInteger("num", num);


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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            num = 1;
            isGreeting = !isGreeting;
            player_Animator.SetBool("isGreeting", isGreeting);
            player_Animator.SetInteger("num", num);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            num = 2;
            isWaving = !isWaving;
            player_Animator.SetBool("isWaving", isWaving);
            player_Animator.SetInteger("num", num);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            num = 3;
            isHappy = !isHappy;
            player_Animator.SetBool("isHappy", isHappy);
            player_Animator.SetInteger("num", num);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            num = 4;
            isDance = !isDance;
            player_Animator.SetBool("isDance", isDance);
            player_Animator.SetInteger("num", num);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            num = 5;
            isClapping = !isClapping;
            player_Animator.SetBool("isClapping", isClapping);
            player_Animator.SetInteger("num", num);
        }

        
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            num = 6;
            isSitting = !isSitting;
            player_Animator.SetBool("isSitting", isSitting);
            player_Animator.SetInteger("num", num);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            num = 7;
            isAngry = !isAngry;
            player_Animator.SetBool("isAngry", isAngry);
            player_Animator.SetInteger("num", num);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            num = 8;

            isSad = !isSad;
            player_Animator.SetBool("isSad", isSad);
            player_Animator.SetInteger("num", num);
        }
    }
}
