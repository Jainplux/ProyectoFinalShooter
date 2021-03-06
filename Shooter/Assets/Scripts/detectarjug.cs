﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class detectarjug : MonoBehaviour, Idamage
{
    public float bulletinst;
    public bool isdisparando = false;
    public float visionRadius1;
    public GameObject bala;
    public float distanciadedisp;
    public Transform player;
    protected UnityEngine.AI.NavMeshAgent agent;
    [SerializeField]
    protected Slider vida;
    [SerializeField]
    protected int MaxHealth;
    [SerializeField]
    protected  Transform[] waypoints;
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
        vida.value = (float) health / MaxHealth;
    }


    public enum estadoenemy
    {
        idle,atacando,dectectojug

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
        if ((waypointscount + 1)<waypoints.Length)
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
        if (dist < distanciadedisp && !isdisparando)
        {
            isdisparando = true;
            StartCoroutine(disparando());
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
   public virtual void Update()
    {

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
        float dist = Vector3.Distance(player.position, transform.position);
        // float dist2 = Vector3.Distance(player.position, transform.position);
        //si la distancia de vision es menos al radio amariilo se acerca
        if (dist < visionRadius1)
        {
            actualestado = estadoenemy.dectectojug;
            detectojugfuc();

        }
        else
        {
            isdisparando = false;
            actualestado = estadoenemy.idle;
           
        }
    }

   

}
