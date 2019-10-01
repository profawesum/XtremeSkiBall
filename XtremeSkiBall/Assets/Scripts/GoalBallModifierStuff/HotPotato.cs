using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPotato : MonoBehaviour
{

    public Rigidbody rb;
    public GameObject goalBall;

    public float timer;

    public bool hotPotato;
    public bool fireItUp;
    public bool slowThrow;
    public bool heavyBall;
    public bool BouncyBall;
    public bool slidyBall;
    public bool stickyBall;


    public enum ballModifier { fireItUp };


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    int checkBallType() {

        if (hotPotato == true)
        {
            return 1;
        }
        else if (fireItUp == true)
        {
            //set it up so the ball fires upwards
        }
        else if (slowThrow == true)
        {
            //reduce the balls mass and the velocity
        }
        else if (heavyBall == true)
        {
            //change the balls mass to be heavier
        }
        else if (BouncyBall == true)
        {
            //apply a physics material
        }
        else if (slidyBall == true)
        {
            return 0;
        }
        else if (stickyBall == true)
        {
            return 0;
        }
        else
        {
            return 0;
        }
    }



}
