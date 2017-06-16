using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTransp : MonoBehaviour {
    public GameObject[] SubMeshes;
    public float Transp;
    // Use this for initialization
    void Start ()
    {
        Transp = 1f;
    }

    // Update is called once per frame
    public void TranSET (float state)
    {
        Transp = state;
    }
    void Update () {
        MeshRenderer [] all = gameObject.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < all.Length; i++) 
        {
            Material cMat = all[i].GetComponent<Renderer>().material;
            all[i].GetComponent<Renderer>().material.color = new Color(cMat.color.r, cMat.color.g, cMat.color.b, Transp);
        }
       // Color[] tmp = SubMeshes.GetComponentsInChildren(typeof(Renderer).material.color;
    }
}
