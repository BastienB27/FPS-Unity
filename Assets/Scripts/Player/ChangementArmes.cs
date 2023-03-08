using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangementArmes : MonoBehaviour
{

    public int armeSélectionnée = 0;
    // Start is called before the first frame update
    void Start()
    {
        ArmeSélectionnée();
    }

    void OnGUI()
    {
        Event currentEvent = Event.current;
        if (currentEvent.type == EventType.KeyDown){
            //Debug.Log("KeyCode : " + currentEvent.keyCode);
        }
    }

    void Update()
    {

        int ArmePrécédente = armeSélectionnée;

        if (Input.GetKeyDown(KeyCode.Ampersand)){ // Touche 1
            armeSélectionnée = 0;
        }
       if (Input.GetKeyDown((KeyCode)160) && transform.childCount >= 2){ // Touche 2
            armeSélectionnée = 1;
        }
        if (Input.GetKeyDown(KeyCode.G) && transform.childCount >= 3){ // Touche 3
            armeSélectionnée = 2;
        }

        if (ArmePrécédente != armeSélectionnée){
            ArmeSélectionnée();
        }
        
        
    }

    public void ArmeSélectionnée(){
        int i = 0;
        foreach (Transform weapon in transform){
            if (i == armeSélectionnée){
                weapon.gameObject.SetActive(true);
            } else {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
        
    }
}
