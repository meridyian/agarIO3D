using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;
using Random = UnityEngine.Random;


public class PlayerController : NetworkBehaviour
{
    private Rigidbody rb;
    public float speed = 10f;
    private Vector3 m_movement;

     void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public override void Spawned()
    {
        //random vector 2 ile harekete başlamış 
        // o geçişi ekleyebilirsin
    }
    

    public override void FixedUpdateNetwork()
    {
        //only move own player
        if (HasStateAuthority == false)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        m_movement.Set(horizontal, 0f, vertical);
        rb.AddForce(m_movement * speed);
    }
}
