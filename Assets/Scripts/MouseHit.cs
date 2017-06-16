using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseHit : MonoBehaviour 

{
		
		public GameObject CameraOBJ;
		private Renderer rend;
		public bool RayOn;
		Ray ray;
		RaycastHit hit;
		

	void Start () 
	{
		//gameObject.SetActive(false);
		rend = GetComponent<Renderer>();
        rend.enabled = false;

		//UpdateEverySecond();

		RayOn = false;
	}
	void gameObjSet()
	{
		
	}

		void Update()
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			//print (RayOn);

		if (Physics.Raycast (ray, out hit))
		{
			//print (hit.collider.name);
			rend.enabled = true;
			RayOn = true;
            CameraOBJ.BroadcastMessage("Raycatch", RayOn);
            //UpdateEverySecond();
            //CameraOBJ = Camera.main;




            //	gameObject.SetActive (true);
        } 
		else 
		{
		//	gameObject.SetActive(false);
			rend.enabled = false;
			RayOn = false;
            //CameraOBJ.SendMessage.RayCatch(false);
            CameraOBJ.BroadcastMessage("Raycatch", RayOn);
            //UpdateEverySecond();

		}

	}
	//void UpdateEverySecond()
	//{
		//Camera.main.GetComponent<CamPublic>().Raycatch(RayOn);
		
        
    //}
}
