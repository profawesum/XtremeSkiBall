using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{

    public float time = 20;
    public Text timeText;

    public goal2Socre goal2;
    public goalScore goal1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        time -= Time.deltaTime;
        string minutes = Mathf.Floor(time / 60).ToString("0");
        string seconds = (time % 60).ToString("00");
        timeText.text = "Time Remaining: " + time.ToString("F2");
        //timeText.text = "Time Remaining: " + string.Format("{0}:{1}", minutes, seconds);


        if (time <= 0) {
            time = 0;
            Time.timeScale = 0.3f;
            timeText.text = "Time Remaining: " + time.ToString("F2");
            if (goal1.goal > goal2.goal) {
                timeText.text = "BLUE WINS \n Press A To restart \n Press B to Quit \n Press X to Return to Menu";
                timeText.color = Color.blue;
                timeText.fontSize = 30;
            }
            else if (goal1.goal == goal2.goal) {
                timeText.text = "IT IS A TIE \n Press A To restart \n Press B to Quit \n Press X to Return to Menu";
                timeText.color = Color.green;
                timeText.fontSize = 30;
            }
            else
            {
                timeText.text = "RED WINS \n Press A To restart \n Press B to Quit \n Press X to Return to Menu";
                timeText.color = Color.red;
                timeText.fontSize = 30;
            }
            
            if (Input.GetKey(KeyCode.R) || Input.GetButton("J1A")){
                Time.timeScale = 1;
                Application.LoadLevel(Application.loadedLevel);
            }
            if (Input.GetKey(KeyCode.Q) || Input.GetButton("J1B")) {
                Application.Quit();
            }
            if (Input.GetKey(KeyCode.M) || Input.GetButton("J1C")) {
                Time.timeScale = 1;
                Application.LoadLevel(0);
            }
        }

    }
}
