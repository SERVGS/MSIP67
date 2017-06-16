using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPublic : MonoBehaviour {
	//public float cameraSensitivity = 90;
	//public float climbSpeed = 4;
	public float normalMoveSpeed = 5000;
	//public float slowMoveFactor = 0.25f;
	public float speed= 10;
	public Transform target;
	private float alpha;
	//public float fastMoveFactor = 3;
	//public GameObject MSSIL; 
	//private Renderer rend;
	//public bool RayCatchOn;
	public GameObject CameraGP; // группа камеры
	public GameObject MSSIL; // группа микросенс м2 одсветка
	//public GameObject SISIL; // Объект бампер подсветка
	public GameObject SISILVIRT; // Объект бампер прозрачный
	public GameObject SCASE;// Объект бампер
    //public GameObject AllAddOns; // группа всех доп частей
	public GameObject Canvas;
    public GameObject Microsense; //microsense group
    public GameObject CradleGUI;
    public GameObject[] ShowInfo;// подсказка на открыть инфу про МС
	public GameObject[] CloseInfo;// подсказка на закрыть инфу про МС
	public GameObject[] ShowParts;// подсказка на открыть части
	public GameObject[] CloseParts;// подсказка на открыть части
	public GameObject ADDONS; // группа всех доп частей
    public GameObject CradleGP;
    //public GameObject GradCapGP;
    //public GameObject SilCaseGP;
    public GameObject CC1;
	public GameObject CC2;
	public GameObject CC3;
	public GameObject[] COL_NFO;
	//public GameObject[] TXT_NFO;
    public GameObject[] Sils; // все силуэты с тэгом Silhouettes
    //private GameObject[] NFOsig;

    public GameObject[] Gradfade;
    //public GameObject CaseUp;
    //public GameObject CaseDown;
    //private Renderer rend;
    private bool TogCanvas; // канвас вкл 
	private bool TogMove0; //автозум к таргету
	private bool TogParts; // доп части вкл
	private int ClearColNFO;
	//private float scmove = -26.6f;
	private bool Sfade = false;
	private bool Sfadein = false;
	private bool Sfadeout = false;
	private bool Sfadeend = false;
     
    private bool Gfade = false;
    private bool Gfadein = false;
    private bool Gfadeout = false;
    private bool Gfadeend = false;
    public bool JumpIn;
    //private Vector3 pos1;
    //private Color tmp;

    Ray ray;
	RaycastHit hit;

	//private float transform = 0.0f;

	// Use this for initialization+
	void Start () 
	{
        JumpIn = false;
        //transform.localPosition = new Vector3(0, 450, 0);
        //MSSIL.SetActive (false);
        //rend = MSSIL.GetComponent<Renderer>();
        Sils = GameObject.FindGameObjectsWithTag("Silhouettes");
        //TXT_NFO = GameObject.FindGameObjectsWithTag("TXTchild");
        Gradfade = GameObject.FindGameObjectsWithTag("Fade2");
        ShowParts = GameObject.FindGameObjectsWithTag("ShowMSPARTS");
        CloseParts = GameObject.FindGameObjectsWithTag("CloseMSPARTS");
        ShowInfo = GameObject.FindGameObjectsWithTag("ShowMSNFO");
        CloseInfo = GameObject.FindGameObjectsWithTag("CloseMSNFO");

        FalseParts();
        FalseInfo();

        SilFalse();
        TogCanvas = false;
		TogMove0 = true;
		TogParts = false;
        SISILVIRT.SetActive(false);
        ZeroingInfo ();
        ZeroingParts();
        ZeroingCOl();
        RayCall ();
        AddONOFF();

    }

	// Update is called once per frame
	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); //рэйкаст от мыши
        GameObject SIlCase = ADDONS.transform.Find("SilCase_GP").gameObject;
        GameObject GC = ADDONS.transform.Find("SilCase_GP").gameObject;
        bool CRstate = CradleGP.activeSelf;
        bool GCstate = GC.activeSelf;
        bool SCstate = SIlCase.activeSelf;
        if (!CRstate && !GCstate && !SCstate)
        {
            TogParts = false;
        }
        //NFOsig = GameObject.FindGameObjectsWithTag("NFOsigns");
        if (JumpIn)
        {
            //print ("jumpin");
            transform.localPosition = new Vector3(0, 400, 0);
            //CameraGP.BroadcastMessage("Raycatch", true);
            CameraGP.GetComponent<Move>().RayCatchOn = true;
            // state = false;
            float rotationX = 90.0f;
            float rotationY = 180.0f;
            //transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.AngleAxis(rotationY, Vector3.up), Time.deltaTime * 3);
            transform.localRotation = Quaternion.AngleAxis(rotationY, Vector3.up);
            //transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.AngleAxis(rotationX, Vector3.right), Time.deltaTime * 3);
            transform.localRotation *= Quaternion.AngleAxis(-rotationX, Vector3.left);


            JumpIn = false;
        }
        //ADDONS.SetActive (TogParts);

        //INFOGP.transform.position = Camera.main.ScreenToWorldPoint( Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane) );
        //CanvasAct ();
        //print (hited);
        //print (TogMove0);
        Canvas.SetActive (TogCanvas);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        //if (Sfadein == true && Sfadeout == true) {
        //	SISILVIRT.SetActive (true);
        //}
        //rend.enabled = RayCatchOn;    // рендер силуэта 

        if (Input.GetAxis ("Mouse ScrollWheel") != 0) { //контроль зума
			var pos = transform.position;

			GameObject MSGUI= Microsense.transform.Find("MS_INFO_GUI/MS_GUI_SubGP").gameObject;
			GameObject CradleSub = CradleGP.transform.Find ("CradleGUI/CradleSubGP").gameObject;
            GameObject GCSub = ADDONS.transform.Find("GradCap_GP/GradCapGUI/GCGUISubGp").gameObject;
            GameObject SilSub = ADDONS.transform.Find("SilCase_GP/SilCaseGUI/SilCaseGUISubGp").gameObject;

            //	pos.x =  Mathf.Clamp(transform.position.x, -2.0f, 2.0f);
            pos += transform.forward * normalMoveSpeed * Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime;
			transform.position = pos;

			var posloc = transform.localPosition;
			Vector3 posloclimit = new Vector3 (0f, 200f, 0f);
			//posloc.y =  Mathf.Clamp(transform.localPosition.y, 200.0f, 1000.0f);
			if (posloc.y <200){
				transform.localPosition = Vector3.Lerp(posloc, posloclimit, 0.5f);
                CradleSub.GetComponent<GUIZoom2>().POSinit = true;
                GCSub.GetComponent<GUIZoom2> ().POSinit = true;
                SilSub.GetComponent<GUIZoom2>().POSinit = true;
                //MSGUI.GetComponent<GuiZoom> ().POSinit = true;
                //	MSGUI.GetComponent<GuiZoom> ().enabled = false;
            }
			//else{ MSGUI.GetComponent<GuiZoom> ().enabled  = true;}
			//print (posloclimit);

			//if (posloc.y <= 100) {
			//	posloc.y = 100;
				
			//} else
			//{
				
			//}
			//transform.localPosition = posloc;


		} 
		else if (TogMove0 == true) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, target.position, step);

		}
        //else {
        //	TogMove0 = false;
        //}
        if (Microsense.activeSelf)
        {
            RayCall();

        }
        else
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "CradleNew_GP_COL" && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //print("raycatch");
                    transform.localPosition = new Vector3(0, 400, 0);
                    //CameraGP.BroadcastMessage("Raycatch", true);
                    CameraGP.GetComponent<Move>().RayCatchOn = true;


                }
            }
          
        }


    }
    void RayCall () {

       // MoveSilcase ();
        
       // MoveGrad ();

        
	
		//print (Sfade);


			
		if (Physics.Raycast (ray, out hit)) {// рэйкаст попал на чейто колайдер 
                                             //hited = true;

            //COL_NFO = GameObject.FindGameObjectsWithTag ("CLNFOchild");

            if (hit.transform.name == "CollideMS" )
            {// рэйкаст попал на колайдер MS
             //rend.enabled = true;

                Highlight(0);
                SilFalse();
                MSSIL.SetActive(true);
                ZeroingInfo();
                ZeroingParts();
                ZeroingCOl();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //print("raycatch");
                    transform.localPosition = new Vector3(0, 400, 0);
                    CameraGP.GetComponent<Move>().RayCatchOn = true;
                }
                // MSSIL.GetComponent<Renderer> ().enabled = true;





            }
           


           

            //{
            //	CameraGP.BroadcastMessage ("Raycatch", false);
            //}


            else if (hit.transform.name == "MS_All_locator")
            {
                Highlight(2);
                FalseParts();
                ZeroingParts();
                ZeroingCOl();
                //Zeroing (true);
                // мыша попала на описание локатор

                //COL_NFO.SetActive (false);
                //public GameObject[] = COL_NFO;



                //ShowParts.SetActive(false);
                //CloseParts.SetActive(false);
                if (TogCanvas == false)
                {
                    

                    foreach (GameObject ShowInfo in ShowInfo)
                    {
                        ShowInfo.transform.GetComponent<Renderer>().enabled = true;
                       // print("ShowInfo");

                    }
                   // ShowInfo.SetActive(true);
                    //Zeroing (true);

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        TogCanvas = true;
                        //CameraGP.transform.position = new Vector3 (-143.9f, 26.3f, -54f);
                        //ShowInfo.SetActive(false);
                        foreach (GameObject ShowInfo in ShowInfo)
                        {
                            ShowInfo.transform.GetComponent<Renderer>().enabled = false;
                        }
                        //CameraGP.BroadcastMessage ("Raycatch", true);
                    }
                }
                else
                {
                    foreach (GameObject CloseInfo in CloseInfo)
                    {
                        CloseInfo.transform.GetComponent<Renderer>().enabled = true;
                    }
                    //CloseInfo.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        TogCanvas = false;
                        //CameraGP.transform.position = new Vector3 (-43.9f, 26.3f, -74f);
                        foreach (GameObject CloseInfo in CloseInfo)
                        {
                            CloseInfo.transform.GetComponent<Renderer>().enabled = false;
                        }

                       // CloseInfo.SetActive(false);
                        //CameraGP.BroadcastMessage ("Raycatch", true);
                    }

                    // rend.enabled = false;



                }
            }
            else if (hit.transform.name == "ChangeColLocator")
            {
                FalseParts();
                FalseInfo();
                ZeroingInfo();
                ZeroingParts();
                Highlight(3);
                //ClearColNFO = ClearColNFO + 1;
                //print (ClearColNFO);
                //CameraGP.BroadcastMessage ("Raycatch", false);					
                //ShowInfo.SetActive(false);
                // CloseInfo.SetActive(false);
                // ShowParts.SetActive(false);
                // CloseParts.SetActive(false);
                //COL_NFO_GP = true;
                //Zeroing (true);

                foreach (GameObject COL_NFO in COL_NFO)
                {
                    COL_NFO.transform.GetComponent<SpriteRenderer>().enabled = true;

                }



                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GameObject[] changecolors;
                    changecolors = GameObject.FindGameObjectsWithTag("MS_color");
                    if (CC1.activeSelf)
                    {
                        CC1.SetActive(false);
                        CC2.SetActive(true);
                        foreach (GameObject changecolor in changecolors)
                        {
                            changecolor.GetComponent<Renderer>().material.SetColor("_Color", new Color32(0, 0, 0, 255));


                        }
                    }
                    else if (CC2.activeSelf)
                    {
                        CC2.SetActive(false);
                        CC3.SetActive(true);
                        foreach (GameObject changecolor in changecolors)
                        {
                            changecolor.GetComponent<Renderer>().material.SetColor("_Color", new Color32(255, 42, 28, 255));

                        }
                    }
                    else if (CC3.activeSelf)
                    {
                        CC3.SetActive(false);
                        CC1.SetActive(true);
                        foreach (GameObject changecolor in changecolors)
                        {
                            changecolor.GetComponent<Renderer>().material.SetColor("_Color", new Color32(255, 122, 21, 255));

                        }
                        //CaseUp.GetComponent<Renderer>().material.SetColor ("_Color", new Color32(255, 122, 21, 255));
                        //CaseDown.GetComponent<Renderer>().material.SetColor ("_Color", new Color32(255, 122, 21, 255));
                        //Renderer rend = changecolor.GetComponent<Renderer>();
                        //ColorUtility.TryParseHtmlString (hexString, out color);
                        //rend.material.SetColor("_SpecColor", Color.red);

                    }

                }



            }
            else if (hit.transform.name == "ShowParts")
            {
                FalseInfo();
                ZeroingInfo();
                ZeroingCOl();
                Highlight(1);

                if (!TogParts)
                {

                    foreach (GameObject ShowParts in ShowParts)
                    {
                        ShowParts.transform.GetComponent<Renderer>().enabled = true;
                    }
                    foreach (GameObject CloseParts in CloseParts)
                    {
                        CloseParts.transform.GetComponent<Renderer>().enabled = false;
                    }
               

                }
                else
                {
                    foreach (GameObject ShowParts in ShowParts)
                    {
                        ShowParts.transform.GetComponent<Renderer>().enabled = false;
                    }
                    foreach (GameObject CloseParts in CloseParts)
                    {
                        CloseParts.transform.GetComponent<Renderer>().enabled = true;
                    }
               
                }

                if (Input.GetKeyDown(KeyCode.Mouse0) && !TogParts)
                {
                    TogParts = true;
                    AddONOFF();

                    //CradleGUI.GetComponent<CradleGUI>().Close2Play = false;

                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && TogParts)
                {
                    TogParts = false;
                    AddONOFF();
                }



            }
            

        }
        else {
            ZeroingInfo();
            ZeroingParts();
            FalseParts();
            FalseInfo();
            ZeroingCOl();
            //	gameObject.SetActive(false);
            //rend.enabled = false;
            //MSSIL.GetComponent<Renderer> ().enabled = false;
            //ShowInfo.SetActive (false);
            //CloseInfo.SetActive (false);
            //GameObject childGO = ADDONS.transform.FindChild ("SilCaseSilheutee").gameObject;
            //childGO.SetActive (false);
            // GameObject childGO1 = ADDONS.transform.FindChild("CradleSilheutee").gameObject;
            // childGO1.SetActive(false);
            // GameObject childGO2 = ADDONS.transform.FindChild("GrapCapSilheutee").gameObject;
            // childGO2.SetActive(false);
            SilFalse();
            Highlight(0);

            //SISIL.SetActive (false);
            //TogMove0 = false;
            //COL_NFO_GP.SetActive (false);


            //foreach (GameObject COL_NFO in COL_NFO) 
            //{
            //	if (COL_NFO.transform.GetComponent<SpriteRenderer>() != null) 
            //	{
            //		COL_NFO.transform.GetComponent<SpriteRenderer> ().enabled = false;
            //		COL_NFO.transform.localPosition = new Vector3 (0, 0, 0);
            //		COL_NFO.transform.GetComponent<ChangeColor> ().enabled = true;
            //		COL_NFO.transform.GetComponent<ChangeColor> ().togA = true;
            //		COL_NFO.transform.GetComponent<ChangeColor> ().alpha = 0f;
            //		COL_NFO.transform.GetComponent<ChangeColor> ().Twotimes = 0;
            //	}
            //	else if (COL_NFO.transform.GetComponent<ShowClose>() != null) {
            //		COL_NFO.transform.GetComponent<Renderer>().enabled = false;
            //		COL_NFO.transform.GetComponent<ShowClose>().enabled = true;
            //		COL_NFO.transform.localPosition = new Vector3 (0, 0, 0);

            //COL_NFO.transform.GetComponent<ChangeColor> ().enabled = false;
            //	}
        }
		//ClearColNFO = 0;
		//COL_NFO = GameObject.FindGameObjectsWithTag ("CLNFOchild");

		//foreach (GameObject COL_NFO in COL_NFO) 
		//{
		//	COL_NFO.transform.localPosition = new Vector3 (0, 0, 0);
		//}
	
	}
		
		///hited = false;
		//mouse0 = mouse0;
		//Canvas.SetActive (mouse0);
		//Canvas.SetActive (true);

		//CameraOBJ.SendMessage.RayCatch(false);

	
    
   
    void ZeroingCOl ()
	{
		COL_NFO = GameObject.FindGameObjectsWithTag ("CLNFOchild");
		//TXT_NFO = GameObject.FindGameObjectsWithTag ("TXTchild");

		foreach (GameObject COL_NFO in COL_NFO)
        {
			//if (COL_NFO.gameObject.GetComponent<SpriteRenderer>() != null) {
			COL_NFO.transform.GetComponent<SpriteRenderer> ().enabled = false;
			COL_NFO.transform.localPosition = new Vector3 (0, 0, 0);
			COL_NFO.transform.GetComponent<ChangeColor> ().enabled = true;
			COL_NFO.transform.GetComponent<ChangeColor> ().togA = true;
			COL_NFO.transform.GetComponent<ChangeColor> ().alpha = 0f;
			COL_NFO.transform.GetComponent<ChangeColor> ().Twotimes = 0;
			//}
			//else if (COL_NFO.gameObject.GetComponent<Renderer>() != null) {
				
			//}
		}
       


    }
    void ZeroingInfo()
    {
        foreach (GameObject ShowInfo in ShowInfo)
        {
            ShowInfo.transform.localPosition = new Vector3(0, 0, 0);
            ShowInfo.SendMessage("Zeroing", true);
        }
        foreach (GameObject CloseInfo in CloseInfo)
        {
            CloseInfo.transform.localPosition = new Vector3(0, 0, 0);
            CloseInfo.SendMessage("Zeroing", true);
        }
    }
    void ZeroingParts()
    {
        foreach (GameObject ShowParts in ShowParts)
        {
            ShowParts.transform.localPosition = new Vector3(0, 0, 0);
            ShowParts.SendMessage("Zeroing", true);
        }
        foreach (GameObject CloseParts in CloseParts)
        {
            CloseParts.transform.localPosition = new Vector3(0, 0, 0);
            CloseParts.SendMessage("Zeroing", true);
        }
    }

    void SilFalse()
    {
        foreach (GameObject Sil in Sils)
        {
            Sil.SetActive(false);
        }
    }
    void AddONOFF() // posinit надо для всех??
    {
		//GameObject CradleSub = CradleGP.transform.Find ("CradleGUI/CradleSubGP").gameObject;

		//CrNewGP.GetComponent<CradleGUI> ().enabled = TogParts;
		//CradleSub.GetComponent<GUIZoom2> ().POSinit = true;
        //print("posinit");
		int children = ADDONS.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            //GameObject ADDONS.GetChild(i);
            GameObject childGO = ADDONS.transform.GetChild(i).gameObject;
            childGO.SetActive(TogParts);
        }

    }
  

    public void InitJump (bool state)
    {
        JumpIn = state;
        //print("Raycatch");
        
        //print (RayCatchOn);

    }
    void FalseInfo()
    {
        foreach (GameObject ShowInfo in ShowInfo)
        {
            ShowInfo.transform.GetComponent<Renderer>().enabled = false;
        }
        foreach (GameObject CloseInfo in CloseInfo)
        {
            CloseInfo.transform.GetComponent<Renderer>().enabled = false;
        }
    }
    void FalseParts()
    {
        foreach (GameObject CloseParts in CloseParts)
        {
            CloseParts.transform.GetComponent<Renderer>().enabled = false;
        }
        foreach (GameObject ShowParts in ShowParts)
        {
            ShowParts.transform.GetComponent<Renderer>().enabled = false;
        }
    }
    void Highlight(int num) // подсветка кнопок
    {
        
        GameObject MSshowParts = Microsense.transform.Find("MS_INFO_GUI/MS_GUI_SubGP/ShowParts/Parts-sign").gameObject;
        GameObject MSinfoSig = Microsense.transform.Find("MS_INFO_GUI/MS_GUI_SubGP/i-sign3").gameObject;
       


        Color tmp1 = MSshowParts.GetComponent<SpriteRenderer>().material.color;
        Color tmp2 = MSinfoSig.GetComponent<SpriteRenderer>().material.color;
        Color tmp3 = CC1.GetComponent<SpriteRenderer>().material.color;
        Color tmp4 = CC2.GetComponent<SpriteRenderer>().material.color;
        Color tmp5 = CC3.GetComponent<SpriteRenderer>().material.color;
        

        if (num == 1)
        {
            tmp1.a = 1.0f;
            tmp3.a = 0.3f;
            tmp4.a = 0.3f;
            tmp5.a = 0.3f;

            if (TogCanvas)
            {
                
                tmp2.a = 1.0f;
               
            }

            else
            {
                tmp2.a = 0.3f;
                
            }
        }
        else if (num == 2)
        {
            tmp1.a = 0.3f;
            tmp2.a = 1.0f;  
            tmp3.a = 0.3f;
            tmp4.a = 0.3f;
            tmp5.a = 0.3f;
        }
        else if (num == 3)
        {
           
            tmp1.a = 0.3f;
            tmp3.a = 1.0f;
            tmp4.a = 1.0f;
            tmp5.a = 1.0f;
            if (TogCanvas)
            {

                tmp2.a = 1.0f;

            }

            else
            {
                tmp2.a = 0.3f;

            }
           

        }
        
        else
        {
            if (TogCanvas)
            {
                tmp1.a = 0.3f;
                tmp2.a = 1.0f;
                tmp3.a = 0.3f;
                tmp4.a = 0.3f;
                tmp5.a = 0.3f;
            }
            else
            {
                tmp1.a = 0.3f;
                tmp2.a = 0.3f;
                tmp3.a = 0.3f;
                tmp4.a = 0.3f;
                tmp5.a = 0.3f;
            }
        }

        MSshowParts.GetComponent<SpriteRenderer>().material.color = tmp1;
        MSinfoSig.GetComponent<SpriteRenderer>().material.color = tmp2;
        

        if (CC1.activeSelf) { CC1.GetComponent<SpriteRenderer>().material.color = tmp3; }
        else if (CC2.activeSelf) { CC2.GetComponent<SpriteRenderer>().material.color = tmp4; }
        else if (CC3.activeSelf) { CC3.GetComponent<SpriteRenderer>().material.color = tmp5; }
    }

}





//void CanvasAct()
//{
//	Canvas.SetActive (true);

//}

//MSSIL.GetComponent<Renderer>(RayCatchOn);
//	rend.enabled = RayCatchOn;
//CameraGP.BroadcastMessage("Raycatch",RayCatchOn);
//MSSIL.BroadcastMessage("Renderer",RayCatchOn);
//}

//	public void Raycatch (bool state)
//	{

//		RayCatchOn = state;
//print (RayCatchOn);
//
//	}


