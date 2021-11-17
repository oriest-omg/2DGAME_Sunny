using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;// Pour utiliser le module UI
using UnityEngine.SceneManagement;//Pour changer de scène


public class CharacterCollision2D : MonoBehaviour
{
    int nbGems;
    public Text txtNbGems;
    public Text txtNbVie;
    public  Text gem;
    // public GameObject maison;
    int vie = 3;
    AudioSource audioSource;
    public AudioClip colSfx;

    public GameObject fondu;
    Animator fonduAnimator;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        fonduAnimator = fondu.GetComponent<Animator>();
    }
    //Pour détecter les collions avec un trigger
    private void OnTriggerEnter2D(Collider2D col) 
    {  // colision = trigger touché
    audioSource.PlayOneShot(colSfx);
        if(col.gameObject.tag == "gems")
        {
            nbGems++;
            txtNbGems.text = nbGems.ToString();
            PlayerPrefs.SetString("gem",gem.text);
            GameObject.Find("DontDestroyOnLoad").GetComponent<DontDestroyOnLoad>().gem = gem;
            Destroy(col.gameObject);

            if(nbGems >= 5) // todo remettre 5
            {
                
                Destroy(GameObject.Find("house").GetComponent<Collider2D>());
            }
        }
        if(col.gameObject.tag == "vie")
        {
            vie++;
            txtNbVie.text = vie.ToString();
            Destroy(col.gameObject);
        }
        if(col.gameObject.tag == "hurt")
        {
            vie--;
            txtNbVie.text = vie.ToString();
            SeFaireMal();
            if(vie <= 0)
            {
                ReloadLevel();
            }
        }
        if(col.gameObject.tag == "fin")// Si on touche la fin
        {
            transform.position = new Vector3(-0.762000024f,0.469999999f,0);
            SceneManager.LoadScene("Jeu 2");
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "mob") // si on saute sur un mob
        {
            //on détruit la grenouille 
            Destroy(col.gameObject.transform.parent.gameObject);
        }
        if(col.gameObject.name == "Fall") // si on tombe
        {
            ReloadLevel();
        }
    }
    public void RetourMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
        Destroy(GameObject.Find("DontDestroyOnLoad"));

    }

    public void ReloadLevel()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SeFaireMal()
    {
        for(int i=0;i<3;i++)
        GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(RedevenirBlanc());
    }

    IEnumerator RedevenirBlanc()
    {
        yield return new WaitForSeconds(1);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
