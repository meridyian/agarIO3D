using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerStateController : NetworkBehaviour
{
    
    [Networked(OnChanged = nameof(OnSizeChanged))]
    private float size { get; set; }
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
        }

       
    }

    public void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Food"))
        {
            other.transform.position = Utils.GetRandomSpawnPosition();
            UpdateSize();
            Debug.Log("collided with food");
        }
        
        /*-
        
        if(other.gameObject.CompareTag("Obstacle"))
        {
            var gameobject = Instantiate(playerBody, transform);

        }
        */

    }

    public void Reset()
    {
        
        size = 1f;
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

    
    
     */
    

    public void UpdateSize()
    {
        playerBody.transform.localScale +=  Vector3.one * 10 * (1/100f);
    }
    
}
