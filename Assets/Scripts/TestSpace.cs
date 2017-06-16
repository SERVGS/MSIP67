using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestSpace : MonoBehaviour 

{
	public int mice2;
		
		
	void Start () 
	{
		mice2 = 0;
	}

	void Update()
		{
		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			mice2 = mice2+1;
			print (mice2);
		}

	}

}
