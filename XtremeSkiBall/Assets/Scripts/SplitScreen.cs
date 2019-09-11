using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreen : MonoBehaviour
{
    [SerializeField] Camera Player1;
    [SerializeField] Camera Player2;

    private bool isHorizontal;
    // Start is called before the first frame update
    void Start()
    {
        Player2.rect = new Rect(0, 0, 1, 0.5f);
        Player1.rect = new Rect(0, 0.5f, 1, 0.5f);
        isHorizontal = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeSplitScreen()
    {
        isHorizontal = !isHorizontal;
        if(isHorizontal)
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
