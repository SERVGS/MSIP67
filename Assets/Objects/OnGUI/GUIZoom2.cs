using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIZoom2 : MonoBehaviour {
    public float normalMoveSpeed = 5000;
    public float speed = 10;
    private float INITdist;
    public bool POSinit;
    private Vector3 INITpos;
    private Vector3 cameraAngles;

    // Use this for initialization
    void Start () {
        var heading = transform.position - Camera.main.transform.position;
        INITdist = heading.magnitude;
        INITpos = transform.position;
        POSinit = false;
        INITpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        var pos = transform.position;
        var heading = pos - Camera.main.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance; // This is now the normalized direction.
        if (POSinit)
        {
            //print("posinit");
            transform.position = INITpos;
            //StartCoroutine(WaitingTime());
            POSinit = false;
        }
        else
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            { //контроль зума


                pos += transform.forward * normalMoveSpeed * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
                transform.position = pos;

                //pos.x =  Mathf.Clamp(transform.position.x, -2.0f, 2.0f);
                //	pos.y =  Mathf.Clamp(transform.position.x, -2.0f, 2.0f);
                //print(pos.y);

                //MSSIL.SetActive (false);
            }
            if (distance > INITdist + 10 )
            {

                if (Input.GetMouseButton(1))
               {
                    transform.position = Vector3.Lerp(pos, INITpos, 0.3f);
                }
                else
                {
                    transform.position = Vector3.Lerp(pos, pos - 10 * direction, 0.5f);

                }
            }

           //else if (Input.GetMouseButton(0))
            //{
            //     float step = speed * Time.deltaTime;
            //  transform.position = INITpos;
            // CameraGP.BroadcastMessage("Raycatch", true);


            //}
            else if (distance < INITdist - 10)
            {
               if (Input.GetMouseButton(1))
                {
                   transform.position = Vector3.Lerp(pos, INITpos, 0.3f);
               }
                else
                {
                    //float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(pos, INITpos, 0.5f);
                   // transform.position = Vector3.Lerp(pos, pos - 10 * direction, 1f);

                }

            }

            cameraAngles = Camera.main.transform.eulerAngles;
            var newRot = Quaternion.Euler(cameraAngles); // get the equivalent quaternion
            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 15* Time.deltaTime);
        }
    }
    //IEnumerator WaitingTime()
    //{

     //   Input.ResetInputAxes();
    //    yield return new WaitForSeconds(0.1f);
        
    //}

}

