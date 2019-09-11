using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePads : MonoBehaviour
{


    public GameObject[] walls;
    public GameObject[] spaceToRise;
    public GameObject wallToRaise;
    public GameObject risePoint;
    int index;
    int positionIndex;

    // Start is called before the first frame update
    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("wall");
        spaceToRise = GameObject.FindGameObjectsWithTag("risePoint");
    }

    // Update is called once per frame
    void Update()
    {
            foreach (GameObject wall in walls)
            {
                wall.transform.Translate(0, -Time.deltaTime * 0.1f, 0);
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            index = Random.Range(0, walls.Length);
            positionIndex = Random.Range(0, spaceToRise.Length);
            risePoint = spaceToRise[positionIndex];
            wallToRaise = walls[index];
            wallToRaise.transform.position = risePoint.transform.position;
        }
    }

}
