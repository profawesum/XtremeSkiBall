using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public bool hasWeapon = false;
    public bool hasBall = false;
    public GameObject weapon;
    public GameObject ball;
    public GameObject weaponHolder;
    public GameObject ballHolder;


    private void Start()
    {
        weaponHolder.SetActive(false);
        ballHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (hasWeapon)
            {
                Instantiate(weapon, (transform.position), transform.rotation);
                hasWeapon = false;
                weaponHolder.SetActive(false);
            }

            if (hasBall) {
                Instantiate(ball, (transform.position + new Vector3(0,0,2)), transform.rotation);
                hasBall = false;
                ballHolder.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pickupWeapon")
        {
            hasWeapon = true;
            Destroy(other.gameObject);
            weaponHolder.SetActive(true);
        }
        if (other.tag == "pickupBall")
        {
            hasBall = true;
            Destroy(other.gameObject);
            ballHolder.SetActive(true);
        }
        if (other.tag == "ball")
        {
            hasBall = true;
            Destroy(other.gameObject);
            ballHolder.SetActive(true);
        }
    }
}