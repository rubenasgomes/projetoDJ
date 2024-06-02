using UnityEngine;
using System.Collections;
using TMPro;

public class FallingBurgers : MonoBehaviour
{
    public GameObject MenuFimNivel; // Menu 
    public float spawnDelayBurgers = 3f;
    public float spawnDelaySpaceTrash = 3f;
    public float spawnDelayBatides = 3f;
<<<<<<< Updated upstream
    public float fallingSpeedBurgers = 1f; // Velocidade dos hambúrgueres
    public float fallingSpeedSpaceTrash = 1f; // Velocidade do lixo espacial
    public float fallingSpeedBatides = 1f; // Velocidade dos "batides"
=======
    public float spawnDelayVelocidade = 3f;
    public float spawnDelayCrescer = 3f;


    public float fallingSpeedBurgers = 1f; // Velocidade dos hambúrgueres
    public float fallingSpeedSpaceTrash = 1f; // Velocidade do lixo espacial
    public float fallingSpeedBatides = 1f; // Velocidade dos "batides"
    public float fallingSpeedVelocidade = 2f; // Velocidade dos "Velocidade"
    public float fallingSpeedCrescer= 2f; // Velocidade dos "Crescer"


>>>>>>> Stashed changes
    // Prefabs dos objetos
    public GameObject burgerPrefab;
    public GameObject spaceTrashPrefab;
    public GameObject batidesPrefab;
<<<<<<< Updated upstream
=======
    public GameObject VelocidadePrefab;
    public GameObject CrescerPrefab;
>>>>>>> Stashed changes
    public GameObject spawnArea;
    private Vector3 spawnSize; // Tamanho da área de "spawn"
    public int simultaneousBurgers = 1; // Nº de hambúrgueres a "spawnar" simultâneamente
    public int simultaneousSpaceTrash = 1; // Nº de lixo espacial a "spawnar" simultâneamente
    public int simultaneousBatides = 1;  // Nº de "batides" a "spawnar" simultâneamente
<<<<<<< Updated upstream
=======
    public int simultaneousVelocidade = 1;  // Nº de "Velocidade" a "spawnar" simultâneamente
    public int simultaneousCrescer = 1;  // Nº de "Crescer" a "spawnar" simultâneamente

>>>>>>> Stashed changes
    public Objective objectiveManager;

    void Start()
    {
        // Obter tamanho da área de "spawn"
        spawnSize = spawnArea.GetComponent<Renderer>().bounds.size;
        MenuFimNivel.SetActive(false);
        // Iniciar o objetivo principal
        objectiveManager.UpdateObjectiveText();
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
    // ===== código semelhante =====
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
<<<<<<< Updated upstream
=======
    // "Spawn" de "batides"/batidos
    public IEnumerator SpawnVelocidade()
    {
        yield return new WaitForSeconds(spawnDelayVelocidade);

        while (true)
        {
            for (int i = 0; i < simultaneousVelocidade; i++)
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

                rb.velocity = Vector3.down * fallingSpeedVelocidade;
            }

            yield return new WaitForSeconds(spawnDelayVelocidade);
        }
    }
    // "Spawn" de "batides"/batidos
    public IEnumerator spawnDelayCrescer()
    {
        yield return new WaitForSeconds(spawnDelayCrescer);

        while (true)
        {
            for (int i = 0; i < simultaneousCrescer; i++)
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

                rb.velocity = Vector3.down * fallingSpeedCrescer;
            }

            yield return new WaitForSeconds(spawnDelayCrescer);
        }
    }
>>>>>>> Stashed changes

    // Acabar com o "spawn" de vez
    public void StopSpawn()
    {
        StopAllCoroutines(); // Parar o "spawn"
        //Destroy(spawnArea); // Destruir "spawner"
    }

    // Fazer com que os objetos sejam destruídos assim que o temporizador chegar ao 0
    public void DisappearObjects()
    {
        GameObject[] burgers = GameObject.FindGameObjectsWithTag("Hamburger");
        GameObject[] trash = GameObject.FindGameObjectsWithTag("Trash");
        GameObject[] batides = GameObject.FindGameObjectsWithTag("Batides");
<<<<<<< Updated upstream
=======
        GameObject[] velocidade = GameObject.FindGameObjectsWithTag("Velocidade");
        GameObject[] crescer = GameObject.FindGameObjectsWithTag("Crescer");
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
    }

    // Método para mudar o rating do "spawn"
    public void ChangeSpawnRates(float newBurgerSpawnDelay, float newSpaceTrashSpawnDelay, float newBatidesSpawnDelay)
=======
        foreach (GameObject velocida in velocidade)
        {
            Destroy(velocida);
        }
        foreach (GameObject cresce in crescer)
        {
            Destroy(cresce);
        }
    }

    // Método para mudar o rating do "spawn"
    public void ChangeSpawnRates(float newBurgerSpawnDelay, float newSpaceTrashSpawnDelay, float newBatidesSpawnDelay, float newVelocidadeSpawnDelay, float newCrescerSpawnDelay)
>>>>>>> Stashed changes
    {
        spawnDelayBurgers = newBurgerSpawnDelay;
        spawnDelaySpaceTrash = newSpaceTrashSpawnDelay;
        spawnDelayBatides = newBatidesSpawnDelay;
<<<<<<< Updated upstream
    }

    // Método para mudar os "spawns" simultâneos
    public void ChangeSimultaneousSpawns(int newSimultaneousBurgers, int newSimultaneousSpaceTrash, int newSimultaneousBatides)
=======
        spawnDelayVelocidade = newVelocidadeSpawnDelay;
        spawnDelayCrescer = newCrescerSpawnDelay;
    }

    // Método para mudar os "spawns" simultâneos
    public void ChangeSimultaneousSpawns(int newSimultaneousBurgers, int newSimultaneousSpaceTrash, int newSimultaneousBatides, int newSimultaneousVelocidade, int newSimultaneousCrescer)
>>>>>>> Stashed changes
    {
        simultaneousBurgers = newSimultaneousBurgers;
        simultaneousSpaceTrash = newSimultaneousSpaceTrash;
        simultaneousBatides = newSimultaneousBatides;
<<<<<<< Updated upstream
=======
        simultaneousVelocidade = newSimultaneousVelocidade;
        simultaneousCrescer = newSimultaneousCrescer;
>>>>>>> Stashed changes
    }
}