using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class goalScore : MonoBehaviour
{

    public int goal;

    public Text goalText;
    

    private void Update()
    {
        goalText.text = goal.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ball" && this.tag == "goal") {
            goal ++;
            goalText.text += goal.ToString();
            Debug.Log("Goal");
        }
    }
}
