using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    // Seleção de personagens
    public void SelectPlayer()
    {
        SceneManager.LoadScene("selection");
    }
    // Cutscene/Vídeo
    public void GoToCutscene()
    {
        SceneManager.LoadScene("Cutscene");
    }
    // Níveis
    public void StartLvl1()
    {
        SceneManager.LoadScene("nivel1");
        PlayerPrefs.SetInt("score", 0);
        Time.timeScale = 1f;
    }
    public void StartLvl2()
    {
        SceneManager.LoadScene("nivel2");
        PlayerPrefs.SetInt("score", 0);
        Time.timeScale = 1f;
    }
    public void StartLvl3()
    {
        SceneManager.LoadScene("nivel3");
        PlayerPrefs.SetInt("score", 0);
        Time.timeScale = 1f;
    }
    // Loja
    public void GoToStore()
    {
        SceneManager.LoadScene("Loja");
    }
    // Opções
    public void GoToOptions()
    {
        SceneManager.LoadScene("Options");
    }
    // Controlos
    public void GoToControlls()
    {
        SceneManager.LoadScene("controlls");
    }
    // Gameplay/Tutorial
    public void GoToGameplay()
    {
        SceneManager.LoadScene("gameplay");
    }
    // Voltar para trás
    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // Sair da aplicação/jogo
    public void QuitGame()
    {
        Application.Quit();
    }
}
