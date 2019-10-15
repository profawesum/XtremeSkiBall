using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFiring : MonoBehaviour
{
    bool justFired;
    [SerializeField]List<CannonSpawnedWeaponHolder> PointHolders;
    private float angle;
    [SerializeField] private GameObject PreFab;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    public string[] weaponBallTypes = {"impactBall", "StunBall" };

    //Update the cannon When adding a new prefab
    enum BallTypes
    {
        STANDARD
    }

    // Update is called once per frame
    void Update()
    {
        if(justFired == false)
        {
            StartCoroutine(SimulateProjectile());
        }
    }

    IEnumerator SimulateProjectile()
    {
        if(PointHolders.Count <= 0)
        {
            Debug.LogWarning("Points in list are not filled. Will not fire");
            yield break;
        }
        int randomSpot = Random.Range(0, (PointHolders.Count));
        if (PointHolders[randomSpot].isHoldingWeapon == true)
        {
            Debug.Log("Don't fire at that spot");
            yield break;
        }
        string temp = weaponBallTypes[Random.Range(0, weaponBallTypes.Length)];
        PreFab.tag = temp;
        GameObject currentObject = Instantiate(PreFab, transform);
        currentObject.transform.position = transform.position;
        currentObject.GetComponent<Rigidbody>().isKinematic = true;
        // Calculate distance to target
        float target_Distance = Vector3.Distance(transform.position, PointHolders[randomSpot].transform.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        currentObject.transform.rotation = Quaternion.LookRotation(PointHolders[randomSpot].transform.position - currentObject.transform.position);
        //Stops it from firing again
        justFired = true;
        float elapse_time = 0;
        while (elapse_time < flightDuration)
        {
            currentObject.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
        if(elapse_time >= flightDuration)
        {
            currentObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        while(elapse_time < flightDuration + 1.0f)
        {
            if (justFired == true)
            {
                justFired = false;
                yield break;
            }
            elapse_time += Time.deltaTime;
        }
    }
    GameObject RandomBall()
    {
        // Update the enum when new ballTypes are made as the last value is used to go through the enum
        int randomValue = Random.Range(0, (int)BallTypes.STANDARD);
        switch (randomValue)
        {
            case (int)BallTypes.STANDARD:
                {
                    return PreFab;
                }
            default:
                {
                    Debug.LogWarning("Value was not on the list. Returning basic weapon ball");
                    return PreFab;
                }
        }
    }
}
