using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleFlappy : MonoBehaviour
{
    public float vitesseX;
    public float vitesseY;
    public float deplacementAleatoire;

    public Sprite flappyBlesse;
    public Sprite flappyGuerit;

    public GameObject pieceOr;
    public GameObject packVie;
    public GameObject champignon;

    // Update is called once per frame
    void Update()
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
        }
        else
        {
            vitesseY = GetComponent<Rigidbody2D>().velocity.y;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);
    }

    //Détection de collisions pour Flappy
    void OnCollisionEnter2D(Collision2D Collision)
    {
        //Collision avec une colonne
        if (Collision.gameObject.name == "Colonne")
        {
            GetComponent<SpriteRenderer>().sprite = flappyBlesse;
        }
        //Collision avec la pièce d'or
        else if (Collision.gameObject.name == "PieceOr")
        {
            Collision.gameObject.SetActive(false);

            Invoke("ReactiverPieceOr", 9f);

            float valeurAleatoireY = Random.Range(-deplacementAleatoire, deplacementAleatoire);
        }
        //Collision avec le pack de vie
        else if (Collision.gameObject.name == "PackVie")
        {
            Collision.gameObject.SetActive(false);

            Invoke("ReactiverPackVie", 9f);

            GetComponent<SpriteRenderer>().sprite = flappyGuerit;
        }
        //Collision avec le champignon
        else if (Collision.gameObject.name == "Champignon")
        {
            Collision.gameObject.SetActive(false);

            Invoke("ReactiverChampignon", 9f);

            transform.localScale *= 1.5f;

            Invoke("DiminuerTailleFlappy", 5f);
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
}
