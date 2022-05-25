using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporttolibary : MonoBehaviour
{
    public GameObject traget,maincam;
    void Start()
    {
        
    }
    void Update()
    {
        Debug.Log(traget.transform);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            other.transform.position = traget.transform.position;
            maincam.transform.position = traget.transform.position;
        }
    }
    
}
