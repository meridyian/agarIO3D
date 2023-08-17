using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;
using Unity.VisualScripting;
using UnityEditor;
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
    private GameObject aiTarget;
    [SerializeField] List<GameObject> aiTargets;
    [SerializeField] private LayerMask foodLayermask;
    [SerializeField] Collider[] botHitcolliders = new Collider[10];
    [SerializeField] Collider[] playerHitcolliders = new Collider[10];
    [SerializeField] PlayerStateController _playerStateController;

    private GameObject playerBody;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        localCamera = GetComponentInChildren<CinemachineFreeLook>();
        playerBody = transform.GetChild(0).transform.gameObject;
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

    public void Update()
    {
        
    }


    public override void FixedUpdateNetwork()
    {
        if (_playerStateController.isBot)
        {
            //transform.GetComponentInChildren<SphereCollider>().radius = 5f;
            if (aiTarget == null)
            {
                // Collider[] hitcolliders = new Collider[10];
                Runner.GetPhysicsScene().OverlapSphere(transform.position, 15f, botHitcolliders, foodLayermask,
                    QueryTriggerInteraction.UseGlobal);
                
                aiTargets = new List<GameObject>();
                //Collider[] hitcolliders = Physics.OverlapSphere(playerBody.transform.position, 15f);
                foreach (Collider collider in botHitcolliders)
                {
                    if (collider.CompareTag("Food"))
                    {
                        aiTargets.Add(collider.gameObject);
                        Debug.Log("target added");
                    }
                    
                }

                aiTarget = aiTargets[Random.Range(0, aiTargets.Count)];
            }
            else
            {
                Vector2 vectorToTarget = (transform.position - aiTarget.transform.position).normalized;
                m_movement.Set(vectorToTarget.x, 0f, vectorToTarget.y);
                m_movement.y = 0f;
                rb.AddForce(m_movement);
                
                // rb.AddForce(m_movement * 100f);
            }
        }

        //only move own player
        if (Object.HasStateAuthority)
        {
       
            moveInput = actions.Player.Move.ReadValue<Vector2>();
            m_movement.Set(moveInput.x, 0f, moveInput.y);
            //m_movement = cameraMainTransform.forward * m_movement.z + cameraMainTransform.right * m_movement.x;
            m_movement.y = 0f;
            rb.AddForce(m_movement * speed);

            /*
            if (moveInput != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg
                                    + cameraMainTransform.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Runner.DeltaTime * 4f);
            }
            */
        }
    }
}