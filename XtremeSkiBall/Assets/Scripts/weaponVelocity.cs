using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponVelocity : MonoBehaviour
{
    //used for the random gameMode when the player throws a ball
    public string[] ballTypes = { "hotPotato", "fireItUp", "slowThrow", "heavyBall", "bouncyBall", "slidyBall", "stickyBall" };

    //components
    public Rigidbody rb;
    public GameObject ballResetPos;

    //floats
    public float speed;
    public float timer;

    //audio
    public AudioSource source;
    public AudioClip respawnBall;

    // Start is called before the first frame update
    void Start()
    {
        //if the ball is tagged with fireItUp then launch the ball upwards when fired
        if (this.tag == "fireItUp") {
            rb.AddForce(transform.up * (speed * Time.deltaTime) * 3);
        }
        //if the ball has the tag slowBall
        if (this.tag == "slowBall")
        {
            //make it so the ball moves slower
            rb.AddForce(transform.forward * ((speed * Time.deltaTime) / 2));
        }
        if (this.tag != "slowBall" || this.tag != "fireItUp")
        {
            //add force to the ball
            rb.AddForce(transform.forward * (speed * Time.deltaTime));
        }
        if (this.tag == "hotPotato") {
            //add force to the ball
            rb.AddForce(transform.forward * (50000 * Time.deltaTime));
        }
 
    }


    private void Update()
    {
        timer+= Time.deltaTime;
        if (timer >= 15) {

            //if it is a weapon then destroy it after 15 seconds once thrown
            if (this.tag == "weapon")
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the ball hits the killFloor tag then respawn it in the center with no velocity
        if (other.tag == "killFloor" && this.tag == "ball"|| other.tag == "killFloor" && this.tag == "hotPotato" || other.tag == "killFloor" && this.tag == "slowThrow"
            || other.tag == "killFloor" && this.tag == "fireItUp" || other.tag == "killFloor" && this.tag == "heavyBall" || other.tag == "killFloor" && this.tag == "bouncyBall" 
            || other.tag == "killFloor" && this.tag == "slidyBall" || other.tag == "killFloor" && this.tag == "stickyBall") {

            this.tag = ballTypes[Random.Range(0, ballTypes.Length)];
            source.PlayOneShot(respawnBall, 0.7F);
            this.transform.position = ballResetPos.transform.position;
            this.rb.velocity = Vector3.zero;
        }
    }
}
