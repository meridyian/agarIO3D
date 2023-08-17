using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
<<<<<<< HEAD
using UnityEditor.Experimental.GraphView;
=======
using Unity.VisualScripting;
>>>>>>> f311ca223b336d1407be5395610d94d4ace67d1f

public class PlayerStateController : NetworkBehaviour
{
    /*
    [Networked(OnChanged = nameof(OnSizeChanged))]
    private ushort size { get; set; }
    */
    private GameObject playerBody;
    private Rigidbody rb;
    public bool isBot;
    public static PlayerStateController Instance;
    
    // attributes for collision
    [SerializeField] private Collider[] playerHitColliders;
    [SerializeField] private LayerMask collisionLayermask;
    [SerializeField] private List<GameObject> collidedGoList;


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
            UpdateSize();
        }

       
    }

<<<<<<< HEAD
    public void FixedUpdateNetwork()
    {
        Runner.GetPhysicsScene().OverlapSphere(transform.position, transform.localScale.magnitude + 0.2f,
            playerHitColliders, collisionLayerMask, QueryTriggerInteraction.UseGlobal);
=======
    public override void FixedUpdateNetwork()
    {
        Runner.GetPhysicsScene().OverlapSphere(transform.position, transform.localScale.magnitude + 0.2f,
            playerHitColliders, collisionLayermask, QueryTriggerInteraction.UseGlobal);
>>>>>>> f311ca223b336d1407be5395610d94d4ace67d1f
        collidedGoList = new List<GameObject>();

        foreach (Collider col in playerHitColliders)
        {
<<<<<<< HEAD
            if (col.transform.CompareTag("Obstacle"))
            {
                Debug.Log("collided with obstacle " +
                          "");
            }
        }
=======
            if (col.CompareTag("Obstacle"))
            {
                // Split sphere into 5 different pieces 
                //col.transform.position = Utils.GetRandomSpawnPosition();
                Debug.Log("collided with obstacle");
            }
            /*
            else if (col.CompareTag("Food"))
            {
                //increase size 
                // static mi olsa 
                col.transform.position = Utils.GetRandomSpawnPosition();
            }

            else
            {
                if (transform.localScale.magnitude > col.transform.localScale.magnitude)
                {
                    Vector3 dirToPlayer = (transform.position - col.transform.position).normalized;
                    col.GetComponent<Rigidbody>().AddForce(dirToPlayer, ForceMode.Impulse);
                    // increaase size
                    // setActive false collided object
                }
                
                else if(transform.parent.GetComponent<PlayerController>())
                {
                    // game over
                }
            }
            */
        }




>>>>>>> f311ca223b336d1407be5395610d94d4ace67d1f
    }
    
    
    
    public void Reset()
    {
        //size = 1;
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
        //playerBody.transform.localScale = Vector3.one + Vector3.one * 100 * (size/65535f);
    }
    
}
