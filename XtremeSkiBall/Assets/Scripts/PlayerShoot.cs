using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public bool hasWeapon = false;
    public GameObject weapon;
    public GameObject weaponHolder;


    private void Start()
    {
        weaponHolder.SetActive(false);
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
    }
}