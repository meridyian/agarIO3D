using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;
using Random = UnityEngine.Random;


public class PlayerController : NetworkBehaviour
{
    public CinemachineFreeLook localCamera;
    public static PlayerController Local { get; set; }
    private Rigidbody rb;
    private PlayerControl actions;
    private Vector2 moveInput;
    private Vector3 m_movement;
    private Transform cameraMainTransform;
    
    public float speed = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        localCamera = GetComponentInChildren<CinemachineFreeLook>();
        actions = new PlayerControl();
    }
    
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            localCamera.transform.parent = null;
            actions.Player.Enable();
            cameraMainTransform = Camera.main.transform;
        }
        else
        {
            localCamera.enabled = false;
        }
    }
    

    public override void FixedUpdateNetwork()
    {
        //only move own player
        if (HasStateAuthority == false)
        {
            return;
        }

        moveInput = actions.Player.Move.ReadValue<Vector2>();
        m_movement.Set(moveInput.x, 0f, moveInput.y);
        m_movement = cameraMainTransform.forward * m_movement.z + cameraMainTransform.right * m_movement.x;
        m_movement.y = 0f;
        rb.AddForce(m_movement * speed);

        if (moveInput != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg 
                + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Runner.DeltaTime * 4f);

        }
    }
}
