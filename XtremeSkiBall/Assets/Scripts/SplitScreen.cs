using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreen : MonoBehaviour
{
    [SerializeField] public Camera Player1;
    [SerializeField] public Camera Player2;
    [SerializeField] public Camera Player3;
    [SerializeField] public Camera Player4;

    [SerializeField] public Camera Player1Animation;
    [SerializeField] public Camera Player2Animation;
    [SerializeField] public Camera Player3Animation;
    [SerializeField] public Camera Player4Animation;

    [SerializeField] private bool isTwoPlayer;

    private bool isHorizontal;
    // Start is called before the first frame update
    void Start()
    {
        isTwoPlayer = PlayerAssign.IsPlayerTwo;
        if (isTwoPlayer || Player3 == null || Player4 == null)
        {
            Player1.rect = new Rect(0, 0.0f, 1, 0.5f);
            Player2.rect = new Rect(0, 0.5f, 1, 0.5f);
            Player1Animation.rect = new Rect(0, 0.0f, 1, 0.5f);
            Player2Animation.rect = new Rect(0, 0.5f, 1, 0.5f);
        }
        else
        {
            Player1Animation.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
            Player2Animation.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
            Player3Animation.rect = new Rect(0, 0, 0.5f, 0.5f);
            Player4Animation.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            Player1.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
            Player2.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
            Player3.rect = new Rect(0, 0, 0.5f, 0.5f);
            Player4.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
        }
        isHorizontal = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeSplitScreen()
    {
        isHorizontal = !isHorizontal;
        if (isTwoPlayer)
        {
            if (isHorizontal)
            {
                Player2.rect = new Rect(0, 0, 1, 0.5f);
                Player1.rect = new Rect(0, 0.5f, 1, 0.5f);
            }
            else
            {
                Player2.rect = new Rect(0, 0, 0.5f, 1);
                Player1.rect = new Rect(0.5f, 0, 0.5f, 1);
            }
        }
    }
}
