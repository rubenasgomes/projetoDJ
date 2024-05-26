using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Jogador : MonoBehaviour
{
    public FallingBurgers fallingBurgers; // Script "FallingBurgers"
    public Temporizador temporizador; // Script "Temporizador"
    private Rigidbody rb;

    public ParticleSystem speedParticles; // Reference to the particle system


    public Animator anim;


    private AudioSource audioSource; 
    [SerializeField] AudioClip  jumpSound; // AudioSource para o som do salto
    [SerializeField] AudioClip  burgerSound; // AudioSource para o som do burger
    [SerializeField] AudioClip  hurtSound; // AudioSource para o som do hurt
    [SerializeField] AudioClip  lixoSound; // AudioSource para o som para apanhar lixo
    [SerializeField] AudioClip  ganharvidaSound; // AudioSource ao ganhar vida
    [SerializeField] AudioClip  VelocidadeSound; // AudioSource ao aumentar a velocidade


    private int lives = 3;
    public float movementSpeed = 20f;
    public float runSpeedMultiplier = 1.5f;


    // public float jumpForce = 20f;
    [SerializeField] float jumpForce = 15f;


    private bool isRunning = false;

    private Sensor_Player   groundSensor;

    private bool isGrounded = false;
    // private bool isGrounded = true;
    public Image[] lifeImages;
    public GameObject GameOver;
    public GameObject HUD;
    private bool canRotate = true;
    public GameObject PlatformShield; // "Escudo" da plataforma
    private bool canToggle = true;
    [SerializeField] float gravityMultiplier = 2.5f; // Multiplicador da gravidade

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.mass = 2; 
        rb.drag = 0; // Reduz a resistência ao movimento
        rb.angularDrag = 0.10f; // Reduz a resistência à rotação

        groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Player>();
        atualizarVidas();
        GameOver.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Movimento();
    }

    IEnumerator DisableToggleForDuration(float duration)
    {
        canToggle = false;
        yield return new WaitForSeconds(duration);
        canToggle = true;
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

private void ApplyFallForce()
{
    if (!isGrounded)
    {
        rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
    }
}

void Update()
{
    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
        isGrounded = false;
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        groundSensor.Disable(0.2f);
        PlayJumpSound();
    }

    ApplyFallForce();
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
            PlayJumpSound(); // Reproduz o som do salto
        }
    }

    // Colisões
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hamburger"))
        {
            Destroy(collision.gameObject);
            fallingBurgers.CollectHamburger();
            PlayBurgerSound(); //burger sound
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
        else if (collision.gameObject.CompareTag("Velocidadess"))
        {
            Destroy(collision.gameObject);
            PlayVelocidadeSound();
            StartCoroutine(IncreaseSpeedForDuration(4f, 10f));
            ActivateSpeedParticles();
        }
    }


private IEnumerator IncreaseSpeedForDuration(float amount, float duration)
{
  movementSpeed += amount;
    if (speedParticles != null)
    {
        speedParticles.Play();
    }
    yield return new WaitForSeconds(duration);
    movementSpeed -= amount;
    if (speedParticles != null)
    {
        speedParticles.Stop();  movementSpeed += amount;
    if (speedParticles != null)
    {
        speedParticles.Play();
    }
    yield return new WaitForSeconds(duration);
    movementSpeed -= amount;
    if (speedParticles != null)
    {
        speedParticles.Stop();
    }
    }
}


    // Triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlatformShield)
        {
            fallingBurgers.ToggleShield();
        }

    }

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

// FUNCÕES DOS SONS 
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

    // funcão para o som powerup Velocidade
    private void PlayVelocidadeSound()
    {
        if (VelocidadeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(VelocidadeSound);
        }
    }

    private void ActivateSpeedParticles()
{
    if (speedParticles != null)
    {
        speedParticles.Play();
    }
}

}
