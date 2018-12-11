using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.AI;

public class Cameramove : MonoBehaviour, Idamage {
    //GUN
    protected int actualgun = 1;
    [SerializeField]
    protected GUNs gunsdb;
    protected bool ischange = false;
    public GameObject Gun;
    public int gundamage;
    public bool recarga = false;
    public float Decaybullet = 0.5f;
    public Text bulletsnum;
    public Text Maxbullets;
    protected LineRenderer lr;
    protected GameObject Partic;

    //Player
    Vector2 mouse;
    Vector2 smooth;
    public float speed = 2;
    public float sensivity = 5;
    public float smoothing = 2;
    bool play = true;
    [SerializeField]
    GameObject character;

    float lastpos;
    float lastposy;
    public float distanciadecol;

    float velocity;
    public Slider vida;
    int escapedown = 0;
    int escapedown1 = 0;
    int change = 0;

    [SerializeField]
    public int Maxhealth;
    public GameObject PauseHud;

    Camera thisCamera;


    public int health
    {
        get; set;
    }

    public void UpdateHealth(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            Destroy(this.transform.parent.gameObject);
        }

        vida.value = ((float)health / Maxhealth);

    }

    protected void changegun()
    {

        Gun.GetComponent<MeshRenderer>().material = gunsdb.gun[actualgun].material;
        gundamage = gunsdb.gun[actualgun].Damage;
        Maxbullets.text = gunsdb.gun[actualgun].Maxammo.ToString();
        lr = gameObject.transform.GetChild(actualgun).gameObject.GetComponent<LineRenderer>();
        Partic = gunsdb.gun[actualgun].partic;
        bulletsnum.text = gunsdb.gun[actualgun].actualammo.ToString();

    }

    // Use this for initialization
    void Start()
    {
        
        StartCoroutine(velo());
        gunsdb.gun[actualgun].actualammo = gunsdb.gun[actualgun].Maxammo;
        changegun();
        character = gameObject.transform.parent.gameObject;
        health = Maxhealth;

        thisCamera = GetComponent<Camera>();


    }
    void disparo()
    {
        if (int.Parse(bulletsnum.text) > 0) {

            RaycastHit raybullet;
            if (Physics.Raycast(thisCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out raybullet, Mathf.Infinity))
            {
                gameObject.transform.GetChild(actualgun).gameObject.SetActive(true);

                lr.SetPosition(0, Gun.transform.position);
                lr.SetPosition(1, raybullet.point);

                Instantiate(Partic, raybullet.point, transform.rotation);

                bulletsnum.text = ((int)(float.Parse(bulletsnum.text) - Decaybullet)).ToString();
                //pregunto si el objeto con el que coliciona es enemigo le baje vida 
                try {
                    raybullet.collider.gameObject.GetComponent<Idamage>().UpdateHealth(gundamage);
                }
                catch (System.Exception)
                {

                }

                //    raybullet.collider.GetComponent<Idamage>().UpdateHealth(daño);

            }
        }
        else
        {
            gameObject.transform.GetChild(actualgun).gameObject.SetActive(false);
        }

    }

    IEnumerator recargar()
    {

        escapedown1 = 0;
        Gun.GetComponent<PlayableDirector>().Play();
        recarga = true;
        if (ischange)
        {
            changegun();
        }
        else
        {
            bulletsnum.text = Maxbullets.text;
        }

        yield return new WaitForSeconds(2.0f);
        recarga = false;
        ischange = false;
    }
    IEnumerator velo()
    {
        while (true)
        {
            
          
            lastpos = transform.parent.transform.position.x;

            lastposy = transform.parent.transform.position.z;
            yield return new WaitForFixedUpdate();
            float sp = (transform.parent.transform.position.x);
            float sp2 = (transform.parent.transform.position.z);
            velocity = ((Mathf.Abs(sp - lastpos)) + (Mathf.Abs(sp2 - lastposy)))/2;
            distanciadecol = velocity * 20;
        }
    }
	// Update is called once per frame
	void Update () {

       
        

        if (Input.GetAxis("Cancel")>0 && escapedown == 0)

        {

            escapedown = 1;
         

        }
        else if (Input.GetAxis("Cancel") == 0 && escapedown == 1)

            {
            escapedown = 0;
            if (play)
            {
                play = false;
                PauseHud.SetActive(true);

            }
            else
            {
                play = true;
                PauseHud.SetActive(false);
            }

        }


       

        if (play)
        {

            if (Input.GetAxis("Change") > 0 && change == 0)

            {

                change = 1;


            }
            else if (Input.GetAxis("Change") == 0 && change == 1)

            {
                change = 0;
                if (!recarga)
                {
                    gunsdb.gun[actualgun].actualammo = int.Parse(bulletsnum.text);
                    if (actualgun == 0)
                    {
                        actualgun = 1;
                    }
                    else
                    {
                        actualgun = 0;
                    }
                    ischange = true;
                 
                    StartCoroutine(recargar());
                }
                
                


            }

            if (Input.GetAxis("Fire3") > 0 && escapedown1 == 0)

            {

                escapedown1 = 1;

            }
            else if (Input.GetAxis("Fire3") == 0 && escapedown1 == 1)

            {
                if (!recarga)
                {
                    StartCoroutine(recargar());
                }

            }



            if (Input.GetAxis("Run")>0)
            {
                speed = 8;
            }
            else
            {
                speed = 2;
            }
            

            if (Input.GetAxis("Fire1")>0)
            {
                if (!recarga)
                {
                    disparo();
                }
                else
                {
                    gameObject.transform.GetChild(actualgun).gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.transform.GetChild(actualgun).gameObject.SetActive(false);
            }
            float traff = Input.GetAxis("Horizontal") * speed;
            float trans = Input.GetAxis("Vertical") * speed;

            trans *= Time.deltaTime;
            traff *= Time.deltaTime;

           character.transform.Translate(traff, 0, trans);


            Vector2 md = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensivity * smoothing, sensivity * smoothing));
            smooth.x = Mathf.Lerp(smooth.x, md.x, 1f / smoothing);
            smooth.y = Mathf.Lerp(smooth.y, md.y, 1f / smoothing);
            mouse += smooth;

            transform.localRotation = Quaternion.AngleAxis(-mouse.y, Vector3.right);
           character.transform.localRotation = Quaternion.AngleAxis(mouse.x, character.transform.up);
        }
    }
    void OnDrawGizmos()
    {
        //Distancia de colison
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanciadecol);
        

    }
}
