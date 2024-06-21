using UnityEngine;
using System.Collections;
using TMPro;
public class Temporizador : MonoBehaviour
{
    private FallingBurgers fallingBurgers; // Script
    public TMP_Text timerText; // Texto do temporizador
    public int initialMinutes = 1;
    public int initialSeconds = 30;
    public int currentMinutes;
    public int currentSeconds;
    public bool timerRunning = true;

    private void Start()
    {
        currentMinutes = initialMinutes;
        currentSeconds = initialSeconds;
        StartCoroutine(UpdateTimer());
        // Encontrar e aramazenar todas as referências ao script "FallingBurgers"
        fallingBurgers = FindObjectOfType<FallingBurgers>();
    }

    public IEnumerator UpdateTimer()
    {
        while (timerRunning)
        {
            // Atualizar o texto do temporizador
            timerText.text = string.Format("{0:00}:{1:00}", currentMinutes, currentSeconds);
            // Se o temporizador chegar ao fim
            if (currentSeconds == 0 && currentMinutes == 0)
            {
                // remover os objetos
                //fallingBurgers.DisappearObjects();
                // ativar o plataforma terrestre para fazer aparecer as outras aéreas
                fallingBurgers.ToggleEntrace();
                // parar o "spawn"
                fallingBurgers.StopSpawn();

                // "resetar" o temporizador
                currentMinutes = initialMinutes;
                currentSeconds = initialSeconds;

                // Sair do "loop"
                break;
            }
            else
            {
                if (currentSeconds == 0)
                {
                    currentMinutes--;
                    currentSeconds = 59;
                }
                else
                {
                    currentSeconds--;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    // Método para mudar para o tempo inicial
    public void ChangeInitialTime(int minutes, int seconds)
    {
        initialMinutes = minutes;
        initialSeconds = seconds;
        currentMinutes = initialMinutes;
        currentSeconds = initialSeconds;
    }

    // "Resetar" todos os áudios/sons
    private void ResetAllAudioSources()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            // Verificar se existe àudio no menu final
            if (audioSource.CompareTag("menuFinal"))
            {
                continue; // Ignora
            }
            // desativa
            audioSource.Stop();
            audioSource.time = 0f;
        }
    }
}
