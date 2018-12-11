using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour,Idamage {

    public bool jugdetected = false;
    public float bulletinst;
    public bool isdisparando = false;
    public GameObject bala;
    public float distanciadedisp;
    public float distanciadealeja;
    public Transform player;
    protected UnityEngine.AI.NavMeshAgent agent;
    [SerializeField]
    protected Slider vida;
    [SerializeField]
    protected int MaxHealth;
    [SerializeField]
    protected Transform[] waypoints;
    protected int waypointscount = 0;
    [SerializeField]
    protected estadoenemy actualestado = new estadoenemy();

    public int Damage;

    public int health
    {
        get; set;
    }

    public void UpdateHealth(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        vida.value = (float)health / MaxHealth;
    }


    public enum estadoenemy
    {
        idle, atacando, dectectojug

    }



    private void Awake()
    {
        actualestado = estadoenemy.idle;
        agent = GetComponent<NavMeshAgent>();
        health = MaxHealth;
    }

    // Use this for initialization


    // Update is called once per frame
    public virtual void move()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            changewaypoint();
        }
    }

    protected void changewaypoint()
    {
        agent.destination = waypoints[waypointscount].position;
        if ((waypointscount + 1) < waypoints.Length)
        {
            waypointscount++;
        }
        else
        {
            waypointscount = 0;
        }

    }

    protected void atacar()
    {
        float dist = Vector3.Distance(player.position, transform.position);

      
        if (dist < distanciadedisp)
        {

            agent.destination = transform.position;

            if (!isdisparando)
            {
                isdisparando = true;
                StartCoroutine(disparando());
            }
        }
        else
        {
            agent.destination = player.position;
        }

    }

    protected IEnumerator disparando()
    {

        while (isdisparando)
        {
            GameObject bullet = Instantiate(bala, gameObject.transform.position, transform.rotation);
            bullet.GetComponent<bullet>().damage = Damage;
            yield return new WaitForSeconds(bulletinst);
        }
    }




    protected void detectojugfuc()
    {
        agent.destination = player.position;

    }

    protected void detector()
    {
        
        float dis = Vector3.Distance(transform.position, player.position);
        if (dis < (player.gameObject.GetComponentInChildren<Cameramove>().distanciadecol + distanciadealeja))
        {
            jugdetected = true;
        }
    }
    public virtual void Update()
    {
        detector();
        switch (actualestado)
        {

            case estadoenemy.idle:
               move();

         
                break;
            case estadoenemy.dectectojug:
                atacar();
                transform.LookAt(player);
                break;
        }
        if (jugdetected && actualestado == estadoenemy.idle)
        {
            actualestado = estadoenemy.dectectojug;
            detectojugfuc();

        }
        
        float disttes = Vector3.Distance(player.position, transform.position);
        if(disttes>distanciadealeja && actualestado == estadoenemy.dectectojug)
        {
            jugdetected = false;
            isdisparando = false;
            actualestado = estadoenemy.idle;

        }
    }

    void OnDrawGizmos()
    {
        //Distancia de disparo
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanciadedisp);

        //Distancia de perdido
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciadealeja);

    }
    
    
}
