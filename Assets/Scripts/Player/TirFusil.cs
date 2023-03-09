using System.Collections;
using UnityEngine;

public class TirFusil : MonoBehaviour
{

    public float degat = 10f;
    public float portée = 100f;
    public float cadence = 15f;

    public int MunitionCapacité = 30;
    private int MunChargeur;
    public float Rechargement = 3f;
    private bool Recharge = false;

    public Camera fpsCam;
    public ParticleSystem Départ_du_coup;
    public GameObject impacte;

    private float cadence_tir = 0f;

    public Animator animationF;

    void Start (){
        MunChargeur = MunitionCapacité;
    }

    void OnGUI(){
        GUI.Label(new Rect(10, Screen.height - 80, 200, 20), MunChargeur + "/" + MunitionCapacité, new GUIStyle() { fontSize = 60 });
    }

    void OnEnable(){ // Appelé tout le temps pas comme Start
        Recharge = false;
        animationF.SetBool("Rechargement", false);
        // Permet de changer d'arme pendant un rechargement, puis de le rependre en revenant dessus
    }

    void Update()
    {

        if (Recharge){ // Empeche de recharger a chaque frame du 0 munitions
            return; // Empeche de faire les autres méthodes
        }

        if (MunChargeur <= 0 || Input.GetKey(KeyCode.R)){
            StartCoroutine(Recharger()); // Permet d'utiliser IEnumerator
            return;
        }

        if(Input.GetKey(KeyCode.Mouse0) && Time.time >= cadence_tir)
        {
            cadence_tir = Time.time + 1f/cadence;
            Shoot();
            
        }   
    }

    IEnumerator Recharger () {
        Recharge = true; 

        Debug.Log("Rechargement...");

        animationF.SetBool("Rechargement", true); // Passe l'animation de rechargement en true
        yield return new WaitForSeconds(Rechargement - .25f); // Permet d'attendre (Rechargement) secondes - 0.25 pour eviter de tirer pendant lanimation
        animationF.SetBool("Rechargement", false); // Passe l'animation de rechargement en false
        yield return new WaitForSeconds(.25f); // Permet d'attendre la fin de l'animation pour pouvoir re tirer

        MunChargeur = MunitionCapacité;

        Recharge = false;
    }
    void Shoot()
    {
        Départ_du_coup.Play(); // Effet du tir
        MunChargeur --;

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