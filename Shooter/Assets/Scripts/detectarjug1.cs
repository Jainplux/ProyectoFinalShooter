using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectarjug1 : detectarjug {


    // Use this for initialization
    void OnDrawGizmos()
    {
        //rango de vision 2
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius1);
        //Distancia de disparo
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanciadedisp);

    }

}
