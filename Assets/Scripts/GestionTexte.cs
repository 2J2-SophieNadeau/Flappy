using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GestionTexte : MonoBehaviour
{
    public TextMeshProUGUI pointage;
    public TextMeshProUGUI finJeu;
    Color transparence;

    public ControleFlappy controleFlappy;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        pointage.text = "Pointage: " + controleFlappy.compteur;

        if (controleFlappy.partieTerminee == true)
        {
            pointage.enabled = false;
            finJeu.enabled = true;
            transparence = finJeu.color;
            transparence.a += 0.001f;
            finJeu.color = transparence;

            if (finJeu.fontSize < 185f)
            {
                finJeu.fontSize += 0.2f;
            }
        }
    }
}
