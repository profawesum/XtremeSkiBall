using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPotato : MonoBehaviour
{

    //bools to check which type the goal ball is
    public bool hotPotato;
    public bool fireItUp;
    public bool slowThrow;
    public bool heavyBall;
    public bool BouncyBall;
    public bool slidyBall;
    public bool stickyBall;


    //checks to see if the player has collided with a ball that has a modifier
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hotPotato")
        {
            hotPotato = true;
            fireItUp = false;
            slowThrow = false;
            heavyBall = false;
            BouncyBall = false;
            slidyBall = false;
            stickyBall = false;
        }
        if (other.tag == "fireItUp")
        {
            hotPotato = false;
            fireItUp = true;
            slowThrow = false;
            heavyBall = false;
            BouncyBall = false;
            slidyBall = false;
            stickyBall = false;
        }
        if (other.tag == "slowThrow")
        {
            hotPotato = false;
            fireItUp = false;
            slowThrow = true;
            heavyBall = false;
            BouncyBall = false;
            slidyBall = false;
            stickyBall = false;
        }
        if (other.tag == "heavyBall")
        {
            hotPotato = false;
            fireItUp = false;
            slowThrow = false;
            heavyBall = true;
            BouncyBall = false;
            slidyBall = false;
            stickyBall = false;
        }
        if (other.tag == "bouncyBall")
        {
            hotPotato = false;
            fireItUp = false;
            slowThrow = false;
            heavyBall = false;
            BouncyBall = true;
            slidyBall = false;
            stickyBall = false;
        }
        if (other.tag == "slidyBall")
        {
            hotPotato = false;
            fireItUp = false;
            slowThrow = false;
            heavyBall = false;
            BouncyBall = false;
            slidyBall = true;
            stickyBall = false;
        }
        if (other.tag == "stickyBall")
        {
            hotPotato = false;
            fireItUp = false;
            slowThrow = false;
            heavyBall = false;
            BouncyBall = false;
            slidyBall = false;
            stickyBall = true;
        }
    }


    //checks what type the goal ball currently is
    public int checkBallType() {

        if (hotPotato == true)
        {
            return 1;
        }
        else if (fireItUp == true)
        {
            return 2;
        }
        else if (slowThrow == true)
        {
            return 3;
        }
        else if (heavyBall == true)
        {
            return 4;
        }
        else if (BouncyBall == true)
        {
            return 5;
        }
        else if (slidyBall == true)
        {
            return 6;
        }
        else if (stickyBall == true)
        {
            return 7;
        }
        return 0;
    }
}
