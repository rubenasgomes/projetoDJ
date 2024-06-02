using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject MenuPausa; // Referência ao menu de pausa
    private bool gameIsPaused = false; // Variável para controlar se o jogo está pausado

    // Start is called before the first frame update
    void Start()
    {
        // Desativa o menu de pausa no início do jogo
        MenuPausa.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu(); // Alternar o menu de pausa ao pressionar Esc
        }
    }

    void TogglePauseMenu()
    {
        gameIsPaused = !gameIsPaused; // Inverte o estado de pausa do jogo

        if (gameIsPaused)
        {
            PauseGame(); // Pausa o jogo e exibe o menu de pausa
        }
        else
        {
            ResumeGame(); // Retoma o jogo e esconde o menu de pausa
        }
    }

    // Pausar o jogo
    void PauseGame()
    {
        Time.timeScale = 0; // Pausa o tempo do jogo
        MenuPausa.SetActive(true); // Ativa o menu de pausa
    }

    // Resumir o jogo
    void ResumeGame()
    {
        // Aqui faz o contrário
        Time.timeScale = 1f;
        MenuPausa.SetActive(false);
    }
    // Começar jogo
    public void StartGame()
    {
        SceneManager.LoadScene("nivel1");
        PlayerPrefs.SetInt("score", 0);
        Time.timeScale = 1f;
    }
    public void ResumeButtonClicked()
    {
        ResumeGame(); // chama a função
    }

    // Sair para o menu
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
    public void QuitGame()
    {
        Application.Quit();
    }

    public void AbrirOpcoes()
   {
    SceneManager.LoadScene("options");
   }

     public void abrirGameplay()
   {
    SceneManager.LoadScene("gameplay");
   }

     public void goBack()
    {
        // Implementar a lógica de "Voltar atrás" aqui
        // Exemplo: Carregar a cena anterior
        SceneManager.LoadScene("MainMenu");
    }
    public void abrirControlls()
    {
        // Implementar a lógica de "Voltar atrás" aqui
        // Exemplo: Carregar a cena anterior
        SceneManager.LoadScene("controlls");
    }

    public void goBackSettings()
    {
        // Implementar a lógica de "Voltar atrás" aqui
        // Exemplo: Carregar a cena anterior
        SceneManager.LoadScene("options");
    }

}
