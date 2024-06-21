using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSwitch : MonoBehaviour
{
    public GameObject TogglePlatforms; // Plataformas
    //public Animator anim1;
    //public Animator anim2;
    public AudioSource audioSource;
    public float interactionDistance = 3f; // Distância entre o jogador e o botão
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

        // Ao carregar "E" perto do botão com a personagem, as plataformas serão ativadas
        if (Vector3.Distance(playerTransform.position, transform.position) <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TogglePlatforms.SetActive(true); // ativa as plataformas
                //anim1.SetTrigger("isPressing"); // animação de carregar no botão
                //anim2.SetTrigger("isPressing"); // animação de carregar no botão
                audioSource.Play(); // ativa o som de carregar no botão
            }
        }
    }
}
