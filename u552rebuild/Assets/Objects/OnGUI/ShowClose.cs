using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowClose : MonoBehaviour {

	public float fadeIn = 0.1f;
	private Color TextColor;
    private Color TextColorINIT;
    public float speed = 1f;
	public float A_limit =1f;
	public bool DirectRight;
	public bool togA;
	public int Twotimes = 0;
    private bool ST;
	// Use this for initialization
	void Start () {
        TextColor = gameObject.GetComponent <TextMesh>().color;
        TextColor.a = 0f;
        TextColorINIT = TextColor;
        togA = true;
        ST = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (ST)
        {
            //print("zeroing");
            gameObject.GetComponent<TextMesh>().color = TextColorINIT;
            TextColor.a = 0f;
            togA = true;
            Twotimes = 0;
            ST = false;
        }
        else
        {
            //print (Twotimes);
            var pos = transform.position;
            //print (TextColor.a);
         
            if (togA == true)
            {
                TextColor.a += fadeIn * Time.deltaTime;
            }
            else
            {
                TextColor.a -= fadeIn * Time.deltaTime;
            }
            if (TextColor.a <= 0)
            {
                togA = true;
            }

            else if (TextColor.a >= A_limit)
            {
                togA = false;
                TextColor.a = A_limit;
                Twotimes = Twotimes + 1;
            }


            if (togA && Twotimes < 1)
            {
                if (DirectRight)
                {
                    pos += transform.right * speed * Time.deltaTime;
                }
                else
                {
                    pos -= transform.right * speed * Time.deltaTime;
                }
            }
            else if (!togA)
            {
                if (DirectRight)
                {
                    pos -= transform.right * speed * Time.deltaTime;
                }
                else
                {
                    pos += transform.right * speed * Time.deltaTime;
                }
            }
            else
            {
                TextColor.a = 0f;
                togA = true;
            }

            transform.position = pos;

            gameObject.GetComponent<TextMesh>().color = TextColor;
            //gameObject.GetComponent<Renderer>().material.color = new TextColor;
            //gameObject.GetComponent.<TextMesh>().color.a += 
        }
    }
    public void Zeroing(bool state)
        
    {
        ST = state;


    }
}