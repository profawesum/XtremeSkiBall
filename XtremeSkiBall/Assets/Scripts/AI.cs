using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Vector3 LastSeenPosition;
    public float FiringRange { get; private set; }
    public float PickupDistance = 0.5f;
    public List<Transform> WeaponPositions;
    public GameObject Destination;
    public GameObject[] Players;
    public Collider[] playerColls;
    public LayerMask obstacleMask;
    public Camera AiCam { get; private set; }
    public Plane[] planes;
    public Transform Target { get; private set; }
    public int currentTarget;

    public NavMeshAgent Agent;

    public StateMachine StateMachine => GetComponent<StateMachine>();
    public bool hasWeapon {get; private set; }

    private void Awake()
    {
        FiringRange = 1.0f;
        Agent = GetComponent<NavMeshAgent>();
        AiCam = GetComponentInChildren<Camera>();
        Players = FindObjectOfType<CharacterInfo>().Players;
        playerColls = FindObjectOfType<CharacterInfo>().PlayersCoil;
        InitializeStateMachine();
        hasWeapon = true;
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(WanderState), new WanderState(this) },
            {typeof(ChaseState), new ChaseState(this) },
            {typeof(AttackState), new AttackState(this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }

    public void SetTarget(Transform _Target, int _CurrentPlayer)
    {
        Target = _Target;
        currentTarget = _CurrentPlayer;
    }
    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    public void FireWeapon()
    {
        //Firing Script stuff goes here
        hasWeapon = false;
    }

    // Update is called once per frame
    void Update()
    {       
    }

    private void OnTriggerEnter(Collider other)
    {
        //Pickup Script goes here
    }
}
