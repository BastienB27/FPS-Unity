using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{

    public float degat = 10f;
    public float portée = 100f;

    public Camera fpsCam;
    public ParticleSystem Départ_du_coup;

    void Update()
    {

        if(Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }   
    }

    void Shoot()
    {
        Départ_du_coup.Play(); // Effet du tir

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, portée)){ // Si le Raycast touche un truc dans une portée de "portée" sort les info dans "hit"
            Debug.Log(hit.transform.name); // Donne le blaze du truc touché

            Cible1 cible = hit.transform.GetComponent<Cible1>(); // Cible1 est le nom du script affecté au cube "Cible1" in game
                                                                 // GetComponent prends les info tu truc touché
            if (cible != null){ // Si le truc touché à un script 
                cible.Touché(degat); // degat ta capterrrrrrrrr
            } 
        }
         
    }
}