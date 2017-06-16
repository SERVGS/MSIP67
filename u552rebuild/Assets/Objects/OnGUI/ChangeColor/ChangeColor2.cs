using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor2 : MonoBehaviour {

	public float fadeIn = 0.1f;
	//public Color TextColor = Color.blue;
	public float speed = 1f;
	public float A_limit =1f;
	public bool DirectRight;
	public bool DirectUp;
	public bool togA;
	public float alpha;
	public GameObject MainGP;
	public int Twotimes = 0;
	// Use this for initialization
	void Start () {
		//Color color = gameObject.GetComponent <Renderer>().material.color ;
		alpha = 0f;
		togA = true;

	}
	
	// Update is called once per frame
	void Update ()
	{
		//print (Twotimes);
			var pos = transform.position;
			//print (TextColor.a);


		if (togA == true) {
				alpha += fadeIn * Time.deltaTime*3;

			} else {
				alpha -= fadeIn * Time.deltaTime*3;
			} 
		if (alpha <= 0) {
			//alpha = 0;
			togA = true;

		} else if (alpha >= A_limit) {
			togA = false;
			alpha = A_limit;
			Twotimes = Twotimes + 1;
		} 
		//else {Twotimes = 0;
		//}
		

		if (togA && Twotimes<1) {
				if (DirectRight && DirectUp) {
					pos += transform.right * speed * Time.deltaTime;
					//pos += transform.up * speed * Time.deltaTime;
				} else if (DirectRight && !DirectUp) {
					pos += transform.right * speed * Time.deltaTime;
					//pos -= transform.up * speed * Time.deltaTime;
				} else {
					pos -= transform.right * speed * Time.deltaTime;
					//pos -= transform.up * speed * Time.deltaTime;
				}
		} else if (!togA) {
				if (DirectRight && DirectUp) {
					pos -= transform.right * speed * Time.deltaTime;
					//pos -= transform.up * speed * Time.deltaTime;
				} else if (DirectRight && !DirectUp) {
					pos -= transform.right * speed * Time.deltaTime;
					//pos += transform.up * speed * Time.deltaTime;
				} else {
					pos += transform.right * speed * Time.deltaTime;
					//pos += transform.up * speed * Time.deltaTime;
				}
		
			}
		else 
		{
			alpha = 0f;
			togA = true;
		}
			transform.position = pos;
			Color tmp = gameObject.GetComponent <Renderer> ().material.color;
			tmp.a = alpha;

			gameObject.GetComponent <Renderer> ().material.color = tmp;
			//gameObject.GetComponent <TextMesh>().color= color;
			//gameObject.GetComponent<Renderer>().material.color.a = new Col.a;
			//gameObject.GetComponent.<TextMesh>().color.a += 

	}

}