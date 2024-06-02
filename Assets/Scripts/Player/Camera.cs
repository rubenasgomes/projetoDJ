using UnityEngine;

public class SeguirJogador : MonoBehaviour
{
    public Transform player; // Referência ao Jogador
    public Vector3 offset = new Vector3(0f, 15f, -20f); // Posição da câmara

    void LateUpdate() // LateUpdate para garantir que corre depois de todas as funções Update
    {
        if (player != null)
        {
            // Calcular a posição da câmara de acordo com a posição da personagem
            Vector3 desiredPosition = player.position + offset;
            // Atualizar a posição da câmara
            transform.position = desiredPosition;
            // Garante que a câmara estará sempre apontada para a personagem
            transform.LookAt(player.position);
        }
    }
}
