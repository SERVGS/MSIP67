

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ButClicked : MonoBehaviour
{
    public GameObject button;
    private bool resbut;
    Ray ray;
    RaycastHit hit;
    // Use this for initialization
    void Start()
    {
        //EventSystem.current;
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerDownHandler);
        // ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerDownHandler);
        resbut = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var pointer = new PointerEventData(EventSystem.current);
       
        if (resbut) {
            
            ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerUpHandler);
        }
        else
        {
            ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerDownHandler);

        }
       
        
        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.name == "MenuCubeMesh")
            {
                
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    resbut = true;
                    


                }

                

            }
           
            



        }
        
    }
}
