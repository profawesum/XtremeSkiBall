using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGoalCloser : MonoBehaviour
{
    public Transform redGoal;
    public Transform blueGoal;

    public float posToMove;


    private void Update()
    {
        if (redGoal.position.y <= -30) {
            Vector3 temp = new Vector3(redGoal.position.x, -30, redGoal.position.z);
            redGoal.position = temp;
        }
        if (blueGoal.position.y <= -30)
        {
            Vector3 temp = new Vector3(blueGoal.position.x, -30, blueGoal.position.z);
            blueGoal.position = temp;
        }
        if (redGoal.position.y >= 60)
        {
            Vector3 temp = new Vector3(redGoal.position.x, 60, redGoal.position.z);
            redGoal.position = temp;
        }
        if (blueGoal.position.y >= 60)
        {
            Vector3 temp = new Vector3(blueGoal.position.x, 60, blueGoal.position.z);
            blueGoal.position = temp;
        }
    }

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
