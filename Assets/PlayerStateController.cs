using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerStateController : NetworkBehaviour
{
    
    [Networked(OnChanged = nameof(OnSizeChanged))]
    private ushort size { get; set; }
    private GameObject playerBody;
    private Rigidbody rb;
    public bool isBot;
    public static PlayerStateController Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        rb = GetComponent<Rigidbody>();
        playerBody = transform.GetChild(0).transform.gameObject;

    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Reset();
            UpdateSize();
        }

       
    }
    
    public void Reset()
    {
        size = 1;
    }
    
    public static void OnSizeChanged(Changed<PlayerStateController> changed)
    {
        
        changed.Behaviour.UpdateSize();
    }
    
    /*
    Collider[] results = new Collider[2];
    LayerMask layer;
    void CollisionCheck()
    {
        //Collider[] results = new Collider[2];
        
        //Collider hitColliders = PhysicsScene().OverlapSphere(playerBody.transform.position, playerBody.transform.localScale.x);
        //OverlapSphere(playerBody.transform.position, playerBody.transform.localScale.x, results);
        OnDCollectFood(12);
    }

    
    public void OnCollectFood(ushort growSize)
    {
        size += growSize;
        UpdateSize();
    }
     */

    public void UpdateSize()
    {
        playerBody.transform.localScale = Vector3.one + Vector3.one * 100 * (size/65535f);
    }
    
}
