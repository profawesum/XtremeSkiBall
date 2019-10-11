using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGoalCloser : MonoBehaviour
{
    public Transform redGoal;
    public Transform blueGoal;

    public float posToMove;


    private void OnTriggerEnter(Collider other)
    {
        if (this.tag == "goalPost") {
            redGoal.position += new Vector3(0, posToMove, 0);
        }
        if (this.tag == "goalPost2")
        {
            blueGoal.position += new Vector3(0, posToMove, 0);
        }
    }
}
