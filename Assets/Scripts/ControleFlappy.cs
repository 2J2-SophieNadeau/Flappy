using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleFlappy : MonoBehaviour
{
    public float vitesseX;
    public float vitesseY;
    public float deplacementAleatoire;

    public Sprite flappyCollision;
    public Sprite flappyGuerit;
    public Sprite flappyCollisionHaut;
    public Sprite flappyGueritHaut;

    public GameObject pieceOr;
    public GameObject packVie;
    public GameObject champignon;
    public GameObject grilleGauche;
    public GameObject grilleDroite;

    public AudioClip sonColonne;
    public AudioClip sonPieceOr;
    public AudioClip sonPackVie;
    public AudioClip sonChampigon;
    public AudioClip sonFinPartie;

    bool flappyBlesse = false;
    public bool partieTerminee = false;

    public int compteur = 0;

    // Update is called once per frame
    void Update()
    {
        if (partieTerminee == false)
        {
            //vitesseX
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                vitesseX = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                vitesseX = -1;
            }
            else
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;
            }

            //Vitesse Y
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                vitesseY = 4;

                if (flappyBlesse == false)
                {
                    GetComponent<SpriteRenderer>().sprite = flappyGuerit;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = flappyCollision;
                }
            }
            else
            {
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;

                if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
                {
                    if (flappyBlesse == false)
                    {
                        GetComponent<SpriteRenderer>().sprite = flappyGueritHaut;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = flappyCollisionHaut;
                    }
                }
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);
        }
    }

    //Détection de collisions pour Flappy
    public void OnCollisionEnter2D(Collision2D Collision)
    {
        //Collision avec une colonne
        if (Collision.gameObject.name == "Colonne" || Collision.gameObject.name == "Decor")
        {   //Si Flappy n'est pas déjà blessé
            if (flappyBlesse == false)
            { 
                GetComponent<SpriteRenderer>().sprite = flappyCollision;
                GetComponent<AudioSource>().PlayOneShot(sonColonne);

                flappyBlesse = true; //on enregistre que Flappy est blessé

                //On ajuste le pointage
                compteur += -5;
            }
            else //Si Flappy est déjà blessé
            {
                partieTerminee = true;

                //Flappy peut tourner
                GetComponent<Rigidbody2D>().freezeRotation = false;
                GetComponent<Rigidbody2D>().angularVelocity = 45f; //vitesse de rotation

                //Désactive le collider 2D de Flappy
                GetComponent<Collider2D>().enabled = false;

                GetComponent<AudioSource>().PlayOneShot(sonFinPartie);

                //Permet de relancer la partie après le délai indiqué
                Invoke("RelancerJeu", 5f);
            }
        }
        //Collision avec la pièce d'or
        else if (Collision.gameObject.name == "PieceOr")
        {
            Collision.gameObject.SetActive(false);

            GetComponent<AudioSource>().PlayOneShot(sonPieceOr);

            Invoke("ReactiverPieceOr", 9f);

            float valeurAleatoireY = Random.Range(-deplacementAleatoire, deplacementAleatoire);

            //On ajuste le pointage
            compteur += 5;

            //On active l'animation de la grille lorsque flappy touche à la pièce
            grilleGauche.GetComponent<Animator>().enabled = true;
            grilleDroite.GetComponent<Animator>().enabled = true;

            //Animation désactivée après 4 secondes;
            Invoke("AnimationPause", 4f);
        }
        //Collision avec le pack de vie
        else if (Collision.gameObject.name == "PackVie")
        {
            Collision.gameObject.SetActive(false);

            GetComponent<AudioSource>().PlayOneShot(sonPackVie);

            Invoke("ReactiverPackVie", 9f);

            GetComponent<SpriteRenderer>().sprite = flappyGuerit;

            flappyBlesse = false;

            //On ajuste le pointage
            compteur += 5;
        }
        //Collision avec le champignon
        else if (Collision.gameObject.name == "Champignon")
        {
            Collision.gameObject.SetActive(false);

            GetComponent<AudioSource>().PlayOneShot(sonChampigon);

            Invoke("ReactiverChampignon", 9f);

            transform.localScale *= 1.5f;

            Invoke("DiminuerTailleFlappy", 5f);

            //On ajuste le pointage
            compteur += 10;
        }
    }

    //Fonctions pour réactivier les object désactivés
    void ReactiverPieceOr()
    {
        pieceOr.SetActive(true);
    }

    void ReactiverPackVie()
    {
        packVie.SetActive(true);
    }

    void ReactiverChampignon()
    {
        champignon.SetActive(true);
    }

    //Fonction pour ramener flappy à sa taille normale
    void DiminuerTailleFlappy()
    {
        transform.localScale /= 1.5f;
    }

    //Fonction pour relancer le jeu
    void RelancerJeu()
    {
        SceneManager.LoadScene("Flappy6");
    }

    //Permet de désactiver l'animation pour la grille
    void AnimationPause()
    {
        grilleGauche.GetComponent<Animator>().enabled = false;
        grilleDroite.GetComponent<Animator>().enabled = false;
    }
}