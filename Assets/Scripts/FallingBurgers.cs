using UnityEngine;
using System.Collections;
using TMPro;

public class FallingBurgers : MonoBehaviour
{
    public GameObject MenuFimNivel;
    //private bool objectiveComplete = false;
    public float spawnDelayBurgers = 3f;
    public float spawnDelaySpaceTrash = 3f;
    public float spawnDelayBatides = 3f;
    public float fallingSpeedBurgers = 1f; // Velocidade dos hambúrgueres
    public float fallingSpeedSpaceTrash = 1f; // Velocidade do lixo espacial
    public float fallingSpeedBatides = 1f; // Velocidade dos "batides"
    // Prefabs dos objetos
    public GameObject burgerPrefab;
    public GameObject spaceTrashPrefab;
    public GameObject batidesPrefab;
    public GameObject spawnArea;
    public GameObject shield;
    public GameObject entraceSwitch;
    private Vector3 spawnSize; // Tamanho da área de "spawn"
    public int simultaneousBurgers = 1; // Nº de hambúrgueres a "spawnar" simultâneamente
    public int simultaneousSpaceTrash = 1; // Nº de lixo espacial a "spawnar" simultâneamente
    public int simultaneousBatides = 1;  // Nº de "batides" a "spawnar" simultâneamente
    public TextMeshProUGUI objectiveText;
    private int hamburgersCollected = 0; // Contador de hambúrgueres colecionados
    public int hamburgerTarget = 20; // Nº de hambúrgueres a serem apanhados

    void Start()
    {
        // Obter tamanho da área de "spawn"
        spawnSize = spawnArea.GetComponent<Renderer>().bounds.size;
        MenuFimNivel.SetActive(false);

        // Começo do "spawn"
        StartCoroutine(SpawnBurgers());
        StartCoroutine(SpawnSpaceTrash());
        StartCoroutine(SpawnBatides());
        shield.SetActive(false);
        entraceSwitch.SetActive(false);

        // Iniciar o objetivo principal
        UpdateObjectiveText();
    }
    // Objetivo Principal
    void UpdateObjectiveText()
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
    public void CollectHamburger()
    {
        hamburgersCollected++; // Método de contagem
        UpdateObjectiveText(); // Atualizar o texto do objetivo assim que apanhamos um hambúrguer
    }
    // "Toggles"
    public void ToggleEntrace()
    {
        entraceSwitch.SetActive(true);
    }
    public void ToggleShield()
    {
        shield.SetActive(true);
    }
    // "Spawn" de hambúrgueres
    public IEnumerator SpawnBurgers()
    {
        yield return new WaitForSeconds(spawnDelayBurgers); // Delay antes de "spawnar" o primeiro objeto

        while (true)
        {
            for (int i = 0; i < simultaneousBurgers; i++)
            {
                // Calcular posição aleatória do "spawn"
                float randomX = Random.Range(-spawnSize.x / 2f, spawnSize.x / 2f);
                float randomZ = Random.Range(-spawnSize.z / 2f, spawnSize.z / 2f);

                Vector3 spawnPosition = spawnArea.transform.position + new Vector3(randomX, 0f, randomZ);
                GameObject newBurger = Instantiate(burgerPrefab, spawnPosition, Quaternion.identity);

                // Rigidbody
                Rigidbody rb = newBurger.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = newBurger.AddComponent<Rigidbody>();
                }

                rb.velocity = Vector3.down * fallingSpeedBurgers;
            }

            yield return new WaitForSeconds(spawnDelayBurgers);
        }
    }
    // Tudo código semelhante =====
    // "Spawn" de lixo espacial
    public IEnumerator SpawnSpaceTrash()
    {
        yield return new WaitForSeconds(spawnDelaySpaceTrash);

        while (true)
        {
            for (int i = 0; i < simultaneousSpaceTrash; i++)
            {
                float randomX = Random.Range(-spawnSize.x / 2f, spawnSize.x / 2f);
                float randomZ = Random.Range(-spawnSize.z / 2f, spawnSize.z / 2f);

                Vector3 spawnPosition = spawnArea.transform.position + new Vector3(randomX, 0f, randomZ);
                GameObject newSpaceTrash = Instantiate(spaceTrashPrefab, spawnPosition, Quaternion.identity);

                Rigidbody rb = newSpaceTrash.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = newSpaceTrash.AddComponent<Rigidbody>();
                }

                rb.velocity = Vector3.down * fallingSpeedSpaceTrash;
            }

            yield return new WaitForSeconds(spawnDelaySpaceTrash);
        }
    }

    // "Spawn" de "batides"/batidos
    public IEnumerator SpawnBatides()
    {
        yield return new WaitForSeconds(spawnDelayBatides);

        while (true)
        {
            for (int i = 0; i < simultaneousBatides; i++)
            {
                float randomX = Random.Range(-spawnSize.x / 2f, spawnSize.x / 2f);
                float randomZ = Random.Range(-spawnSize.z / 2f, spawnSize.z / 2f);

                Vector3 spawnPosition = spawnArea.transform.position + new Vector3(randomX, 0f, randomZ);
                GameObject newBatide = Instantiate(batidesPrefab, spawnPosition, Quaternion.identity);

                Rigidbody rb = newBatide.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = newBatide.AddComponent<Rigidbody>();
                }

                rb.velocity = Vector3.down * fallingSpeedBatides;
            }

            yield return new WaitForSeconds(spawnDelayBatides);
        }
    }

    // Fim de "nível"
    public void StopSpawn()
    {
        StopAllCoroutines(); // Parar o "spawn"
        //MenuFimNivel.SetActive(true); // Ativar o menu de finalização
    }

    // Fazer com que os objetos sejam destruídos assim que o temporizador chegar ao 0
    public void DisappearObjects()
    {
        GameObject[] burgers = GameObject.FindGameObjectsWithTag("Hamburger");
        GameObject[] trash = GameObject.FindGameObjectsWithTag("Trash");
        GameObject[] batides = GameObject.FindGameObjectsWithTag("Batides");

        foreach (GameObject burger in burgers)
        {
            Destroy(burger);
        }

        foreach (GameObject tr in trash)
        {
            Destroy(tr);
        }

        foreach (GameObject batide in batides)
        {
            Destroy(batide);
        }
    }

    // Método para mudar o rating do "spawn"
    public void ChangeSpawnRates(float newBurgerSpawnDelay, float newSpaceTrashSpawnDelay, float newBatidesSpawnDelay)
    {
        spawnDelayBurgers = newBurgerSpawnDelay;
        spawnDelaySpaceTrash = newSpaceTrashSpawnDelay;
        spawnDelayBatides = newBatidesSpawnDelay;
    }

    // Método para mudar os "spawns" simultâneos
    public void ChangeSimultaneousSpawns(int newSimultaneousBurgers, int newSimultaneousSpaceTrash, int newSimultaneousBatides)
    {
        simultaneousBurgers = newSimultaneousBurgers;
        simultaneousSpaceTrash = newSimultaneousSpaceTrash;
        simultaneousBatides = newSimultaneousBatides;
    }
}