using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawnWeapon : MonoBehaviour
{
    public GameObject weapon;
    public GameObject impactBall;
    public GameObject stunBall;
    public GameObject explosiveBall;
    public GameObject gravBall;

    public int rng;
    public bool restartTimer;
    public float timer;
    public float maxTimer;

    private void Start()
    {
        restartTimer = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (restartTimer == false) {
            timer += Time.deltaTime;
        }
        if (timer >= maxTimer) {
            rng = (int)((Random.Range(1, 2)) + 0.5);
            if (rng == 1)
            {
                Instantiate(weapon, this.transform.position, this.transform.rotation);
            }
            if (rng == 2)
            {
                Instantiate(impactBall, this.transform.position, this.transform.rotation);
            }
            if (rng == 3)
            {
                Instantiate(stunBall, this.transform.position, this.transform.rotation);
            }
            if (rng == 4)
            {
                Instantiate(explosiveBall, this.transform.position, this.transform.rotation);
            }
            if (rng == 5)
            {
                Instantiate(gravBall, this.transform.position, this.transform.rotation);
            }

            restartTimer = true;
            timer = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "firePoint")
        {
            restartTimer = false;
        }
    }
}
