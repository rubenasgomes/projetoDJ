using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSwitch : MonoBehaviour
{
    public GameObject TogglePlatforms;
    //public Animator anim;
    //private AudioSource audioSource;
    public float interactionDistance = 3f; // Distance between the player and the switch
    private Transform playerTransform;

    void Start()
    {
        if (TogglePlatforms != null)
        {
            TogglePlatforms.SetActive(false);
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found. Make sure the player is tagged 'Player' and instantiated in the scene.");
        }

        //audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerTransform == null)
        {
            // Try to find the player again if not found in Start()
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                return; // Exit Update() if player is still not found
            }
        }

        // When pressing "E" near the switch, the platforms will be activated
        // The player has to be near the switch to activate it
        if (Vector3.Distance(playerTransform.position, transform.position) <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TogglePlatforms.SetActive(true); // the platforms are activated
                                                 //anim.SetTrigger("isPressing"); // the switch pressing animation is displayed
                                                 //audioSource.Play();
            }
        }
    }
}
