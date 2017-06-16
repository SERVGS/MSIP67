using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGI : MonoBehaviour
{

    // Use this for initialization
    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        //InvokeRepeating("UpdateGI", 0, 0.1F);
    }

    void Update()
    {
        RendererExtensions.UpdateGIMaterials(renderer);
    }
}
