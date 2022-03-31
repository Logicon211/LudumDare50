using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunDebug : MonoBehaviour
{                    // Distance in Unity units over which the Debug.DrawRay will be drawn

    private Camera fpsCam;                                // Holds a reference to the first person camera
    public Transform bulletSpawnPoint;

    void Start()
    {
        // Get and store a reference to our Camera by searching this GameObject and its parents
        fpsCam = GetComponentInParent<Camera>();
    }


    void Update()
    {
        // Create a vector at the center of our camera's viewport
        Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        Vector3 rayOrigin = bulletSpawnPoint.position;
        // Draw a line in the Scene View  from the point lineOrigin in the direction of fpsCam.transform.forward * weaponRange, using the color green
        Debug.DrawRay(lineOrigin, fpsCam.transform.forward * 200, Color.green);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, 200f))
        {
            Debug.DrawRay(rayOrigin, fpsCam.transform.forward * 200, Color.red);
            Debug.DrawRay(rayOrigin, hit.point, Color.blue);

        }
    }
}
