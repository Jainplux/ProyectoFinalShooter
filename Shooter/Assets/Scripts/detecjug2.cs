using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detecjug2 : detectarjug {
    UnityEngine.AI.NavMeshAgent enemy;
    // Use this for initialization
    public float visionRadius2;
    public float visionRadius1;
    public GameObject bala;
    bool disparo=false;
    public Slider vida;//vida del jugador
    public override void  Start () {
        targetP = waypoints[0].position;
        enemy = GetComponent<UnityEngine.AI.NavMeshAgent>();
        vida.value = 100;
        InvokeRepeating("disparando", 2, 1);

    }


    // Update is called once per frame
    public override void Update () {
        if (Vector3.Distance(transform.position, targetP) < limitR)
        {
            changet();
        }
        move();
        float dist = Vector3.Distance(player.position, transform.position);
        float dist2 = Vector3.Distance(player.position, transform.position);
        //si la distancia de vision es menos al radio amariilo se acerca
        if (dist < visionRadius2)
        {
            enemy.destination = player.position;
            disparo = true;
           // Instantiate(bala, player.position, transform.rotation);
        }
        //si la distancia de vision es menos al radio azul regresa a proteger la moneda
        if (dist2< visionRadius1)
        {
            enemy.destination = this.transform.position;
        }
        transform.LookAt(player);

        }

    void disparando()
    {
        if (disparo == true)
        {
          Instantiate(bala, gameObject.transform.position, transform.rotation);
        }

    }

    void OnDrawGizmos()
    {
        //rango de vision 2
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius2);


        //rango de vision 1
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, visionRadius1);

    }
}
