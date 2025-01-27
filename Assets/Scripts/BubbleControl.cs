using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleControl : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceMusic; // Fuente de audio principal
    [SerializeField] private AudioSource audioSourceEffects; // Fuente para efectos de sonido
    private Rigidbody2D rb;
    [SerializeField] private float upwardForce = 5f; // Fuerza hacia arriba
    [SerializeField] private float gravityScaleDefault = 1f; // Gravedad por defecto
    [SerializeField] private float powerUpSpeedMultiplier = 1.2f; // Multiplicador de velocidad en Power-Up

    [SerializeField] private float invulnerabilityDuration = 1f; // Duración de la inmortalidad en segundos

    static public bool superBubble = false;

    [SerializeField] private GameObject GameOverMessage;

    [SerializeField] private AudioClip background; // Música de fondo
    [SerializeField] private AudioClip powerupSong; // Sonido del Power-Up
    [SerializeField] private AudioClip gameOverSong; // Sonido de Game Over
    [SerializeField] private AudioClip powerupEffect; // Efecto de sonido del Power-Up

    [SerializeField] private CircleCollider2D circleCollider; // Referencia al CircleCollider2D
    [SerializeField] private float superBubbleRadius = 0.8f; // Tamaño del collider en modo SuperBubble
    [SerializeField] private float normalRadius = 0.6f; // Tamaño normal del collider

    public bool isDead = false;
    private bool isSpacePressed = false; // Controla si el espacio está presionado
    private bool powerUpActive = false; // Rastrea si el Power-Up estuvo activo

    private bool isInvulnerable = false; // Indica si el jugador está en estado de inmortalidad

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb.gravityScale = gravityScaleDefault;

        // Asegúrate de que el CircleCollider2D esté asignado
        if (circleCollider == null)
        {
            circleCollider = GetComponent<CircleCollider2D>();
        }

        // Asegúrate de que el clip de audio está precargado y reproducido
        if (audioSourceMusic != null && background != null)
        {
            audioSourceMusic.clip = background;
            audioSourceMusic.Play();
        }
    }

    void Update()
    {
        // Captura la entrada de usuario en cada frame
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpacePressed = true; // Marca que el espacio está presionado
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isSpacePressed = false; // Marca que el espacio ya no está presionado
        }

        // Detecta si el modo Power-Up terminó
        if (powerUpActive && !superBubble)
        {
            ReduceSpeedAfterPowerUp();
        }
    }

    private void FixedUpdate()
    {
        // Aplica fuerza solo si el espacio está presionado
        if (isSpacePressed)
        {
            rb.linearVelocity = Vector2.zero; // Detén cualquier velocidad previa
            rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
        }

        // Asegúrate de que la gravedad solo actúa cuando el espacio no está presionado
        rb.gravityScale = isSpacePressed ? 0 : gravityScaleDefault;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            if (isInvulnerable)
            {
                Debug.Log("Jugador es inmortal, bloque ignorado.");
                return; // No hacer nada si es inmortal
            }

            if (superBubble == false)
            {
                GameOverMessage.SetActive(true);
                audioSourceMusic.Stop();

                isDead = true;
                anim.SetBool("Die", true);
            }
            else
            {
                anim.SetBool("PowerUp", false);
                superBubble = false;
                audioSourceMusic.clip = background;
                audioSourceMusic.Play();
                ResetColliderSize(); // Restaura el tamaño del collider

                StartCoroutine(ActivateInvulnerability()); // Activa la inmortalidad
            }
        }
        if (other.gameObject.CompareTag("PowerUpWater"))
        {
            Destroy(other.gameObject);
            ActivateSuperBubble();

            // Reproducir el efecto del Power-Up sin cambiar la música de fondo
            if (audioSourceEffects != null && powerupEffect != null)
            {
                audioSourceEffects.PlayOneShot(powerupEffect);
            }

            // Cambiar la música principal a la canción del Power-Up
            if (powerupSong != null)
            {
                audioSourceMusic.clip = powerupSong;
                audioSourceMusic.Play();
            }

            // Incrementar la velocidad durante el Power-Up
            TimerController.time *= powerUpSpeedMultiplier;
            powerUpActive = true; // Marca que el Power-Up está activo
            Debug.Log($"Power-Up activado. Velocidad aumentada a {TimerController.time}");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            if (isInvulnerable)
            {
                Debug.Log("Jugador es inmortal, bloque ignorado.");
                return; // No hacer nada si es inmortal
            }

            if (superBubble == false)
            {
                GameOverMessage.SetActive(true);
                audioSourceMusic.Stop();

                isDead = true;
                anim.SetBool("Die", true);
            }
            else
            {
                anim.SetBool("PowerUp", false);
                superBubble = false;
                audioSourceMusic.clip = background;
                audioSourceMusic.Play();
                ResetColliderSize(); // Restaura el tamaño del collider

                StartCoroutine(ActivateInvulnerability()); // Activa la inmortalidad
            }
        }
        else if (collision.gameObject.CompareTag("PowerUpWater"))
        {
            Destroy(collision.gameObject);
            ActivateSuperBubble();

            // Reproducir el efecto del Power-Up sin cambiar la música de fondo
            if (audioSourceEffects != null && powerupEffect != null)
            {
                audioSourceEffects.PlayOneShot(powerupEffect);
            }

            // Cambiar la música principal a la canción del Power-Up
            if (powerupSong != null)
            {
                audioSourceMusic.clip = powerupSong;
                audioSourceMusic.Play();
            }

            // Incrementar la velocidad durante el Power-Up
            TimerController.time *= powerUpSpeedMultiplier;
            powerUpActive = true; // Marca que el Power-Up está activo
            Debug.Log($"Power-Up activado. Velocidad aumentada a {TimerController.time}");
        }
    }


    private IEnumerator ActivateInvulnerability()
    {
        isInvulnerable = true; // Activa la inmortalidad
        Debug.Log("Inmortalidad activada.");

        yield return new WaitForSeconds(invulnerabilityDuration); // Espera el tiempo de inmortalidad

        isInvulnerable = false; // Desactiva la inmortalidad
        Debug.Log("Inmortalidad desactivada.");
    }

    private void ActivateSuperBubble()
    {
        superBubble = true;
        anim.SetBool("PowerUp", true);

        // Cambia el tamaño del collider
        circleCollider.radius = superBubbleRadius;
        Debug.Log("SuperBubble activado. Radio del collider aumentado.");
    }

    private void ResetColliderSize()
    {
        superBubble = false;

        // Restaura el tamaño del collider
        circleCollider.radius = normalRadius;
        Debug.Log("SuperBubble desactivado. Radio del collider restaurado.");
    }

    private void ReduceSpeedAfterPowerUp()
    {
        // Divide la velocidad por el multiplicador para restablecerla
        TimerController.time /= powerUpSpeedMultiplier;
        powerUpActive = false; // Marca que el Power-Up ya no está activo
        Debug.Log($"Power-Up terminado. Velocidad reducida a {TimerController.time}");
    }
}
