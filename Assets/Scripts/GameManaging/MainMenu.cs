using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
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
