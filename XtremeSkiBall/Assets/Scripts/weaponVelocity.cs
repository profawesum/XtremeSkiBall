using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponVelocity : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;

    public float timer;


    public AudioSource source;
    public AudioClip respawnBall;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * (speed * Time.deltaTime));
    }
    

    private void Update()
    {
        timer+= Time.deltaTime;
        if (timer >= 15) {
            if (this.tag == "ball") {
                return;
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "killFloor" && this.tag == "ball") {
            source.PlayOneShot(respawnBall, 0.7F);
            this.transform.position = new Vector3(0, 0, 0);
            this.rb.velocity = Vector3.zero;
        }
    }
}
