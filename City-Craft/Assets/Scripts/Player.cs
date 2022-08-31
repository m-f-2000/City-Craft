using System;
using System.Collections;
using System.Collections.Generic;
using Gp7;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float yaw = 0.0f, pitch = 0.0f;
    private Rigidbody rb;
    public Camera PlayerCamera;

    [SerializeField] float walkspeed = 0.0f, sensitivity = 2.0f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement();
        if (Input.GetKey(KeyCode.LeftShift) && Physics.Raycast(rb.transform.position, Vector3.down, 0.3f + 0.001f))
            rb.velocity = new Vector3(rb.velocity.x, 2.0f, rb.velocity.z);
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        pitch -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -90.0f, 90.0f);
        yaw += Input.GetAxisRaw("Mouse X") * sensitivity;
        PlayerCamera.transform.localRotation = Quaternion.Euler(pitch, yaw, 0);
    }

    private void Movement()
    {
        Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")).normalized * walkspeed;
        Vector3 forward = new Vector3(-PlayerCamera.transform.right.z, 0.0f, PlayerCamera.transform.right.x);
        Vector3 wishDirection = (forward * axis.x + PlayerCamera.transform.right * axis.y + Vector3.up * rb.velocity.y);
        rb.velocity = wishDirection;
    }
}
