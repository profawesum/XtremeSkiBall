﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that spawns players based on the number selected on the last screen
public class GameStart : MonoBehaviour
{
    [SerializeField] private GameObject preFab;
    [SerializeField] private Transform[] spawnPosTeam1;
    [SerializeField] private Transform[] spawnPosTeam2;
    private GameObject CurrentObject;
    private SplitScreen SplitRef;
    // Start is called before the first frame update
    void Start()
    {
        CurrentObject = Instantiate<GameObject>(preFab, spawnPosTeam1[0]);
        CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().PlayerNumber = 1;
        SplitRef = FindObjectOfType<SplitScreen>();
        SplitRef.Player1 = CurrentObject.GetComponentInChildren<Camera>();
        CurrentObject = Instantiate<GameObject>(preFab, spawnPosTeam2[0]);
        CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().PlayerNumber = 2;
        SplitRef.Player2 = CurrentObject.GetComponentInChildren<Camera>();
        //Assign the main cameras of the objects to the splitscreen.
        if (PlayerAssign.IsPlayerTwo)
        {
            return;
        }
        else
        {
            CurrentObject = Instantiate<GameObject>(preFab, spawnPosTeam1[1]);
            CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().PlayerNumber = 3;
            SplitRef.Player3 = CurrentObject.GetComponentInChildren<Camera>();
            CurrentObject = Instantiate<GameObject>(preFab, spawnPosTeam2[1]);
            CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().PlayerNumber = 4;
            SplitRef.Player4 = CurrentObject.GetComponentInChildren<Camera>();
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
