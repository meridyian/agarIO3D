using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEditor.Experimental.GraphView;

public class PlayerStateController : NetworkBehaviour
{
    
    [Networked(OnChanged = nameof(OnSizeChanged))]
    private float size { get; set; }
    private GameObject playerBody;
    private Rigidbody rb;
    public bool isBot;
    public static PlayerStateController Instance;


    [SerializeField] private Collider[] playerHitColliders;
    [SerializeField] private LayerMask collisionLayerMask;
    [SerializeField] private List<GameObject> collidedGoList;

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

    public void FixedUpdateNetwork()
    {
        Runner.GetPhysicsScene().OverlapSphere(transform.position, transform.localScale.magnitude + 0.2f,
            playerHitColliders, collisionLayerMask, QueryTriggerInteraction.UseGlobal);
        collidedGoList = new List<GameObject>();

        foreach (Collider col in playerHitColliders)
        {
            if (col.transform.CompareTag("Obstacle"))
            {
                Debug.Log("collided with obstacle " +
                          "");
            }
        }
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
