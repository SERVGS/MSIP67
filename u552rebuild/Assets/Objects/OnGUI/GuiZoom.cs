using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiZoom : MonoBehaviour {
    public float normalMoveSpeed = 5000;
    public float speed = 10;
    public Transform target;
    //private Vector3 INITpos;
    // Use this for initialization
	//public bool POSinit;
	//private Vector3 INITpos;

    void Start ()
    {
        //INITpos = transform.position;
    }
	
	// Update is called once per frame
	void LateUpdate () {



        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        { //контроль зума
            var pos = transform.position;
            //	pos.x =  Mathf.Clamp(transform.position.x, -2.0f, 2.0f);
            //	pos.y =  Mathf.Clamp(transform.position.x, -2.0f, 2.0f);
            //print (pos.y);
            pos -= transform.right * normalMoveSpeed * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
            transform.position = pos;
            //MSSIL.SetActive (false);
        }
      

      // else if (Input.GetKeyDown(KeyCode.Mouse0))
       //{

         //  float step = speed * Time.deltaTime;
           // transform.position = Vector3.MoveTowards(transform.position, target.position, 1000*step);
            //CameraGP.BroadcastMessage("Raycatch", true);

        //}
        else
        {
            float step = speed * Time.deltaTime;
           transform.position = Vector3.MoveTowards(transform.position, target.position, 100 *step);
            

        }

    }
}
