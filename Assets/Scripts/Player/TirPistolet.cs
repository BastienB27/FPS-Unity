using System.Collections;
using UnityEngine;

public class TirPistolet : MonoBehaviour
{

    public float degatP = 8f;
    public float portéeP = 100f;
    public float cadenceP = 5f;

    public int MunitionCapacitéP = 17;
    private int MunChargeurP;
    public float RechargementP = 1f;
    private bool RechargeP = false;

    public Camera fpsCam;
    public ParticleSystem Départ_du_coup;
    public GameObject impacte;

    private float cadence_tirP = 0f; 

    public Animator animationP;

    void Start (){
        MunChargeurP = MunitionCapacitéP;
    }

    void OnEnable(){ // Appelé tout le temps pas comme Start
        RechargeP = false;
        animationP.SetBool("Rechargement", false);
        // Permet de changer d'arme pendant un rechargement, puis de le rependre en revenant dessus
    }

    void Update()
    {

        if (RechargeP){ // Empeche de recharger a chaque frame du 0 munitions
            return; // Empeche de faire les autres méthodes
        }

        if (MunChargeurP <= 0){
            StartCoroutine(RechargerP()); // Permet d'utiliser IEnumerator
            return;
        }

        if(Input.GetKey(KeyCode.Mouse0) && Time.time >= cadence_tirP)
        {
            cadence_tirP = Time.time + 1f/cadenceP;
            Shoot();
        }   
    }

    IEnumerator RechargerP () {
        RechargeP = true; 

        Debug.Log("Rechargement...");

        animationP.SetBool("Rechargement", true); // Passe l'animation de rechargement en true
        yield return new WaitForSeconds(RechargementP - .25f); // Permet d'attendre (Rechargement) secondes - 0.25 pour eviter de tirer pendant lanimation
        animationP.SetBool("Rechargement", false); // Passe l'animation de rechargement en false
        yield return new WaitForSeconds(.25f); // Permet d'attendre la fin de l'animation pour pouvoir re tirer        MunChargeurP = MunitionCapacitéP;

        MunChargeurP = MunitionCapacitéP;

        RechargeP = false;
    }

    void Shoot()
    {
        Départ_du_coup.Play(); // Effet du tir
        MunChargeurP --;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, portéeP)){ // Si le Raycast touche un truc dans une portée de "portée" sort les info dans "hit"
            //Debug.Log(hit.transform.name); // Donne le blaze du truc touché

            Cible1 cible = hit.transform.GetComponent<Cible1>(); // Cible1 est le nom du script affecté au cube "Cible1" in game
                                                                 // GetComponent prends les info tu truc touché
            if (cible != null){ // Si le truc touché à un script 
                cible.Touché(degatP); // degat ta capterrrrrrrrr
            } 

            GameObject impactGO = Instantiate(impacte, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.1f);
        }
         
    }
}