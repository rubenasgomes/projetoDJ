using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Jogador : MonoBehaviour
{
    public FallingBurgers fallingBurgers; // script
    public Objective objectiveManager; // script
    private Rigidbody rb;
    public Animator anim;
    private AudioSource audioSource;
    [SerializeField] AudioClip burgerSound; // AudioSource para o som do burger
    [SerializeField] AudioClip hurtSound; // AudioSource para o som do hurt
    [SerializeField] AudioClip lixoSound; // AudioSource para o som para apanhar lixo
    [SerializeField] AudioClip ganharvidaSound; // AudioSource ao ganhar vida
    private int lives = 3; // Nº total de vidas
    public float movementSpeed = 20f; // Velocidade do movimento
    public float runSpeedMultiplier = 1.5f; // Velocidade de corrida
    public float jumpForce = 20f; // Força de salto
    private bool isRunning = false; // Booleano de corrida (saber se a personagem está a correr ou não)
    private bool isGrounded = true; // Booleano de salto (saber se a personagem está a saltar ou não)
    public Image[] lifeImages; // Array de vidas
    public GameObject GameOver; // Ecrã de derrota
    public GameObject HUD;
    private bool canRotate = true; // Booleano de rotação (saber se a personagem está a rodar consoante o movimento)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        atualizarVidas();
        GameOver.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        Movimento();
    }

    private void Movimento()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

        if (movementDirection.magnitude == 0)
        {
            anim.SetBool("isMoving", false);
            isRunning = false;
        }
        else
        {
            isRunning = Input.GetKey(KeyCode.LeftShift);
            if (canRotate)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
            float speed = movementSpeed;
            if (isRunning)
            {
                speed *= runSpeedMultiplier;
            }
            transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
            anim.SetBool("isMoving", true);
            anim.SetBool("isRunning", isRunning);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("salto", false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            anim.SetBool("salto", true);
        }
    }

    // Colisões
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hamburger"))
        {
            Destroy(collision.gameObject);
            objectiveManager.CollectHamburger();
            PlayBurgerSound();
        }
        else if (collision.gameObject.CompareTag("Trash"))
        {
            anim.SetTrigger("hit");
            Destroy(collision.gameObject);
            perderVidas();
            PlayLixoSound();
        }
        else if (collision.gameObject.CompareTag("Batides"))
        {
            Destroy(collision.gameObject);
            ganharVidas();
            PlayGanharVidaSoundSound();
        }
        else if (collision.gameObject.CompareTag("DeathGround"))
        {
            isGrounded = true;
            anim.SetBool("salto", false);
            anim.SetTrigger("morrer");
            movementSpeed = 0;
            jumpForce = 0;
            canRotate = false;
            GameOver.SetActive(true);
            HUD.SetActive(false);
            fallingBurgers.StopSpawn();
            fallingBurgers.DisappearObjects();
            //PlayHurtSound();
        }
    }
    // Vidas/Health Bar
    private void perderVidas()
    {
        lives--;
        if (lives <= 0)
        {
            anim.SetTrigger("morrer");
            movementSpeed = 0;
            jumpForce = 0;
            canRotate = false;
            GameOver.SetActive(true);
            HUD.SetActive(false);
            fallingBurgers.StopSpawn();
            fallingBurgers.DisappearObjects();
            PlayHurtSound();
        }
        else if (lives > 0 && lives < 3)
        {
            atualizarVidas();
        }
    }

    private void atualizarVidas()
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].enabled = i < lives;
        }
    }

    private void ganharVidas()
    {
        lives++;
        atualizarVidas();
    }

    // FUNÇÕES DOS SONS 
    // funcão para o som do burger
    private void PlayBurgerSound()
    {
        if (burgerSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(burgerSound);
        }
    }
    // funcão para o som do Hurt
    private void PlayHurtSound()
    {
        if (hurtSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }
    }
    // funcão para o som ao apanhar o lixo
    private void PlayLixoSound()
    {
        if (lixoSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(lixoSound);
        }
    }
    // funcão para o som ao ganhar vida
    private void PlayGanharVidaSoundSound()
    {
        if (ganharvidaSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(ganharvidaSound);
        }
    }
}