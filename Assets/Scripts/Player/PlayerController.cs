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
    
    public float speed = 10f;

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
        rb.AddForce(m_movement * speed);
    }
}
