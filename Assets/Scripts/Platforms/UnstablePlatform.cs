using UnityEngine;

public class UnstablePlatform : MonoBehaviour
{
    private bool isShaking = false;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (isShaking)
        {
            // Shake the platform for 2 seconds
            float shakeAmount = 0.1f; // Adjust as needed
            transform.position = originalPosition + new Vector3(Random.Range(-shakeAmount, shakeAmount), 0, Random.Range(-shakeAmount, shakeAmount));

            // After 2 seconds, fall
            Invoke("Fall", 2f);
        }
    }

    void Fall()
    {
        // Disable the platform
        gameObject.SetActive(false);

        // Reappear after 1 second
        Invoke("Reappear", 1f);
    }

    void Reappear()
    {
        // Reset position and re-enable the platform
        transform.position = originalPosition;
        gameObject.SetActive(true);

        // Reset the shaking state
        isShaking = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Start shaking when the player hits the platform
            isShaking = true;
        }
    }
}
