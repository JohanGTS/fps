using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemente : MonoBehaviour
{

    public float speed = 12f;
    public float gravity = -9.81f *2;
    public float jumpHeight=3f;
    private CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        controller= GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);
        Debug.Log(isGrounded);
        if (isGrounded && velocity.y<0)
        {
            velocity.y= -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right *x + transform.forward*z;
        controller.Move(move*speed *Time.deltaTime);

        if(Input.GetButtonDown("Jump" )&& isGrounded)
        {
            velocity.y= (float)Math.Sqrt(jumpHeight*-2f*gravity);
        }

        velocity.y+= gravity * Time.deltaTime;

        //controller.Move(velocity* Time.deltaTime);

        if (lastPosition!= gameObject.transform.position && isGrounded)
        {
            isMoving=true;
        }
        else
        {
            isMoving=false;
        }

        lastPosition= gameObject.transform.position;




    }
}
