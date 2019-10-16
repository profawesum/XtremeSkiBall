using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class goal2Socre : MonoBehaviour
{
    public AudioSource p1Source;

    public AudioClip[] sounds;

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
        if (this.tag == "goal2" && other.tag == "ball" || this.tag == "goal2" && other.tag == "hotPotato" || this.tag == "goal2" && other.tag == "slowThrow"
            || this.tag == "goal2" && other.tag == "fireItUp" || this.tag == "goal2" && other.tag == "heavyBall" || this.tag == "goal2" && other.tag == "bouncyBall"
            || this.tag == "goal2" && other.tag == "slidyBall" || this.tag == "goal2" && other.tag == "stickyBall")
        {
            p1Source.PlayOneShot(sounds[Random.Range(0, sounds.Length)], 0.7F);
            goal++;
            goalText.text += goal.ToString();
            goalText1.text += goal.ToString();
            Debug.Log("Goal");
        }
    }
}
