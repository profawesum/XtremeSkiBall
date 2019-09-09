using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public bool hasWeapon = false;
    public GameObject weapon;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            if (hasWeapon) {
                Instantiate(weapon, transform.position, transform.rotation);
                hasWeapon = false;
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pickupWeapon") {
            hasWeapon = true;
            Destroy(other.gameObject);
        }
    }



}
