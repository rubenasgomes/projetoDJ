using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform player; // Referência ao jogador/personagem

    void Update()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y; // Deixar a câmera com a sua altura inicial
        transform.position = newPosition;
    }
}
