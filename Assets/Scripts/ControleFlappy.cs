using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleFlappy : MonoBehaviour
{
    public float vitesseX;
    public float vitesseY;

    // Update is called once per frame
    void Update()
    {
        //vitesseX
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            vitesseX = 2;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            vitesseX = -2;
        }
        else
        {
            vitesseX = GetComponent<Rigidbody2D>().velocity.x;
        }

        //Vitesse Y
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            vitesseY = 6;
        }
        else
        {
            vitesseY = GetComponent<Rigidbody2D>().velocity.y;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);
    }
}
