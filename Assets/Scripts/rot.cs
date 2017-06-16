using UnityEngine;
using System.Collections;

public class rot : MonoBehaviour {

    void Update() 
    {
        // Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(Vector3.up * 20*Time.deltaTime);

        // ...also rotate around the World's Y axis
        //transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
    }
}