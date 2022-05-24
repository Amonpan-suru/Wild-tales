using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnAimation : MonoBehaviour
{
    public GameObject selector;
    public Animator btn_Animator;

    private void Start()
    {
        btn_Animator = gameObject.GetComponent<Animator>();
    }

    public void PointEnter()
    {
        selector.SetActive(true);
        btn_Animator.SetBool("Hover", true);
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }

    public void PointOut()
    {
        selector.SetActive(false);
        btn_Animator.SetBool("Hover", false);
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
