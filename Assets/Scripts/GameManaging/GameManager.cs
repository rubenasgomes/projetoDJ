using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject MenuPausa; // Referência ao menu de pausa
    private bool gameIsPaused = false; // Variável para controlar se o jogo está pausado

    void Start()
    {
        // Desativa o menu de pausa e o mini mapa no início do jogo
        MenuPausa.SetActive(false);
    }

    void Update()
    {
        // ao carregar na tecla ESC, o menu de pausa aparece
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }
    // Lógica da pausa
    void TogglePauseMenu()
    {
        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
        {
            // Pausa o jogo e exibe o menu de pausa
            Time.timeScale = 0; // Pausa o tempo do jogo
            MenuPausa.SetActive(true); // Ativa o menu de pausa 
        }
        else
        {
            // Retoma o jogo e esconde o menu de pausa
            // Aqui faz o contrário
            Time.timeScale = 1f;
            MenuPausa.SetActive(false);
        }
    }
    // Resumir jogo carregando num botão
    public void ResumeButtonClicked()
    {
        Time.timeScale = 1f;
        MenuPausa.SetActive(false);
    }
    // Sair para o menu principal
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // Recomeçar o jogo
    public void RestartGame()
    {
        // Resetar o tempo para não estar pausado
        PlayerPrefs.SetInt("score", 0);
        Time.timeScale = 1f;
        // Carregar a cena atual para recomeçar o jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
