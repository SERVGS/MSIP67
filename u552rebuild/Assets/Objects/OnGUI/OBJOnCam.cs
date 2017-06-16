using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJOnCam : MonoBehaviour {
    //Ray ray;
    //RaycastHit hit;
    //public GameObject DescOBJ;
    //var myCamera : Transform;
    //public float distance  = 5.0;
    // Use this for initialization
    // public float RotationSpeed=5f;

    //private Transform targetPosition;
    //public Vector3 posCam;
    //public Quaternion lastPos;
    //values for internal use
    //public Transform target;
    //public Object CamGP;
    //  private Quaternion _lookRotation;
    //private Quaternion _lookRotationInv;
    //private Vector3 _direction;
    //public float smooth = 0.3F;
    //public float distance = 5.0F;
    //private float yVelocity = 0.0F;
    

    private void Start()
    {
       
    }

    // Update is called once per frame
    void LateUpdate () {

        //_direction = new Vector3 (distance, distance, distance);
       // Vector3 pos = Camera.main.transform.position+ _direction;
        //cameraTransform.position = Vector3.Lerp(Camera.main.transform.position, TargetPosition, 5.0f * Time.deltaTime);
        //pos.x = Mathf.SmoothDamp( Camera.main.transform.position.x, pos.x, ref yVelocity, smooth);
        //pos.y = Mathf.SmoothDamp(Camera.main.transform.position.y, pos.y, ref yVelocity, smooth);
        //pos.z = Mathf.SmoothDamp(Camera.main.transform.position.z, pos.z, ref yVelocity, smooth);
        //transform.LookAt(pos + Camera.main.transform.rotation * Vector3.left, Camera.main.transform.rotation * Vector3.up);
      
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.left, Camera.main.transform.rotation * Vector3.up);
        


        //posCam = camCam.position;
        //lastPos = Quaternion.LookRotation(-posCam);
        //posCam.y = 0;
        // transform.rotation = Quaternion.Slerp(transform.rotation, lastPos, Time.deltaTime * RotationSpeed);
        // transform.LookAt(Camera.main.transform.position, -Vector3.up);
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Vector3 pos = GameObject.Find("EmptyTarget1").transform.position;
        //_direction = (-(Camera.main.transform.position)).normalized;
        // _direction = (-pos).normalized;
        // print(pos);
        //_lookRotation = Quaternion.LookRotation(_direction);
        //_lookRotationInv = Quaternion.LookRotation(_direction);
        //Vector3 myEulerAngles = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);


        // Vector3 rot =transform.eulerAngles;
        //  if (rot.y > 0 ) { transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotationInv, Time.deltaTime * RotationSpeed);}
        //else if (rot.x < 0) { transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed); }
        //else
        //{
        //transform.rotation = Quaternion.Lerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        //}
        /// Mathf.Clamp(rot.y, 0, 180);
        //rot.x = Mathf.Clamp(rot.x, 0, 180);
        // rot.z = Mathf.Clamp(rot.z, 0, 180);
        // print(CamGP.transform.position );
        //
        //rot.y = Mathf.Clamp(rot.y, 0, 180); 
        //rot.y = 0;
        //transform.eulerAngles = rot;

        //transform.Rotate(Vector3.up, Space.World);
        // transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
        // transform.LookAt(Vector3.MoveTowards(transform.position + Camera.main.transform.rotation * Vector3.right, Camera.main.transform.rotation * Vector3.up, 1));
        //_direction = (Camera.main.position - transform.position).normalized;
        //transform.position = Vector3.MoveTowards(transform.position + Camera.main.transform.rotation * Vector3.right, Camera.main.transform.rotation * Vector3.up, 1);
        //Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
        //pos.x = 1000+Mathf.Clamp01(pos.x);
        //pos.y = 700+Mathf.Clamp01(pos.y);
        //pos.z = -100;
        //transform.position = Camera.main.ScreenToWorldPoint (pos);
        //var rotation:Quaternion = Camera.main.rotation;
        //position = Camera.main.position + (rotation *Vector3.forward * distance);
        //transform.position = position; 
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/4,Screen.height/4, Camera.main.nearClipPlane));
        //this.transform.position = worldPos;
        //position = Camera.main.ScreenToWorldPoint( Vector3(Screen.width/2, Screen.height/2, ) );
        //if (Physics.Raycast (ray, out hit)) {
        //	DescOBJ.SetActive (true);
        //}
        //else 
        //{
        //	DescOBJ.SetActive (false);
        //}
    }
}
