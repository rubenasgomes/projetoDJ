using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Array of character prefabs
    public KeyCode switchKey = KeyCode.Space; // Default key to switch characters

    private int currentCharacterIndex = 0; // Index of the currently selected character

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            SwitchCharacter();
        }
    }

    void SwitchCharacter()
    {
        // Deactivate the current character
        characterPrefabs[currentCharacterIndex].SetActive(false);

        // Increment the character index or loop back to the first character
        currentCharacterIndex = (currentCharacterIndex + 1) % characterPrefabs.Length;

        // Activate the new character
        characterPrefabs[currentCharacterIndex].SetActive(true);
    }

    public void LoadNextScene()
    {
        // Save the current character index to PlayerPrefs for persistence between scenes
        PlayerPrefs.SetInt("SelectedCharacterIndex", currentCharacterIndex);
        PlayerPrefs.Save();

        // Load the next scene
        SceneManager.LoadScene("NextSceneName"); // Replace "NextSceneName" with the name of your next scene
    }
}
