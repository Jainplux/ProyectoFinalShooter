using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detecjug2 : detectarjug {
    // Use this for initialization
    public Transform Tesoro;
    public float disttesoro;

  
    protected void protegertesoro()
    {
        if (!isdisparando)
        {
            isdisparando = true;
            StartCoroutine(disparando());
        }
    }

    // Update is called once per frame
    public override void Update () {
        
        switch (actualestado)
        {
            case estadoenemy.atacando:
                protegertesoro();
                break;
            case estadoenemy.idle:
               // move();
                break;
            case estadoenemy.dectectojug:
                atacar();
                break;
        }
        float dist = Vector3.Distance(player.position, transform.position);
        float disttes = Vector3.Distance(player.position, Tesoro.position);
       // float dist2 = Vector3.Distance(player.position, transform.position);
        //si la distancia de vision es menos al radio amariilo se acerca
        if (dist < visionRadius1 && actualestado != estadoenemy.atacando)
        {
            actualestado = estadoenemy.dectectojug;
            detectojugfuc();

        }
        else if (disttes < disttesoro)
        {
            actualestado = estadoenemy.atacando;
            agent.destination = Tesoro.position;
        }else
        {
            isdisparando = false;
            actualestado = estadoenemy.idle;
            agent.destination = agent.transform.position;
        }
        
        //si la distancia de vision es menos al radio azul regresa a proteger la moneda
       
        transform.LookAt(player);

        }


    void OnDrawGizmos()
    {
        //rango de vision 2
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius1);
        //Distancia de disparo
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanciadedisp);
        //Distancia de tesoro
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Tesoro.position, disttesoro);

    }
}
