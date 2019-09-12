using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public bool hasWeapon = false;
    public bool hasBall = false;
    float timer;
    public GameObject weapon;
    public GameObject ball;
    public GameObject weaponHolder;
    public GameObject ballHolder;


    public AudioSource source;
    public AudioClip respawnBall;


    private void Start()
    {
        weaponHolder.SetActive(false);
        ballHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetButtonDown("J" + GetComponentInParent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().PlayerNumber + "B")) {


            if (hasWeapon) {
                source.PlayOneShot(respawnBall, 1.5F);
                timer = 2.0f;
                Instantiate(weapon, (transform.position), transform.rotation);
                hasWeapon = false;
                weaponHolder.SetActive(false);
            }

            if (hasBall) {
                source.PlayOneShot(respawnBall, 1.5F);
                timer = 0.5f;
                Instantiate(ball, (transform.position), transform.rotation);
                hasBall = false;
                ballHolder.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "pickupWeapon")
        {
            if (timer <= 0)
            {
                hasWeapon = true;
                Destroy(other.gameObject);
                weaponHolder.SetActive(true);
            }
        }
        if (other.tag == "pickupBall")
        {
            hasBall = true;
            Destroy(other.gameObject);
            ballHolder.SetActive(true);
        }
        if (other.tag == "ball")
        {
            if (timer <= 0)
            {
                hasBall = true;
                Destroy(other.gameObject);
                ballHolder.SetActive(true);
            }
        }
        else if (other.tag == "Player" && other.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().IsChargeEnd == false)
        {
            if (hasBall)
            {
                Instantiate(ball, (transform.position + new Vector3(4, 5, 2)), transform.rotation);
                hasBall = false;
                ballHolder.SetActive(false);

            }// Knock out script
            //GameObject gameObject = Instantiate(weapon, (transform.position), transform.rotation);
            //hasWeapon = false;
            //weaponHolder.SetActive(false);
        }
    }
}