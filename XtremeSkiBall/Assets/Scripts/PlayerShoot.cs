using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class PlayerShoot : MonoBehaviour
    {
        //get access to the script HotPotato which holds the values for the random ball modifiers
        [SerializeField] HotPotato randomBallModifiers;
        [SerializeField] RigidbodyFirstPersonController playerController;
        [SerializeField] Image UIBallImage;
        [SerializeField] Text UIText;
        [SerializeField] Sprite GoalBall;
        [SerializeField] Sprite GravBall;
        [SerializeField] Sprite StunBall;
        [SerializeField] Sprite ImpactBall;
        //[SerializeField] Sprite StealBall;

        //physics materials for the random balls
        public PhysicMaterial stickyMaterial;
        public PhysicMaterial defaultMaterial;
        public PhysicMaterial bouncyMaterial;
        public PhysicMaterial iceMaterial;

        public string weaponType;

        //bools
        public bool hasWeapon = false;
        public bool hasBall = false;
        public bool randomBallMode = true;

        //floats
        public float timer;
        float dropTimer;
        public float ballMass;

        //various gameObjects
        public GameObject weapon;
        public GameObject ball;
        public GameObject weaponHolder;
        public GameObject ballHolder;

        //audio sources
        //public AudioSource source;
        public AudioClip respawnBall;

        //used for the random gameMode when the player throws a ball
        public string[] ballTypes = { "hotPotato", "fireItUp", "slowThrow", "heavyBall", "bouncyBall", "slidyBall", "stickyBall" };


        private void Start()
        {
            UIBallImage.color = new Color(UIBallImage.color.r, UIBallImage.color.g, UIBallImage.color.b, 0.0f);
            UIText.text = "None";
            playerController = GetComponentInParent<RigidbodyFirstPersonController>();
            randomBallModifiers = GetComponent<HotPotato>();
            //disable the ball holders
            weaponHolder.SetActive(false);
            ballHolder.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            //timer for the hotPotato Ball
            if (hasBall)
            {
                //checks to see if the ball being held is of type hot potato
                if (randomBallModifiers.checkBallType() == 1 && randomBallMode)
                {
                    //increase the timer
                    dropTimer++;
                    //if the timer reaches a set number then force the player to throw the ball
                    if (dropTimer >= 3)
                    {
                        timer = 3.0f;
                        Instantiate(ball, (transform.position), transform.rotation);
                        hasBall = false;
                        ballHolder.SetActive(false);
                    }
                }
            }

            timer -= Time.deltaTime;
            //if the player wants to throw the ball, they can press 'B'
            if (Input.GetButtonDown("J" + GetComponentInParent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().PlayerNumber + "B") || Input.GetKey(KeyCode.C))
            {
                //if the player has a weapon ball
                if (hasWeapon)
                {
                    if(weaponType == "StunBall")
                    {
                        //source.PlayOneShot(respawnBall, 1.5F);
                        timer = 2.0f;
                        //create and throw a ball
                        playerController.timer = 1.5f;
                        weapon.tag = "stunBallThrown";
                        Instantiate(weapon, (transform.position), transform.rotation);
                        hasWeapon = false;
                        weaponHolder.SetActive(false);
                    }
                    if (weaponType == "ImpactBall") {

                        //source.PlayOneShot(respawnBall, 1.5F);
                        timer = 2.0f;
                        //create and throw a ball
                        playerController.timer = 1.5f;
                        weapon.tag = "impactBallThrown";
                        Instantiate(weapon, (transform.position), transform.rotation);
                        hasWeapon = false;
                        weaponHolder.SetActive(false);
                    }
                    if (weaponType == "StealBall") {
                        //source.PlayOneShot(respawnBall, 1.5F);
                        timer = 2.0f;
                        //create and throw a ball
                        playerController.timer = 1.5f;
                        weapon.tag = "stealBall";
                        Instantiate(weapon, (transform.position), transform.rotation);
                        hasWeapon = false;
                        weaponHolder.SetActive(false);

                    }
                    //disable the UI Ball
                    UIBallImage.color = new Color(UIBallImage.color.r, UIBallImage.color.g, UIBallImage.color.b, 0.0f);
                    UIText.text = "None";
                }

                //if the player has the goalable ball
                if (hasBall)
                {
                    //checks to see if the random game mode is enabled
                    if (randomBallMode)
                    {

                        //switch based on what ball there is
                        switch (randomBallModifiers.checkBallType())
                        {
                            //hotPotato
                            case 1:
                                {
                                    ball.GetComponent<BoxCollider>().material = defaultMaterial;
                                    //implemented before this so just skip this then
                                    ball.GetComponent<Rigidbody>().mass = 0.1f;
                                    //disable the UI Ball
               
                                }
                                break;
                            //fireItUp
                            case 2:
                                {
                                    ball.GetComponent<BoxCollider>().material = defaultMaterial;
                                    //the ball moves upwards in the weapon velocity script
                                    ball.GetComponent<Rigidbody>().mass = 0.1f;
                                }
                                break;
                            //slowThrow
                            case 3:
                                {
                                    ball.GetComponent<BoxCollider>().material = defaultMaterial;
                                    //the ball moves slower in the weapon velocity script
                                    ball.GetComponent<Rigidbody>().mass = 0.25f;
                                }
                                break;
                            //heavy Ball
                            case 4:
                                {
                                    ball.GetComponent<BoxCollider>().material = defaultMaterial;
                                    ball.GetComponent<Rigidbody>().mass = 0.5f;
                                }
                                break;
                            //BouncyBall
                            case 5:
                                {
                                    ball.GetComponent<BoxCollider>().material = defaultMaterial;
                                    ball.GetComponent<BoxCollider>().material = bouncyMaterial;
                                    ball.GetComponent<Rigidbody>().mass = 0.1f;
                                }
                                break;
                            //slidy Ball
                            case 6:
                                {
                                    ball.GetComponent<BoxCollider>().material = defaultMaterial;
                                    ball.GetComponent<BoxCollider>().material = iceMaterial;
                                    ball.GetComponent<Rigidbody>().mass = 0.1f;
                                }
                                break;
                            //sticky ball
                            case 7:
                                {
                                    ball.GetComponent<BoxCollider>().material = defaultMaterial;
                                    ball.GetComponent<BoxCollider>().material = stickyMaterial;
                                    ball.GetComponent<Rigidbody>().mass = 0.1f;
                                }
                                break;
                            //reset the ball
                            default:
                                {
                                    ball.GetComponent<BoxCollider>().material = defaultMaterial;
                                    ball.GetComponent<Rigidbody>().mass = 0.1f;
                                }
                                break;
                        }

                    }// end of random ball mode

                    //launch the goal ball
                    //source.PlayOneShot(respawnBall, 1.5F);
                    timer = 3.0f;
                    Instantiate(ball, (transform.position), transform.rotation);
                    //make it so the player does not have the ball and set the holder to false
                    hasBall = false;
                    ballHolder.SetActive(false);
                    //disable the UI Ball
                    UIBallImage.color = new Color(UIBallImage.color.r, UIBallImage.color.g, UIBallImage.color.b, 0.0f);
                    UIText.text = "None";
                }
            }
        }

        public void dropBall() {
            if (hasBall)
            {
                //make them drop the ball
                Instantiate(ball, (transform.position + new Vector3(4, 5, 2)), transform.rotation);
                hasBall = false;
                ballHolder.SetActive(false);
                UIBallImage.color = new Color(UIBallImage.color.r, UIBallImage.color.g, UIBallImage.color.b, 0.0f);
                UIText.text = "None";
            }
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "killFloor") {
                if (hasBall) {
                    dropBall();
                }
                if (hasWeapon)
                {
                    //make them drop the ball
                    hasWeapon = false;
                    weaponHolder.SetActive(false);
                    //disable the UI Ball
                    UIBallImage.color = new Color(UIBallImage.color.r, UIBallImage.color.g, UIBallImage.color.b, 0.0f);
                    UIText.text = "None";
                }
            }


            #region pickup

            //if the player collides with a weapon tagged object
            if (other.tag == "pickupWeapon")
            {
                //check to make sure they have not just thrown it
                if (timer <= 0)
                {
                    //pickup the ball and enable the weapon holder
                    hasWeapon = true;
                    Destroy(other.gameObject);
                    weaponHolder.SetActive(true);
                    //Need it to check what ball it is.
                    UIBallImage.sprite = StunBall;
                    UIBallImage.color = new Color(UIBallImage.color.r, UIBallImage.color.g, UIBallImage.color.b, 1.0f);
                    UIText.text = "Goal Ball";
                }
            }
            //if the player collides with a ball tagged pickup ball 
            if (other.tag == "pickupBall")
            {
                if (timer <= 0)
                {
                    //give them the ball
                    hasBall = true;
                    Destroy(other.gameObject);
                    ballHolder.SetActive(true);
                    UIBallImage.sprite = GoalBall;
                    UIBallImage.color = new Color(UIBallImage.color.r, UIBallImage.color.g, UIBallImage.color.b, 1.0f);
                    UIText.text = "GoalBall";
                }
            }

            if (other.tag == "stunBall")
            {
                if (timer <= 0)
                {
                    hasWeapon = true;
                    weaponHolder.SetActive(true);
                    Destroy(other.gameObject);
                    UIBallImage.sprite = StunBall;
                    UIBallImage.color = new Color(UIBallImage.color.r, UIBallImage.color.g, UIBallImage.color.b, 1.0f);
                    UIText.text = "Stun Ball";
                    weaponType = "StunBall";
                }
            }

            if (other.tag == "impactBall")
            {
                if (timer <= 0)
                {
                    hasWeapon = true;
                    weaponHolder.SetActive(true);
                    Destroy(other.gameObject);
                    UIBallImage.sprite = ImpactBall;
                    UIBallImage.color = new Color(UIBallImage.color.r, UIBallImage.color.g, UIBallImage.color.b, 1.0f);
                    UIText.text = "Impact Ball";
                    weaponType = "ImpactBall";
                }
            }

            if (other.tag == "stealBall")
            {
                if (timer <= 0)
                {
                    hasWeapon = true;
                    weaponHolder.SetActive(true);
                    Destroy(other.gameObject);
                    UIBallImage.sprite = StunBall;
                    UIBallImage.color = new Color(UIBallImage.color.r, UIBallImage.color.g, UIBallImage.color.b, 1.0f);
                    UIText.text = "Steal Ball";
                    weaponType = "StealBall";
                }
            }

            //if the player collides with a goal ball
            if (other.tag == "ball" || other.tag == "hotPotato" || other.tag == "fireItUp" || other.tag == "slowThrow" || other.tag == "heavyBall" || other.tag == "bouncyBall" || other.tag == "slidyBall" || other.tag == "stickyBall")
            {
                if (timer <= 0)
                {
                    //give them the ball
                    hasBall = true;
                    Destroy(other.gameObject);
                    ballHolder.SetActive(true);
                    UIBallImage.sprite = GoalBall;
                    UIBallImage.color = new Color(UIBallImage.color.r, UIBallImage.color.g, UIBallImage.color.b, 1.0f);
                    UIText.text = "GoalBall";
                    if (randomBallMode) {
                       string temp = ballTypes[Random.Range(0, ballTypes.Length)];
                        ball.tag = temp;
                        Debug.Log(ball.tag);
                    }
                    if (other.tag == "hotPotato") {
                        UIBallImage.color = new Color(UIBallImage.color.r, UIBallImage.color.g, UIBallImage.color.b, 0.0f);
                        UIText.text = "None";
                    }
                }
            }

            #endregion

                //if a charging player collides with another player
                else if (other.tag == "Player" && other.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().IsChargeEnd == false)
                {
                    //if the player that was hit with the charge has the ball
                    if (hasBall)
                    {
                        //make them drop the ball
                        Instantiate(ball, (transform.position + new Vector3(4, 5, 2)), transform.rotation);
                        hasBall = false;
                        ballHolder.SetActive(false);
                    }
                }
            //if the player collides with a steal ball that is 
            //thrown then drop the goal ball if they have it
            if (other.tag == "stealBall") {
                if (hasBall) {
                    dropBall();
                }
            }
        }
    }
}