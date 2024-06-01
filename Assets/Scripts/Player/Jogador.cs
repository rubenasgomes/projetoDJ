using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Jogador : MonoBehaviour
{
    public FallingBurgers fallingBurgers; // Script "FallingBurgers"
    private Rigidbody rb;
    public Animator anim;
    private AudioSource audioSource;
    [SerializeField] AudioClip jumpSound; // AudioSource para o som do salto
    [SerializeField] AudioClip burgerSound; // AudioSource para o som do burger
    [SerializeField] AudioClip hurtSound; // AudioSource para o som do hurt
    [SerializeField] AudioClip lixoSound; // AudioSource para o som para apanhar lixo
    [SerializeField] AudioClip ganharvidaSound; // AudioSource ao ganhar vida
    private int lives = 3;
    public float movementSpeed = 20f;
    public float runSpeedMultiplier = 1.5f;
    public float jumpForce = 20f;
    private bool isRunning = false;
    private bool isGrounded = true;
    public Image[] lifeImages;
    public GameObject GameOver;
    public GameObject HUD;
    private bool canRotate = true;
    public Objective objectiveManager;

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
            anim.SetTrigger("cair");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            anim.SetBool("salto", true);
            //PlayJumpSound(); // Reproduz o som do salto
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
    // funcão para o som do salto
    private void PlayJumpSound()
    {
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

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