using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class goal2Socre : MonoBehaviour
{

    public int goal;

    public Text goalText;
    public Text goalText1;


    private void Update()
    {
        goalText.text = goal.ToString();
        goalText1.text = goal.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ball" && this.tag == "goal2")
        {
            goal++;
            goalText.text += goal.ToString();
            goalText1.text += goal.ToString();
            Debug.Log("Goal");
        }
    }
}
