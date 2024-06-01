using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSwitch : MonoBehaviour
{
    public GameObject TogglePlatforms;
    //public Animator anim;
    //private AudioSource audioSource;
    public float interactionDistance = 3f; // Distância entre o jogador e o interruptor
    private Transform playerTransform;

    void Start()
    {
        if (TogglePlatforms != null)
        {
            TogglePlatforms.SetActive(false);
        }
        playerTransform = GameObject.FindWithTag("Player").transform; // Capturar o jogador
        //audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Ao carregar "E" perto do interruptor, as plataformas
        // O jogador terá de estar perto da mesma para a ativar
        if (Vector3.Distance(playerTransform.position, transform.position) <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TogglePlatforms.SetActive(true); // as plataformas são ativadas
                                                 //anim.SetTrigger("isPressing"); // a animação de "carregar no interruptor" é exibida
                                                 //audioSource.Play();
            }
        }
    }
}
