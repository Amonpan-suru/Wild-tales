using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EmoteHotKey : MonoBehaviour
{
    Animator player_Animator;
    bool isRunning = false;
    bool isWalking = false;

    void Start()
    {
        //if (!IsLocalPlayer) enabled = false;
        //player_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Keypad1))
        //{
        //    tab = !tab;
        //    Tab_Animator.SetBool("isOpen", tab);

        //    Debug.Log(tab);
        //}
    }
}
