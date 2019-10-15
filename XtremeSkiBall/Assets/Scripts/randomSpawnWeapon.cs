using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawnWeapon : MonoBehaviour
{
    public GameObject weapon;

    public string[] weaponBallTypes = { "impactBall", "StunBall", "gravBall", "stealBall" };

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
        if (timer >= maxTimer)
        {
            string temp = weaponBallTypes[Random.Range(0, weaponBallTypes.Length)];
            weapon.tag = temp;
            Instantiate(weapon, transform);
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
