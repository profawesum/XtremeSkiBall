using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawnWeapon : MonoBehaviour
{
    public GameObject weapon;


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
            Instantiate(weapon, this.transform.position, this.transform.rotation);
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
