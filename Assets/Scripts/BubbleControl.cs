using UnityEngine;

public class BubbleControl : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceMusic; // Fuente de audio principal
    [SerializeField] private AudioSource audioSourceEffects; // Fuente para efectos de sonido
    private Rigidbody2D rb;
    [SerializeField] private float upwardForce = 5f; // Fuerza hacia arriba
    [SerializeField] private float gravityScaleDefault = 1f; // Gravedad por defecto

    static public bool superBubble = false;

    [SerializeField] private GameObject GameOverMessage;

    [SerializeField] private AudioClip background; // Música de fondo
    [SerializeField] private AudioClip powerupSong; // Sonido del power-up
    [SerializeField] private AudioClip powerupEffect; // Efecto de sonido del power-up

    public bool isDead = false;
    private bool isSpacePressed = false; // Controla si el espacio está presionado

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScaleDefault;

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
            if (superBubble == false)
            {
                GameOverMessage.SetActive(true);
                audioSourceMusic.Stop();
                isDead = true;
                Destroy(gameObject);
            }
            else
            {
                superBubble = false;
                audioSourceMusic.clip = background;
                audioSourceMusic.Play();
            }
        }
        if (other.gameObject.CompareTag("PowerUpWater"))
        {
            Destroy(other.gameObject);
            ActivateSuperBubble();

            // Reproducir el efecto del power-up sin cambiar la música de fondo
            if (audioSourceEffects != null && powerupEffect != null)
            {
                audioSourceEffects.PlayOneShot(powerupEffect);
            }

            // Cambiar la música principal a la canción del power-up
            if (powerupSong != null)
            {
                audioSourceMusic.clip = powerupSong;
                audioSourceMusic.Play();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            if (superBubble == false)
            {
                GameOverMessage.SetActive(true);
                audioSourceMusic.Stop();
                isDead = true;
                Destroy(gameObject);
            }
            else
            {
                superBubble = false;
                audioSourceMusic.clip = background;
                audioSourceMusic.Play();
            }
        }
    }

    private void ActivateSuperBubble()
    {
        superBubble = true;
    }
}
