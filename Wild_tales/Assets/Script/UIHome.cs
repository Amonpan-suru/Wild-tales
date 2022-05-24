using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHome : MonoBehaviour
{
    public GameObject controlUI;
    public void startGame()
    {
        SceneManager.LoadScene("Testchat");
    }

    public void ControlOpen()
    {
        controlUI.SetActive(true);
    }

    public void ControlClose()
    {
        controlUI.SetActive(false);
    }

    void Start()
    {
        
    }
}
