using System.Collections;
using UnityEngine;

public class ShooterPlatforms : MonoBehaviour
{
    public GameObject bulletPrefab; // The bullet prefab to be instantiated
    public Transform shootPoint;    // The point from which the bullet will be shot
    public float shootDelay = 2f;   // Delay between shots in seconds
    public float bulletSpeed = 10f; // Speed of the bullet

    void Start()
    {
        // Start the automatic shooting coroutine
        StartCoroutine(ShootContinuously());
    }

    IEnumerator ShootContinuously()
    {
        while (true) // Infinite loop to keep shooting
        {
            // Instantiate the bullet at the shoot point's position and rotation
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

            // Get the Rigidbody component of the bullet and apply force to it
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = shootPoint.forward * bulletSpeed;
            }

            // Wait for the specified delay before the next shot
            yield return new WaitForSeconds(shootDelay);
        }
    }
}
