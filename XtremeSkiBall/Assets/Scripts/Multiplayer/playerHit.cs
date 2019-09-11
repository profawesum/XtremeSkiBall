using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;


public class playerHit : MonoBehaviour
{
    //[SyncVar]
    public float deaths = 0;

    public float timer;
    public bool timerStart;

    public Text healthText;
    public Text deathText;

    private int currentHealth = 100;

    private Vector3 initialPosition;

    private void Start()
    {
            deathText = GameObject.Find("Deaths").GetComponent<Text>();
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
            this.initialPosition = this.transform.position;
    }

    private void Update()
    {
            healthText.text = "Health: " + currentHealth.ToString();


            if (currentHealth <= 0) {
                RpcRespawn();
            }
            if (this.transform.position == initialPosition) {
                timer += Time.deltaTime;
                this.GetComponent<CharacterController>().enabled = false;
                if (timer >= 0.5f) {
                    deathText.text = "Deaths: " + deaths.ToString();
                    deaths += 1;
                    currentHealth = 100;
                    timer = 0;
                    this.GetComponent<CharacterController>().enabled = true;
                }
            }
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.tag == "weapon" && this.tag == "Player")
            {
                    currentHealth -= 5;
            }
    }


    void RpcRespawn()
    {

            this.transform.position = initialPosition;
    
    }
}