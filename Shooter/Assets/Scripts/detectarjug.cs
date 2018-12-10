using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectarjug : MonoBehaviour
{
    public  Transform player;
    UnityEngine.AI.NavMeshAgent enemy;
    public bool range = false;
    public  Transform[] waypoints;
     public  Vector3 targetP;
    public   float limitR;
    public float speed = 0.5f;
    private   float health= 10.0f;
    private  float damage;
   

    // Use this for initialization

    public virtual void Start()
    {
        targetP = waypoints[0].position;
        enemy = GetComponent<UnityEngine.AI.NavMeshAgent>();

    }

    // Update is called once per frame
   public virtual void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetP, Time.deltaTime * speed);

        
    }
   public virtual void changet()
    {
        int randomI = Random.Range(0, waypoints.Length);
        targetP = waypoints[randomI].position;
    }
    private  void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            range = true;
        }
        else if (other.tag == "Particula")
        {
            if (health > 0)
            {
                health -= 1;
            }
            else
           {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            range = false;
        }
    }
   public virtual void Update()
    {
     //   if (Vector3.Distance(transform.position, targetP) < limitR)
      //  {
           
        //}
       move();
        
        if (range)
        {
            enemy.destination = player.position;
        }
        if (!range)
        {

            enemy.destination = this.transform.position;
        }
       
        
    }
}
S