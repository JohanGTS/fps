using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float mouseSensitivity=500f;
    float xRotation =0f;
    float yRotation =0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")* mouseSensitivity* Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y")* mouseSensitivity* Time.deltaTime;
        xRotation-= mouseY;

        xRotation = Math.Clamp(xRotation,-90f,90f);
        
        yRotation+=mouseX;
        transform.localRotation= Quaternion.Euler(xRotation,yRotation,0f);


    }
}
