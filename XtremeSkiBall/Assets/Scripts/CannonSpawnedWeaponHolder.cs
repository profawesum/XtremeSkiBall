using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSpawnedWeaponHolder : MonoBehaviour
{
    bool isUp;

    [SerializeField] private float speed = 1.0f;
    float RotationSpeed;

    Vector3 startPos;
    Vector3 endPos;

    GameObject CurrentObject;
    //bool
    public bool isHoldingWeapon;

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotates the ball around when there is one
        if(CurrentObject != null)
        {
            CurrentObject.transform.Rotate(Vector3.up * (RotationSpeed * Time.deltaTime));
            CurrentObject.transform.position = transform.position;
            Hover();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "firePoint")
        {
            isHoldingWeapon = false;
            CurrentObject = null;
        }
        else if (other.tag == "pickupWeapon")
        {
            CurrentObject = other.gameObject;
            isHoldingWeapon = true;
        }
    }
    void Hover()
    {
        Vector3 oso = Vector3.up * Mathf.Sin(speed * Time.time);
        transform.position = startPos + oso;
    }
}
