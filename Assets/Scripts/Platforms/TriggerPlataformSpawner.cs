using UnityEngine;
using System.Collections;
using TMPro;

public class TriggerPlataformSpawner : MonoBehaviour
{
    public FallingBurgers fb; // script
    public Objective objectiveManager;
    public GameObject objectToDestroy; // destruir o "escudo" qunado o temporizador chegar ao 0
    public GameObject SwitchTogglePlatform; // botão para ativar as plataformas
    public GameObject WinScreen; // Ecrâ de vitória
    public GameObject LoseScreen; // Ecrâ de derrota
    public TMP_Text timerTextMesh; // Texto do temporizador
    public float timerDuration = 5f; // Duração do temporizador
    public bool isLastPlatform = false; // Booleano para a última plataforma
    private bool hasTriggered = false; // Booleano para o "collider" das plataformas (responsável pelo spawn dos hambúrgueres)

    void Start()
    {
        // se não houver nenhum objecto selecionado, este passo será opcional
        if (SwitchTogglePlatform != null)
        {
            SwitchTogglePlatform.SetActive(false);
        }
    }

    // ao colidir com o "escudo invisível"
    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered)
        {
            // Começar o "spawn"
            if (fb != null)
            {
                fb.StartCoroutine(fb.SpawnBurgers());
                fb.StartCoroutine(fb.SpawnBatides());
                fb.StartCoroutine(fb.SpawnSpaceTrash());
                fb.StartCoroutine(fb.SpawnVelocidade());
                fb.StartCoroutine(fb.spawnCrescer());
            }
            // Começar o temporizador
            StartCoroutine(StartTimer());
            // Ativar o "trigger" dos "escudos invisíveis"
            hasTriggered = true;
        }
    }
    // Temporizador
    private IEnumerator StartTimer()
    {
        float timer = timerDuration;

        while (timer > -0.01f)
        {
            // Update the timer text
            UpdateTimerText(timer);

            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }

        // Parar o "spawning"
        fb.StopSpawn();
        fb.DisappearObjects();
        // "Spawnar" o interruptor
        SwitchTogglePlatform.SetActive(true);

        // Destruir o "escudo"
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }

        // Verificar se a plataforma principal é a final
        if (isLastPlatform)
        {
            CheckGameOutcome();
        }

        Debug.Log("Função executada com sucesso!");
    }
    // Atualizar temporizador
    private void UpdateTimerText(float timeRemaining)
    {
        if (timerTextMesh != null)
        {
            // Converter o restante tempo em minutos e segundos
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            // Formatar o temporizador para "00:00"
            string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);

            timerTextMesh.text = timeText;
        }
        else
        {
            Debug.LogError("`Text Mesh` não adicionado através do inspector!");
        }
    }
    // Verificar as condições do jogo (perder e/ou ganhar)
    private void CheckGameOutcome()
    {
        if (objectiveManager != null)
        {
            int hamburgersCollected = objectiveManager.GetHamburgersCollected(); // obter os hambúrgueres coletados
            int hamburgerTarget = objectiveManager.hamburgerTarget; // obter o limite estipulado dos hambúrgueres

            if (hamburgersCollected >= hamburgerTarget)
            {
                Debug.Log("Bom trabalho, soldado! Total de pontos neste nível: " + hamburgersCollected);
                WinScreen.SetActive(true);
            }
            else
            {
                Debug.Log("GAME OVER! Perdestes o jogo...");
                LoseScreen.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("`Objective Manager` não adicionado através do inspector!");
        }
    }
}
