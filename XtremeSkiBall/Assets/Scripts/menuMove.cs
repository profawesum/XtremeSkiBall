using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuMove : MonoBehaviour
{


    public void onPlayClicked() {
        Application.LoadLevel(2);
    }

    public void onQuitClick() {
        Application.Quit();
    }

}
