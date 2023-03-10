using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour

{
    public AudioClip explosionSound; // ajoutez une variable pour l'AudioClip
    private AudioSource audioSourceG;

    public float retardement = 3f;
    public float rayon = 5f;
    public float forceExplosion = 1000f;
    public float degatG = 800f;


    float compteRebours;
    bool explosée = false;

    public GameObject effetExplosion;


    // Start is called before the first frame update
    void Start()
    {
        audioSourceG = GetComponent<AudioSource>();
        compteRebours = retardement;
    }

    // Update is called once per frame
    void Update()
    {
        compteRebours -= Time.deltaTime;
        if (compteRebours <= 0f && explosée == false){
            Explose();
            explosée = true;
        }
    }

    void Explose(){
        GetComponent<AudioSource>().PlayOneShot(explosionSound);
        GameObject grenadeClone = Instantiate(effetExplosion, transform.position, transform.rotation); // Effet explosion
        Collider[] collision = Physics.OverlapSphere(transform.position, rayon); // Recupere dans une liste tous les objets dans rayon

        foreach (Collider nearbyObject in collision){
            ReceiveDamage cible = nearbyObject.GetComponent<ReceiveDamage>(); // Cible1 est le nom du script affecté au cube "Cible1" in game
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>(); // Prends le parametre Rigidbody de tous les objets issus de la liste collision
            if (rb != null){
                rb.AddExplosionForce(forceExplosion, transform.position, rayon); // Applique lexplosion sur les objets
            }
                                                                 // GetComponent prends les info tu truc touché
            if (cible != null){ // Si le truc touché à un script 
                cible.GetDamage(degatG); // degat ta capterrrrrrrrr

            } 
        }
        Destroy(grenadeClone, 1f); // Enleve le clone de explosion
        Destroy(gameObject);
    }
}
