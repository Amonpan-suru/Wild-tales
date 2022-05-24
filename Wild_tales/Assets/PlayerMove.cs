using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class PlayerMove : NetworkBehaviour
{
    public float speed = 6f;
    public float rotationSpeed = 10f;
    public GameObject mainCam;
    public Transform cam;
    float turnSmoothVelocity;
    // Update is called once per frame
    public Rigidbody rb;
    public CinemachineFreeLook Vcam;


    private void Start()
    {
        if (!IsLocalPlayer) enabled = false;
        mainCam = GameObject.Find("Main Camera");
        cam = mainCam.GetComponent<Transform>();
        Vcam = FindObjectOfType<CinemachineFreeLook>();

        Vcam.gameObject.name = "cam_" + this.name;

        if(!IsLocalPlayer){
            Vcam.enabled = false;
            this.enabled = false;
        }
        
    }
    void Update()
    {
        // if (!IsLocalPlayer) enabled = false;
        
        // float horizontal = Input.GetAxisRaw("Horizontal");
        // float vertical = Input.GetAxisRaw("Vertical");
        // Vector3 direction = new Vector3(horizontal, 0f, vertical);

        // if (direction.magnitude >= 0.1f)
        // {
        //     float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        //     float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        //     transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //     // Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        // }

            
    }

    private void FixedUpdate()
    {
      
            rb = GetComponentInParent<Rigidbody>();
            float translation = Input.GetAxis("Vertical") * speed;
            translation *= Time.deltaTime;
            rb.MovePosition(rb.position + this.transform.forward * translation);

            float rotation = Input.GetAxis("Horizontal");
            if (rotation != 0)
            {
                rotation *= rotationSpeed;
                Quaternion turn = Quaternion.Euler(0f, rotation, 0f);
                rb.MoveRotation(rb.rotation * turn);
            }
            else
            {
                // rb.angularVelocity = Vector3.zero;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 10;
                Debug.Log("speed");
            }
            else
            {
                speed = 6;
            }

        
    }

}


