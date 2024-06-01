using UnityEngine;
using System.Collections;

public class BurgerPickup : MonoBehaviour
{
    public int pointToAdd;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Jogador>() == null)

            return; // If the collision does not involve the Jogador (player) object, exit the function


        ScoreManager.AddPoints(pointToAdd);

        // Check if SpriteRenderer exists
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // Disable the SpriteRenderer component
        }
    }
}
