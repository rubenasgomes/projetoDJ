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
    [SerializeField] AudioClip velocidadeSound; // AudioSource ao ganhar vida
    [SerializeField] AudioClip crescerSound; // AudioSource ao ganhar vida
    public float Altura = 0f; // Altura da personagem quando apanha um "batide" de morango
    private int lives = 3; // Nº total de vidas
    public float movementSpeed = 20f; // Velocidade do movimento
    public float runSpeedMultiplier = 1.5f; // Velocidade de corrida
    public float jumpForce = 25f; // Força de salto aumentada
    private bool canDoubleJump = false;
    private bool isRunning = false; // Booleano de corrida (saber se a personagem está a correr ou não)
    private bool isGrounded = true; // Booleano de salto (saber se a personagem está a saltar ou não)
    public Image[] lifeImages; // Array de vidas
    public GameObject GameOver; // Ecrã de derrota
    public GameObject HUD;
    private bool canRotate = true; // Booleano de rotação (saber se a personagem está a rodar consoante o movimento)
    private float gravityMultiplier = 4f; // Aumenta a velocidade de queda
    private bool isPoweredUp = false; // Booleano do powerup de invencibilidade (indica se a personagem o apanhou ou não)
    private bool isSpeedBoostActive = false; // Booleano para o power-up de velocidade (indica se a personagem a apanhou ou não)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        atualizarVidas();
        GameOver.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        rb.useGravity = true;
    }

    void Update()
    {
        // Jumping logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset y velocity
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canDoubleJump = false;
            }
        }

        // desativar o shift enquanto se salta
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded)
        {
            isRunning = false;
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
            rb.velocity = new Vector3(movementDirection.x * speed, rb.velocity.y, movementDirection.z * speed);
            anim.SetBool("isMoving", true);
            anim.SetBool("isRunning", isRunning);
        }

        // Aumento de gravidade
        rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
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
        else if (collision.gameObject.CompareTag("Velocidade"))
        {
            Destroy(collision.gameObject);
            PlayVelocidadeSound();
            if (!isSpeedBoostActive) // Check if the speed boost is not active
            {
                StartCoroutine(IncreaseSpeedForDuration(2f, 10f));
            }
        }
        else if (collision.gameObject.CompareTag("Crescer"))
        {
            Destroy(collision.gameObject);
            PlayCrescerSound();
            if (!isPoweredUp) // Check if the speed boost is not active
            {
                StartCoroutine(ChangeScaleForDuration(2f, 10f)); // Change scale for 10 seconds
            }
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
    // Crescimento temporária
    private IEnumerator ChangeScaleForDuration(float scaleMultiplier, float duration)
    {
        Vector3 originalScale = transform.localScale; // Store the original scale
        Vector3 newScale = originalScale * scaleMultiplier; // Calculate the new scale based on the multiplier
        isPoweredUp = true; // Set power-up state to true
        transform.localScale = newScale; // Apply the new scale
        Debug.Log("Scale changed to " + newScale); // Debug log
        yield return new WaitForSeconds(duration); // Wait for the duration
        transform.localScale = originalScale; // Revert to the original scale
        Debug.Log("Scale reverted to " + originalScale); // Debug log
        isPoweredUp = false; // Reset power-up state
    }
    // Velocidade temporária
    private IEnumerator IncreaseSpeedForDuration(float speedMultiplier, float duration)
    {
        isSpeedBoostActive = true;
        movementSpeed *= speedMultiplier;
        yield return new WaitForSeconds(duration);
        movementSpeed /= speedMultiplier;
        isSpeedBoostActive = false;
    }
    // Vidas/Health Bar
    private void perderVidas()
    {
        if (!isPoweredUp) // Verificar se o jogador tem o powerup de invencibilidade
        {
            lives--;
            atualizarVidas();
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
        // verificar se o personagem possui o número máximo de vidas
        if (lives >= 3)
        {
            return; // sai da função se a personagem tiver as 3 vidas
        }
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
    private void PlayVelocidadeSound()
    {
        if (velocidadeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(velocidadeSound);
        }
    }
    private void PlayCrescerSound()
    {
        if (crescerSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(crescerSound);
        }
    }
}