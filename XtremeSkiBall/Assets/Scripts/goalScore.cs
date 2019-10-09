using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class goalScore : MonoBehaviour
{

    public AudioSource p1Source;
    public AudioClip goalSfx;


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
        if (this.tag == "goal" && other.tag == "ball" || this.tag == "goal" && other.tag == "hotPotato" || this.tag == "goal" && other.tag == "slowThrow"
             || this.tag == "goal" && other.tag == "fireItUp" || this.tag == "goal" && other.tag == "heavyBall" || other.tag == "goal" && other.tag == "bouncyBall"
             || this.tag == "goal" && other.tag == "slidyBall" || this.tag == "goal" && other.tag == "stickyBall")
        {
            p1Source.PlayOneShot(goalSfx, 0.7F);
            goal ++;
            goalText.text += goal.ToString();
            goalText1.text += goal.ToString();
            Debug.Log("Goal");
        }
    }
}
