using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGoalCloser : MonoBehaviour
{
    public Transform goal;
    public Transform goal2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ball" && this.tag == "goalPost") {
            goal.position += new Vector3(0, 0, -20);
        }
        if (other.tag == "ball" && this.tag == "goalPost2")
        {
            goal2.position += new Vector3(0, 0, 20);
        }
    }
}
