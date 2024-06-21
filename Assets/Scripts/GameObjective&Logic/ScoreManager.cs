using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class ScoreManager : MonoBehaviour
{
    private static int score;
    private static TMP_Text textoPontuacao;
    void Start()
    {
        textoPontuacao = GetComponent<TMP_Text>();
        score = PlayerPrefs.GetInt("score");
        if (score < 0) { score = 0; }
        textoPontuacao.text = "" + score; // falta melhorar o estilo
    }
    public static void AddPoints(int points)
    {
        score += points;
        PlayerPrefs.SetInt("score", score);
        textoPontuacao.text = "" + score; // falta melhorar o estilo
    }
    public static int GetPoints()
    {
        return score;
    }
    public static void Reset()
    {
        score = 0;
        PlayerPrefs.SetInt("score", score);
        textoPontuacao.text = "" + score; // falta melhorar o estilo
    }
}