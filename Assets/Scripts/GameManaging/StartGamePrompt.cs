using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class StartGamePrompt : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public float fadeSpeed = 1f; // Velocidade do efeito
    public float rapidFlashSpeed = 0.1f; // Velocidade do efeito quando pressionado a tecla (rápido)
    public AudioSource audioSource;
    private bool isRapidFlashing = false; //Booleano para ver se existe alguma ação na tecla ENTER

    void Start()
    {
        StartCoroutine(FadeText());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isRapidFlashing)
            {
                isRapidFlashing = true; // começa a "flashar" o texto rapidamente
                StartCoroutine(RapidFlashAndLoadScene());
                audioSource.Play();
            }
        }
    }
    // Texto estático
    IEnumerator FadeText()
    {
        // caso não tenha sido pressionado a tecla ENTER, ele continua em loop a executar o efeito normalmente
        while (!isRapidFlashing)
        {
            yield return StartCoroutine(FadeOut());
            yield return StartCoroutine(FadeIn());
        }
    }
    // Efeito de fade in e fade out
    IEnumerator FadeIn()
    {
        Color color = promptText.color;
        while (color.a < 1)
        {
            color.a += Time.deltaTime * fadeSpeed;
            promptText.color = color;
            yield return null;
        }
    }
    IEnumerator FadeOut()
    {
        Color color = promptText.color;
        while (color.a > 0)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            promptText.color = color;
            yield return null;
        }
    }
    // Para o texto "Pressionar ENTER para começar"
    IEnumerator RapidFlashAndLoadScene()
    {
        for (int i = 0; i < 10; i++)  // efeito executado rapidamente 10 vezes
        {
            promptText.enabled = !promptText.enabled;
            yield return new WaitForSeconds(rapidFlashSpeed);
        }

        // pausas no efeito antes de carregar no ENTER
        for (int i = 0; i < 3; i++)
        {
            promptText.enabled = false;
            yield return new WaitForSeconds(0.2f);
            promptText.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        // vai para o Menu inicial
        SceneManager.LoadScene("MainMenu");
    }
}
