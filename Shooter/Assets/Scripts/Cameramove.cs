using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramove : MonoBehaviour {
    Vector2 mouse;
    Vector2 smooth;
    public float speed = 2;
    public float sensivity = 5;
    public float smoothing = 2;
    public GameObject Gun;
    bool play = true;
    GameObject character;

    public GameObject PauseHud;

    protected LineRenderer lr;
    [SerializeField]
    protected GameObject Partic;
    Camera thisCamera;

    // Use this for initialization
    void Start () {

        thisCamera = GetComponent<Camera>();
        character = this.transform.parent.gameObject;
        lr = gameObject.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();

    }
	void disparo()
    {
     
        RaycastHit raybullet;
        if (Physics.Raycast(thisCamera.ScreenPointToRay(new Vector3(Screen.width / 2 ,Screen.height / 2,0)), out raybullet, Mathf.Infinity))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
           
          lr.SetPosition(0, Gun.transform.position);
         lr.SetPosition(1, raybullet.point);

           Instantiate(Partic, raybullet.point, transform.rotation);
            
           
          //    raybullet.collider.GetComponent<Idamage>().UpdateHealth(daño);


        }
    }
	// Update is called once per frame
	void Update () {
   

        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 8;
            }
            else
            {
                speed = 2;
            }
            

            if (Input.GetMouseButton(0))
            {
                disparo();
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
