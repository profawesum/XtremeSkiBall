using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class splashMove : MonoBehaviour
{


    public GameObject videoplayer; // GameObject having the attached video
    public VideoPlayer videofile; // VideoPLayer component 
    public bool setAwake = false;

    private void Awake()
    {
        setAwake = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (videofile.isPaused) {
            Application.LoadLevel(1);
        }
    }
}
