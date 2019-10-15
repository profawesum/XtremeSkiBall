﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that spawns players based on the number selected on the last screen
public class GameStart : MonoBehaviour
{
    [SerializeField] private GameObject RedPrefab;
    [SerializeField] private GameObject BluepreFab;
    [SerializeField] private Transform[] spawnPosTeam1;
    [SerializeField] private Transform[] spawnPosTeam2;
    private GameObject CurrentObject;
    private SplitScreen SplitRef;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerAssign.IsPlayerTwo = true;
        CurrentObject = Instantiate<GameObject>(RedPrefab, spawnPosTeam1[0]);
        CurrentObject.transform.position = spawnPosTeam1[0].position;
        CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().PlayerNumber = 1;
        CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().HUD.color = new Color(255, 0, 0);
        SplitRef = FindObjectOfType<SplitScreen>();
        SplitRef.Player1 = CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().cam;
        SplitRef.Player1Animation = CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().AnimationCam;
        CurrentObject = Instantiate<GameObject>(BluepreFab, spawnPosTeam2[0]);
        CurrentObject.transform.position = spawnPosTeam2[0].position;
        CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().HUD.color = new Color(0, 0, 255);
        CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().PlayerNumber = 2;
        SplitRef.Player2 = CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().cam;
        SplitRef.Player2Animation = CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().AnimationCam;
        //Assign the main cameras of the objects to the splitscreen.
        if (PlayerAssign.IsPlayerTwo)
        {
            return;
        }
        else
        {
            CurrentObject = Instantiate<GameObject>(RedPrefab, spawnPosTeam1[1]);
            CurrentObject.transform.position = spawnPosTeam1[1].position;
            CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().PlayerNumber = 3;
            CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().HUD.color = new Color(255, 0, 0);
            SplitRef.Player3 = CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().cam;
            SplitRef.Player3Animation = CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().AnimationCam;
            CurrentObject = Instantiate<GameObject>(BluepreFab, spawnPosTeam2[1]);
            CurrentObject.transform.position = spawnPosTeam2[1].position;
            CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().PlayerNumber = 4;
            CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().HUD.color = new Color(0, 0, 255);
            SplitRef.Player4 = CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().cam;
            SplitRef.Player4Animation = CurrentObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().AnimationCam;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
