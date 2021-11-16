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
    public GameObject maison;
    int vie = 3;
    AudioSource audioSource;
    public AudioClip colSfx;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    //Pour détecter les collions avec un trigger
    private void OnTriggerEnter2D(Collider2D col) 
    {  // colision = trigger touché
    audioSource.PlayOneShot(colSfx);
        if(col.gameObject.tag == "gems")
        {
            nbGems++;
            txtNbGems.text = nbGems.ToString();
            Destroy(col.gameObject);
            if(nbGems >= 1) // todo remettre 5
            {
                Destroy(maison.GetComponent<Collider2D>());
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
