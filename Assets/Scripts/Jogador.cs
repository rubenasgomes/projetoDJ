using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Jogador : MonoBehaviour
{
    public FallingBurgers fallingBurgers; // Script "FallingBurgers"
    public Temporizador temporizador; // Script "Temporizador"
    private Rigidbody rb;
    public Animator anim;
    private AudioSource AudioSourceObjects;
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
    public GameObject PlatformShield; // "Escudo" da plataforma
    private bool canToggle = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AudioSourceObjects = GetComponent<AudioSource>();
        atualizarVidas();
        GameOver.SetActive(false);
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
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
        }
    }
    // Colisões
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hamburger"))
        {
            Destroy(collision.gameObject);
            fallingBurgers.CollectHamburger();
            AudioSourceObjects.Play();
        }
        else if (collision.gameObject.CompareTag("Trash"))
        {
            anim.SetTrigger("hit");
            Destroy(collision.gameObject);
            perderVidas();
        }
        else if (collision.gameObject.CompareTag("Batides"))
        {
            Destroy(collision.gameObject);
            ganharVidas();
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
}
