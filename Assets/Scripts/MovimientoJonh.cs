using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovimientoJonh : MonoBehaviour
{
    public float JumpForce; // Fuerza del salto
    public float Speed;     // Velocidad de movimiento
    public GameObject BalaPrefab; // Prefab de bala
    public LayerMask sueloLayer; // Capa que define al suelo
    public Transform detectorSuelo; // Para detectar si esta tocando suelo
    public float radioDeteccion; // Radio para detectar si esta tocando el suelo
    public TMP_Text textoVida; // Referencia al TextMeshPro para mostrar la vida

    public AudioClip disparoSound; // Clip de sonido para el disparo
    private AudioSource audioSource; // Referencia al AudioSource

    private Rigidbody2D rigidbody2; // Rigidbody del jugador
    private Animator animator; // Animacion del jugador
    private float Horizontal; // Movimiento horizontal del jugador
    private bool Grounded; // Suelo
    public int MaxVida = 5; // Vida máxima del jugador
    private int Vida; // Vida actual del jugador

    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 

        Vida = MaxVida; 

        ActualizarTextoVida();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        animator.SetBool("estaCorriendo", Horizontal != 0.0f);

        Grounded = Physics2D.OverlapCircle(detectorSuelo.position, radioDeteccion, sueloLayer);

        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Disparo();
        }
    }

    private void Jump()
    {
        rigidbody2.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void Disparo()
    {
        Vector3 direccion;
        if (transform.localScale.x == 1.0f) direccion = Vector3.right;
        else direccion = Vector3.left;
        
        GameObject Bala = Instantiate(BalaPrefab, transform.position + direccion * 0.1f, Quaternion.identity);
        Bala.GetComponent<Bala>().SetDirection(direccion);

        
        if (disparoSound != null)
        {
            audioSource.PlayOneShot(disparoSound);
        }
    }
    
    private void FixedUpdate()
    {
        rigidbody2.velocity = new Vector2(Horizontal * Speed, rigidbody2.velocity.y);
    }

    public void Hit()
    {
        Vida = Vida - 1;

        ActualizarTextoVida();

        if (Vida == 0)
            Morir();
    }

    private void Morir()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.PerderJuego();
        }
        Destroy(gameObject); 
    }

    private void ActualizarTextoVida()
    {
        if (textoVida != null)
        {
            textoVida.text = Vida.ToString(); 
        }
    }
}
