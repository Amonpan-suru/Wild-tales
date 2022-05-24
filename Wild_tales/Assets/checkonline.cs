using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Unity.Netcode;
using Cinemachine;

public class checkonline : NetworkBehaviour
{

    public CinemachineFreeLook cinemachineFreeLook;

    void Start()
    {    

        cinemachineFreeLook = this.GetComponentInChildren<CinemachineFreeLook>();

        if(!IsLocalPlayer){
            cinemachineFreeLook.enabled = false;
        }

      
    }

    void Update()
    {     
 
    }
}
