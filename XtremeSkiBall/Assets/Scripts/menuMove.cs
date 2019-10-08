using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuMove : MonoBehaviour
{
    public bool isRandom = false;

    public GameObject RandomText;
    public GameObject RegularText;
    public GameObject twoPlayerText;
    public GameObject fourPlayerText;

    private void Start()
    {
        RandomText.SetActive(false);
        RegularText.SetActive(false);
        twoPlayerText.SetActive(false);
        fourPlayerText.SetActive(false);
    }



    public void onPlayClicked() {
        RandomText.SetActive(true);
        RegularText.SetActive(true);
    }

    public void onRandomClicked() {
        isRandom = true;
        twoPlayerText.SetActive(true);
        fourPlayerText.SetActive(true);
    }
    public void onRegularClicked() {
        isRandom = false;
        twoPlayerText.SetActive(true);
        fourPlayerText.SetActive(true);
    }

    public void on2PlayerClicked() {
        //PlayerAssign.IsPlayerTwo = true;
        //load the random level with 2 players
        if (isRandom)
        {
            Application.LoadLevel(4);
        }
        //load the level that is not random with two players
        else {
            Application.LoadLevel(3);
        }
    }

    public void on4PlayerClicked() {
       // PlayerAssign.IsPlayerTwo = false;
        //load random level with 4 players
        if (isRandom)
        {
            Application.LoadLevel(5);
        }
        //load the level that is not random with four players
        else
        {
            Application.LoadLevel(2);
        }

    }

    //quit the game
    public void onQuitClick() {
        Application.Quit();
    }

}
