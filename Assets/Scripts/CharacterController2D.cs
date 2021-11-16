using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float maxSpeed = 2; // Vitesse max du perso
    public float jumpForce = 10; // force de saut
    public Transform groundCheck; //vérif si on touche le sol
    public LayerMask groundMask;// qu'est ce que le sol ?
    bool isGrounded = false; // Est-on au sol ?
    // TODO
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    public GameObject pauseMenu;
    bool isPaused = false;
    AudioSource audioSource;
    public AudioClip jumpSfx;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Lorsqu'on travaille avec de la physique , il faut utiliser FIxedUpdate
    private void FixedUpdate() {
        //Dir horizontale
        float hor = Input.GetAxis("Horizontal"); // -1 ou 1 (gauche ou droite)
        anim.SetFloat("Speed",Mathf.Abs(hor));// on indique la vitesse
        rb.velocity = new Vector2(hor*maxSpeed,rb.velocity.y); // on se déplace
        Flip(hor); // on regarde dans la bonne direction
        //test si on est au sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,0.15f,groundMask);
        anim.SetBool("Grounded",isGrounded);// on indique si on saute
        if(Input.GetButton("Jump") && isGrounded) // Si touche espace & on touche le sol
        {
            rb.AddForce(Vector3.up * jumpForce);
            if(!audioSource.isPlaying)
            audioSource.PlayOneShot(jumpSfx);
        }

        //déclencher la pause
        if(Input.GetKey(KeyCode.Escape))
        {
            if(!isPaused)
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    public void ResumeGame()
    {
                isPaused = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
    }

    void Flip( float h)
    {
        if(h < 0)
        sr.flipX = true;
        if(h > 0)
        sr.flipX = false;
    }
}
