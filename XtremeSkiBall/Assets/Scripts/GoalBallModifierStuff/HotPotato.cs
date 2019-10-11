using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class HotPotato : MonoBehaviour
    {

        [SerializeField] RigidbodyFirstPersonController controller;

        //bools to check which type the goal ball is
        public bool hotPotato;
        public bool fireItUp;
        public bool slowThrow;
        public bool heavyBall;
        public bool BouncyBall;
        public bool slidyBall;
        public bool stickyBall;

        public GameObject hotPotatoText;
        public GameObject fireItUpText;
        public GameObject slowThrowText;
        public GameObject heavyBallText;
        public GameObject BouncyBallText;
        public GameObject slidyBallText;
        public GameObject stickyBallText;

        private void Start()
        {
            hotPotatoText = GameObject.Find("HotPotato");
            fireItUpText = GameObject.Find("FireitUp");
            slowThrowText = GameObject.Find("SlowBall");
            heavyBallText = GameObject.Find("HeavyBall");
            BouncyBallText = GameObject.Find("BouncyBall");
            slidyBallText = GameObject.Find("SlidyBall");
            stickyBallText = GameObject.Find("StickyBall");

            if (PlayerAssign.IsPlayerTwo && controller.PlayerNumber == 2)
            {
                hotPotatoText.SetActive(false);
                fireItUpText.SetActive(false);
                slowThrowText.SetActive(false);
                heavyBallText.SetActive(false);
                BouncyBallText.SetActive(false);
                slidyBallText.SetActive(false);
                stickyBallText.SetActive(false);
            }
            else if (!PlayerAssign.IsPlayerTwo && controller.PlayerNumber == 4) {
                hotPotatoText.SetActive(false);
                fireItUpText.SetActive(false);
                slowThrowText.SetActive(false);
                heavyBallText.SetActive(false);
                BouncyBallText.SetActive(false);
                slidyBallText.SetActive(false);
                stickyBallText.SetActive(false);
            }
        }


        //checks to see if the player has collided with a ball that has a modifier
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "hotPotato")
            {
                hotPotato = true;
                fireItUp = false;
                slowThrow = false;
                heavyBall = false;
                BouncyBall = false;
                slidyBall = false;
                stickyBall = false;
                hotPotatoText.SetActive(true);
                fireItUpText.SetActive(false);
                slowThrowText.SetActive(false);
                heavyBallText.SetActive(false);
                BouncyBallText.SetActive(false);
                slidyBallText.SetActive(false);
                stickyBallText.SetActive(false);
            }
            if (other.tag == "fireItUp")
            {
                hotPotato = false;
                fireItUp = true;
                slowThrow = false;
                heavyBall = false;
                BouncyBall = false;
                slidyBall = false;
                stickyBall = false;
                hotPotatoText.SetActive(false);
                fireItUpText.SetActive(true);
                slowThrowText.SetActive(false);
                heavyBallText.SetActive(false);
                BouncyBallText.SetActive(false);
                slidyBallText.SetActive(false);
                stickyBallText.SetActive(false);
            }
            if (other.tag == "slowThrow")
            {
                hotPotato = false;
                fireItUp = false;
                slowThrow = true;
                heavyBall = false;
                BouncyBall = false;
                slidyBall = false;
                stickyBall = false;
                hotPotatoText.SetActive(false);
                fireItUpText.SetActive(false);
                slowThrowText.SetActive(true);
                heavyBallText.SetActive(false);
                BouncyBallText.SetActive(false);
                slidyBallText.SetActive(false);
                stickyBallText.SetActive(false);
            }
            if (other.tag == "heavyBall")
            {
                hotPotato = false;
                fireItUp = false;
                slowThrow = false;
                heavyBall = true;
                BouncyBall = false;
                slidyBall = false;
                stickyBall = false;
                hotPotatoText.SetActive(false);
                fireItUpText.SetActive(false);
                slowThrowText.SetActive(false);
                heavyBallText.SetActive(true);
                BouncyBallText.SetActive(false);
                slidyBallText.SetActive(false);
                stickyBallText.SetActive(false);
            }
            if (other.tag == "bouncyBall")
            {
                hotPotato = false;
                fireItUp = false;
                slowThrow = false;
                heavyBall = false;
                BouncyBall = true;
                slidyBall = false;
                stickyBall = false;
                hotPotatoText.SetActive(false);
                fireItUpText.SetActive(false);
                slowThrowText.SetActive(false);
                heavyBallText.SetActive(false);
                BouncyBallText.SetActive(true);
                slidyBallText.SetActive(false);
                stickyBallText.SetActive(false);
            }
            if (other.tag == "slidyBall")
            {
                hotPotato = false;
                fireItUp = false;
                slowThrow = false;
                heavyBall = false;
                BouncyBall = false;
                slidyBall = true;
                stickyBall = false;
                hotPotatoText.SetActive(false);
                fireItUpText.SetActive(false);
                slowThrowText.SetActive(false);
                heavyBallText.SetActive(false);
                BouncyBallText.SetActive(false);
                slidyBallText.SetActive(true);
                stickyBallText.SetActive(false);
            }
            if (other.tag == "stickyBall")
            {
                hotPotato = false;
                fireItUp = false;
                slowThrow = false;
                heavyBall = false;
                BouncyBall = false;
                slidyBall = false;
                stickyBall = true;
                hotPotatoText.SetActive(false);
                fireItUpText.SetActive(false);
                slowThrowText.SetActive(false);
                heavyBallText.SetActive(false);
                BouncyBallText.SetActive(false);
                slidyBallText.SetActive(false);
                stickyBallText.SetActive(true);
            }
        }


        //checks what type the goal ball currently is
        public int checkBallType() {

            if (hotPotato == true)
            {
                return 1;
            }
            else if (fireItUp == true)
            {
                return 2;
            }
            else if (slowThrow == true)
            {
                return 3;
            }
            else if (heavyBall == true)
            {
                return 4;
            }
            else if (BouncyBall == true)
            {
                return 5;
            }
            else if (slidyBall == true)
            {
                return 6;
            }
            else if (stickyBall == true)
            {
                return 7;
            }
            return 0;
        }
    }
}
