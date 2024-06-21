using UnityEngine;
using TMPro;

public class Objective : MonoBehaviour
{
    public TMP_Text objectiveText;
    private int hamburgersCollected = 0; // Contador de hambúrgueres colecionados
    public int hamburgerTarget = 20; // Nº de hambúrgueres a serem apanhados

    // Método chamado para atualizar o texto do objetivo
    public void UpdateObjectiveText()
    {
        // Se o objetivo estiver completo, o texto fica verde e continuar a contar os hambúrgueres
        if (hamburgersCollected >= hamburgerTarget)
        {
            objectiveText.text = "Objetivo completo! " + hamburgersCollected + "/" + hamburgerTarget;
            objectiveText.color = Color.green;
        }
        else
        {
            objectiveText.text = "Apanha os hambúrgueres: " + hamburgersCollected + "/" + hamburgerTarget;
        }
    }

    // Método chamado quando um hambúrguer é coletado
    public void CollectHamburger()
    {
        hamburgersCollected++; // Método de contagem
        UpdateObjectiveText(); // Atualizar o texto do objetivo assim que apanhamos um hambúrguer
    }

    // Método para obter o número de hambúrgueres coletados
    public int GetHamburgersCollected()
    {
        return hamburgersCollected;
    }
}
