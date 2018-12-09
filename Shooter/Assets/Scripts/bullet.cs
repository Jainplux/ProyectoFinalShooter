using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
    Rigidbody rb;
    public int speed;
    detecjug2 enemy;
    public GameObject player;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        enemy.GetComponent<detecjug2>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag=="Player")
        {
            if (enemy.vida.value > 0)
            {
                Destroy(obj.gameObject);
                enemy.vida.value = enemy.vida.value - 20;
            }
            else
            {
                player.SetActive(false);
            }
        } 
    }
}
