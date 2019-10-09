using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFiring : MonoBehaviour
{
    [SerializeField]List<Transform> PointHolders;
    private float angle;
    [SerializeField] private GameObject PreFab;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(SimulateProjectile());
        }
    }

    IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(1.5f);
        GameObject currentObject = Instantiate(PreFab, transform);
        currentObject.transform.position = transform.position;
        currentObject.GetComponent<Rigidbody>().isKinematic = true;
        // Calculate distance to target
        float target_Distance = Vector3.Distance(transform.position, PointHolders[0].position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        currentObject.transform.rotation = Quaternion.LookRotation(PointHolders[0].position - currentObject.transform.position);

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
    }
}
