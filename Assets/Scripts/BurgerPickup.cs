using UnityEngine;
using System.Collections;

public class BurgerPickup : MonoBehaviour
{
    public int pointToAdd;
    //private AudioSource CoinPickupEffect;

    void Start()
    {
        //CoinPickupEffect = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Jogador>() == null)

            return; // If the collision does not involve the Jogador (player) object, exit the function


        ScoreManager.AddPoints(pointToAdd);
        //CoinPickupEffect.Play(); // Play the sound effect

        // Check if SpriteRenderer exists
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // Disable the SpriteRenderer component
        }
    }
}
