using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponVelocity : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * speed);
    }

    private void Update()
    {
        timer+= Time.deltaTime;
        if (timer >= 15) {
            Destroy(this.gameObject);
        }
    }
}
