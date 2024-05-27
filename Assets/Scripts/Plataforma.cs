using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plataforma : MonoBehaviour
{
    private AudioSource AudioSourceObjects;
    public GameObject particleEffect;

    void Start()
    {
        AudioSourceObjects = GetComponent<AudioSource>();
    }
    // Colisões dos objetos na plataforma
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hamburger"))
        {
            Destroy(collision.gameObject);
            // AudioSourceObjects.Play();
            //EfeitoParticulasExplosao(collision.contacts[0].point);
        }
        else if (collision.gameObject.CompareTag("Trash"))
        {
            Destroy(collision.gameObject);
            //AudioSourceObjects.Play();
        }
        else if (collision.gameObject.CompareTag("Batides"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Velocidadess"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("CrescerM"))
        {
            Destroy(collision.gameObject);
        }
    }
    // Particulas (Quando os objectos cairem na plataforma, eles emitirão uma "animação" quando desaparecem)
    private void EfeitoParticulasExplosao(Vector3 position)
    {
        if (particleEffect != null)
        {
            Instantiate(particleEffect, position, Quaternion.identity);
        }
    }
}

