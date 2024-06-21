using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform checkpointA; // First checkpoint
    public Transform checkpointB; // Second checkpoint
    public float speed = 2f; // Speed of the platform
    private bool movingToB = true; // Determine direction of movement
    private bool isWaiting = false; // Check if platform is waiting
    public float StopTimeCheckpoint = 1f;

    void FixedUpdate()
    {
        if (isWaiting) return; // Skip movement while waiting

        // Determine target position
        Transform target = movingToB ? checkpointB : checkpointA;

        // Move platform towards target position
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);

        // Check if platform reached target position
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitBeforeSwitchingDirection());
        }
    }

    private IEnumerator WaitBeforeSwitchingDirection()
    {
        isWaiting = true;
        yield return new WaitForSeconds(StopTimeCheckpoint); // Wait for 1 second
        movingToB = !movingToB; // Toggle the direction of movement
        isWaiting = false;
    }

    void OnDrawGizmos()
    {
        // Draw lines in the editor to visualize the movement path
        if (checkpointA != null && checkpointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(checkpointA.position, checkpointB.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player character collides with the platform
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the platform");
            // Set the player character's parent to the platform
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player character exits the platform
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the platform");
            // Reset the player character's parent
            other.transform.SetParent(null);
        }
    }
}
