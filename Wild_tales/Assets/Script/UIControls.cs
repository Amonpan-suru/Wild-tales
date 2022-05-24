using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    public GameObject TabUI;
    Animator Tab_Animator;
    
    bool tab = false ;
    private void Start()
    {
        Tab_Animator = TabUI.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            tab = !tab;
            Tab_Animator.SetBool("isOpen", tab);

            Debug.Log(tab);
        }
    }

    public void btnClose()
    {
        tab = !tab;
        Tab_Animator.SetBool("isOpen", tab);
    }
        
    
}