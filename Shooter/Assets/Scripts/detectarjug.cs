using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectarjug : MonoBehaviour
{
    public Transform player;
    UnityEngine.AI.NavMeshAgent enemy;
    private bool range = false;
    public Transform[] waypoints;
    Vector3 targetP;
    public float limitR;
    public float speed = 3;
    private float health;
    private float damage;


    // Use this for initialization

    void Start()
    {
        targetP = waypoints[0].position;
        enemy = GetComponent<UnityEngine.AI.NavMeshAgent>();

    }

    // Update is called once per frame
    private void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetP, Time.deltaTime * speed);

        
    }
    void changet()
    {
        int randomI = Random.Range(0, waypoints.Length);
        targetP = waypoints[randomI].position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            range = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            range = false;
        }
    }
    void Update()
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
            changet();
        }
    }
}
