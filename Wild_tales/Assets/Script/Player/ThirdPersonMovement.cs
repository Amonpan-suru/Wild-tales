using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ThirdPersonMovement : NetworkBehaviour
{
    public CharacterController controller;
    public float turnSmoothTime = 0.1f;
    public float speed = 6f;
    public GameObject mainCam;
    public Transform cam;
    float turnSmoothVelocity;
    // Update is called once per frame


    private void Start()
    {
        if (!IsLocalPlayer) enabled = false;
        mainCam = GameObject.Find("Main Camera");
        cam = mainCam.GetComponent<Transform>();
    }

    void Update()
    {
        if (!IsLocalPlayer) enabled = false;
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 10;
                Debug.Log("speed");
            }
            else
            {
                speed = 6;
            }

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
