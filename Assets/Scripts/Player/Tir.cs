
using UnityEngine;

public class Tir : MonoBehaviour
{

    public float degat = 10f;
    public float portée = 100f;
    public float cadence = 15f;

    public Camera fpsCam;
    public ParticleSystem Départ_du_coup;
    public GameObject impacte;

    private float cadence_tir = 0f;

    void Update()
    {

        if(Input.GetKey(KeyCode.Mouse0) && Time.time >= cadence_tir)
        {
            cadence_tir = Time.time + 1f/cadence;
            Shoot();
        }   
    }

    void Shoot()
    {
        Départ_du_coup.Play(); // Effet du tir

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, portée)){ // Si le Raycast touche un truc dans une portée de "portée" sort les info dans "hit"
            //Debug.Log(hit.transform.name); // Donne le blaze du truc touché

            Cible1 cible = hit.transform.GetComponent<Cible1>(); // Cible1 est le nom du script affecté au cube "Cible1" in game
                                                                 // GetComponent prends les info tu truc touché
            if (cible != null){ // Si le truc touché à un script 
                cible.Touché(degat); // degat ta capterrrrrrrrr
            } 

            GameObject impactGO = Instantiate(impacte, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.1f);
        }
         
    }
}