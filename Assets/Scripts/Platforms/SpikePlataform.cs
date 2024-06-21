using UnityEngine;
using System.Collections;

public class SpikePlatform : MonoBehaviour
{
    public float downDuration = 0.5f; // Duration for the spike to move down
    public float bottomStayDuration = 2f; // Duration the spike stays at the bottom
    public float upDuration = 0.1f; // Duration for the spike to move up rapidly
    public float initialStayDuration = 3f; // Duration the spike stays at the initial position
    public float moveDistance = 2f; // Distance the spike moves down
    private Vector3 initialPosition; // Initial position of the spike
    private Vector3 downPosition; // Position the spike moves down to

    void Start()
    {
        initialPosition = transform.localPosition;
        downPosition = initialPosition - new Vector3(0, moveDistance, 0);
        StartCoroutine(SpikeMovement());
    }

    private IEnumerator SpikeMovement()
    {
        while (true)
        {
            // Move the spike down
            float elapsedTime = 0f;
            while (elapsedTime < downDuration)
            {
                transform.localPosition = Vector3.Lerp(initialPosition, downPosition, elapsedTime / downDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = downPosition; // Ensure it reaches the down position

            // Stay at the down position
            yield return new WaitForSeconds(bottomStayDuration);

            // Move the spike up rapidly
            elapsedTime = 0f;
            while (elapsedTime < upDuration)
            {
                transform.localPosition = Vector3.Lerp(downPosition, initialPosition, elapsedTime / upDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = initialPosition; // Ensure it reaches the initial position

            // Stay at the initial position
            yield return new WaitForSeconds(initialStayDuration);
        }
    }
}
