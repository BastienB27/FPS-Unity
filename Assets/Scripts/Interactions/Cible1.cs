using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cible1 : MonoBehaviour
{

    public float vie = 40f;
    
    public void Touché (float amount){
        vie -= amount; 
        if (vie <= 0f){
            Mort();
        }
    }

    void Mort(){
        Destroy(gameObject);
    }
}
