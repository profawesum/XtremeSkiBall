﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerOnePlayer : MonoBehaviour
{
    public float time = 20;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeText.text = "Time Remaining: " + time.ToString("F2");
        if (time <= 0)
        {
            time = 0;
            timeText.text = "Time Remaining: " + time.ToString("F2");
            //if (goal1.goal > goal2.goal)
            //{
            //    timeText.text = "BLUE WINS \n Press A To restart \n Press B to Quit \n Press X to Return to Menu";
            //    timeText.color = Color.blue;
            //    timeText.fontSize = 30;
     
            //}
            //else if (goal1.goal == goal2.goal)
            //{
            //    timeText.text = "IT IS A TIE \n Press A To restart \n Press B to Quit \n Press X to Return to Menu";
            //    timeText.color = Color.green;
            //    timeText.fontSize = 30;
            //}
            //else
            //{
            //    timeText.text = "RED WINS \n Press A To restart \n Press B to Quit \n Press X to Return to Menu";
            //    timeText.color = Color.red;
            //    timeText.fontSize = 30;
            //}
        }

    }
}
