using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Cameramove : MonoBehaviour {
    Vector2 mouse;
    Vector2 smooth;
    public float speed = 2;
    public float sensivity = 5;
    public float smoothing = 2;
    public GameObject Gun;
    public bool recarga=false;
    bool play = true;
    GameObject character;
    public GameObject enemy;

    public float Decaybullet = 0.5f;
    public Text bulletsnum;
    public Text Maxbullets;
    public Slider vida;
    int escapedown = 0;
    int escapedown1 = 0;

    public GameObject PauseHud;

    protected LineRenderer lr;
    [SerializeField]
    protected GameObject Partic;
    Camera thisCamera;

    // Use this for initialization
    void Start () {
        vida.value = 100000000;
        thisCamera = GetComponent<Camera>();
        character = this.transform.parent.gameObject;
        lr = gameObject.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();

    }
	void disparo()
    {
       if (int.Parse(bulletsnum.text) > 0) {
          
        RaycastHit raybullet;
                if (Physics.Raycast(thisCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out raybullet, Mathf.Infinity))
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    lr.SetPosition(0, Gun.transform.position);
                    lr.SetPosition(1, raybullet.point);

                    Instantiate(Partic, raybullet.point, transform.rotation);

                    bulletsnum.text = ((int)(float.Parse(bulletsnum.text) - Decaybullet)).ToString();
                //pregunto si el objeto con el que coliciona es enemigo le baje vida 
                if (raybullet.collider.gameObject == enemy)
                {
                    if (vida.value > 0)
                    {
                        vida.value = vida.value - 0.1f;
                    }
                    else
                    {
                        enemy.SetActive(false);
                    }
                }

                    //    raybullet.collider.GetComponent<Idamage>().UpdateHealth(daño);

                }
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    IEnumerator recargar()
    {
        escapedown1 = 0;
        Gun.GetComponent<PlayableDirector>().Play();
        recarga = true;
        bulletsnum.text = Maxbullets.text;

        yield return new WaitForSeconds(2.0f);
        recarga = false;
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
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
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
}
