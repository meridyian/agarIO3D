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
    private float horizontalMove;
    private float verticalMove;
    private ushort size = 5;
    private Vector2 inputDirection;

     void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }
    
    public override void Spawned()
    {
        //random vector 2 ile harekete başlamış 
        // o geçişi ekleyebilirsin
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        horizontalMove = movementVector.x;
        verticalMove = movementVector.y;
    }

    public override void FixedUpdateNetwork()
    {
        //only move own player
        if (HasStateAuthority == false)
        {
            return;
        }
        // keep the player within the playField
        /*
         * if(transform.position.x < Utils.GetPlayFieldSize()/2f * -1 + spriteRenderer.transform.localScale.x / 2f && movementDirection.x<0)
         *  movementDirection.x =0;
         *
         * if(transform.position.x>Utils.GetPlayFieldSize()/2f - spriteRenderer.transform.localScale.x / 2f && movementDirection.x>0)
         *  movementDirection.x =0;
         *
         * if(transform.position.y < Utils.GetPlayFieldSize()/2f * -1 + spriteRenderer.transform.localScale.y / 2f && movementDirection.y<0)
         *  movementDirection.y =0;
         *
         * if(transform.position.y>Utils.GetPlayFieldSize()/2f - spriteRenderer.transform.localScale.y / 2f && movementDirection.y>0)
         *  movementDirection.y =0;
         */

        Vector3 playerMovement = new Vector3(horizontalMove,0,verticalMove);
        
        float movementSpeed = (size / Mathf.Pow(size, 1.1f)) * 2;
        rb.AddForce(playerMovement*movementSpeed, ForceMode.Impulse);

    }
}
