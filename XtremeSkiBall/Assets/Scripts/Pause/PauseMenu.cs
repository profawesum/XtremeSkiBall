using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseCanvas;


    public void onContinue() {
        Time.timeScale = 1.0f;
        pauseCanvas.SetActive(false);      
    }
    public void returnToMenu() {
        Time.timeScale = 1.0f;
        Application.LoadLevel(0);
    }
    public void quitGame() {
        Application.Quit();
    }




}
