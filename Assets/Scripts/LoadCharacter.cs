using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;
    public string characterTag = "Player"; // Add a public string for the tag
    // public TMP_Text label;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        GameObject prefab = characterPrefabs[selectedCharacter];
        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        // Set the tag of the instantiated character
        clone.tag = characterTag;

        // Get the camera script and set the player to the instantiated character
        SeguirJogador cameraScript = Camera.main.GetComponent<SeguirJogador>();
        if (cameraScript != null)
        {
            cameraScript.player = clone.transform;
        }

        // label.text = prefab.name;
    }
}
