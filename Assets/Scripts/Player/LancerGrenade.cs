using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerGrenade : MonoBehaviour
{


    public float forceLancer = 15f;
    public GameObject grenadePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.DoubleQuote)){
            LanceLaGrenade();
        }
        
    }

    void LanceLaGrenade(){
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * forceLancer, ForceMode.VelocityChange);
    }
}
