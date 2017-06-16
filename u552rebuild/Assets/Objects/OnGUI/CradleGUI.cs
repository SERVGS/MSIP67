using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CradleGUI : MonoBehaviour
{
     public RuntimeAnimatorController animINST;
	//private Vector3 INITscale;
	//private float INITDISTScreen;
	//private float CurDISTScreen;
	private Vector3 INITCAMpos;
	//public Transform target;
	// public GameObject CradleGP;
	public GameObject CameraGP;
	public GameObject MS_GP;
	public GameObject ADDONS;
	private bool Cfade = false;
	private bool Cfadein = false;
	private bool Cfadeout = false;
	private bool Cfadeend = false;
	public GameObject[] Sils;
	// все силуэты с тэгом Silhouettes
	public GameObject[] Cradlefade;
	private bool Only;
	public GameObject[] ShowInfo;
	// подсказка на открыть инфу про крэдл
	public GameObject[] CloseInfo;
	// подсказка на закрыть инфу про крэдл
	public GameObject[] CloseMesh;
	public GameObject[] OnlyMesh;
	public GameObject[] NOTOnlyMesh;
	public GameObject[] PlayAn;
	//public GameObject[] ResetScript;
	private GameObject[] CrAnDescText;
	private GameObject[] CrAnDescPic;
	public GameObject Canvas;
	public GameObject CradleSub;
	private GameObject CM;
	// only camera main
	private Animator anim;
	private GameObject CradleAnimCanv;
	// for on-off canvas-anim
	private GameObject CrGUI;
	private GameObject CrDesc1;
	private GameObject CrDesc2;
	private GameObject CrDesc3;
	private bool TogCanvas;
	// канвас вкл
	//private float speed = 2.5f;
	private Vector3 INITpos;
	public int SSAN;
	//вкл-выкл анимации при старте
	Ray ray;
	RaycastHit hit;
	private GameObject[] TXT_NFO;
	private GameObject[] TXT_NFO2;
	private bool FWD;
	private bool BCW;
	public float AnimFulltime = 3f;
	private float AnimCurtime;

	public bool Close2Play;
	// change close-play icon cather



	//private Vector3 moveDistance;
	//public GameObject Cam;
	// Use this for initialization
	void Start ()
	{
		SSAN = 0;
		Only = false;
		FWD = false;
		BCW = false;
		AnimCurtime = AnimFulltime;

		// INITscale = transform.localScale;
		//INITposScreen = Camera.main.WorldToScreenPoint(INITpos);
		//INITDISTScreen = (Camera.main.WorldToScreenPoint(INITpos) - Camera.main.WorldToScreenPoint(target.position)).magnitude;

		//INITCAMpos = Camera.main.transform.position;
		Sils = GameObject.FindGameObjectsWithTag ("Silhouettes");
		Cradlefade = GameObject.FindGameObjectsWithTag ("Fade");
		//ResetScript = GameObject.FindGameObjectsWithTag ("ScriptsONOFF");
		CradleSub = transform.Find ("CradleGUI/CradleSubGP").gameObject;
		Canvas = CradleSub.transform.Find ("CradleCanvas").gameObject;


		ShowInfo = GameObject.FindGameObjectsWithTag ("ShowCradleINF");
		CloseInfo = GameObject.FindGameObjectsWithTag ("CloseCradleINF");
		CloseMesh = GameObject.FindGameObjectsWithTag ("CloseMesh");
		PlayAn = GameObject.FindGameObjectsWithTag ("PlayAn");
		OnlyMesh = GameObject.FindGameObjectsWithTag ("OnlyMesh");
		NOTOnlyMesh = GameObject.FindGameObjectsWithTag ("NotOnlyMesh");
		CrGUI = transform.Find ("CradleGUI").gameObject;
		CrAnDescPic = GameObject.FindGameObjectsWithTag ("CrAnimText");
		CrAnDescText = GameObject.FindGameObjectsWithTag ("CrAnimPic");

        //ShowInfo.SetActive(false);
        //CloseInfo.SetActive(false);
        CHANGEPlay(false);
        CHANGEClose(true);
        Highlight(0);
      	CHANGEShowInfo (false);
		CHANGECloseInfo (false);
		CHANGEMeshOnly (false);
		CHANGENOTMeshOnly (false);
		Canvas.SetActive (false);
		TogCanvas = false;
		//Zeroing();
		ZeroingCloseAndPlay ();
		ZeroingInfo ();
		ZeroingMeshOnly ();
		//

		Close2Play = false;
        
		//var inpos = GetComponent.<  > ().POSinit;
		CM = CameraGP.transform.Find ("Main Camera").gameObject;

		anim = CM.GetComponent<Animator> ();
		CradleAnimCanv = transform.Find ("CradleAnimINFO").gameObject;
		CradleAnimCanv.SetActive (false);
		CrDesc1 = CradleAnimCanv.transform.Find ("Canvas/DESC1").gameObject;
		CrDesc2 = CradleAnimCanv.transform.Find ("Canvas/DESC2").gameObject;
		CrDesc3 = CradleAnimCanv.transform.Find ("Canvas/DESC3").gameObject;
		CrDesc1.SetActive (false);
		CrDesc2.SetActive (false);
		CrDesc3.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
	{
        

		if (SSAN == -1 && anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {

			// print("Jump INIT");
			//if (SSAN == -1)
			//{
            
			CradleAnimCanv.SetActive (false);

			//print("Empty");
			CM.GetComponent<CamPublic> ().enabled = true;

			// CM.GetComponent<CamPublic>().enabled = true;
			CameraGP.GetComponent<Move> ().enabled = true;
			CradleSub.GetComponent<GUIZoom2> ().POSinit = true;
			CM.GetComponent<CamPublic> ().JumpIn = true;
            
			anim.enabled = false;
			CrGUI.SetActive (true);
			GameObject CradleSILONLY = transform.Find ("CradleSilheuteeOnly").gameObject;
			CradleSILONLY.GetComponent<Renderer> ().enabled = true;
			SSAN = 0;
			//}
		}
		if (anim.enabled == true) {
			AnimatorStateInfo currInfo = anim.GetCurrentAnimatorStateInfo (0);
			//FWD = true;
			//WaitingTime();
			if (FWD) { 
				//StartCoroutine(WaitingTime(5f)); 
				//BCW=false;
				AnimCurtime -= Time.deltaTime;
				//print (AnimCurtime);
				if (AnimCurtime <= 0) {
					SSAN = SSAN + 1;
					AnimCurtime = AnimFulltime;
				}

				if (SSAN > 4) { 
					SSAN = 2; 
				}
			}
			 if (BCW){
				
				AnimCurtime -= Time.deltaTime;
				//print (AnimCurtime);
				if (AnimCurtime <= 0) {
					SSAN = SSAN - 1;
					AnimCurtime = AnimFulltime;
				}
				//if (SSAN < -1) { SSAN = 3; }


				//BCW = true;
				//SSANrestrict ();

				if (SSAN == 0) { 

					SSAN = 3;

				} 
			}
            if (AnimCurtime < AnimFulltime-0.1f) { CameraGP.GetComponent<Move>().enabled = false; }

            //else
            // {
            //     SSAN = SSAN - 1;
            // }

            if (currInfo.IsName ("Idle")) {

				CameraGP.GetComponent<Move> ().RayCatchOn = true;
               
			} //else {
			//	CameraGP.GetComponent<Move> ().enabled = false;
				//if (currInfo.normalizedTime == currInfo.normalizedlength)
				//{
                
				//}
			//}
			//
			if (SSAN == 1 || SSAN == 4) {
				//CameraGP.GetComponent<Move>().enabled = false;
				CrDesc1.SetActive (true);
				CrDesc2.SetActive (false);
				CrDesc3.SetActive (false);
				//StartCoroutine (WaitingTime (2));



			} else if (SSAN == 2) {
				CrDesc1.SetActive (false);
				CrDesc2.SetActive (true);
				CrDesc3.SetActive (false);
				//StartCoroutine (WaitingTime (3));

			} else if (SSAN == 3) {
				CrDesc1.SetActive (false);
				CrDesc2.SetActive (false);
				CrDesc3.SetActive (true);
				//StartCoroutine (WaitingTime (4));

			}


		} else {
			CameraGP.GetComponent<Move> ().enabled = true;
		}

		anim.SetInteger ("CP", SSAN);

		MoveCradle ();
		//GameObject CradleGUI = transform.FindChild("CradleGUI").gameObject;
       
		if (CrGUI.activeSelf == true) {
			RayGUI ();
		}
	}

	void RayGUI ()
	{ 
		ray = Camera.main.ScreenPointToRay (Input.mousePosition); //рэйкаст от мыши

        

		if (Physics.Raycast (ray, out hit)) {

           //print(Close2Play);
			if (hit.transform.name == "CloseSignLoc") {

				ZeroingInfo ();
				ZeroingMeshOnly ();

				if (!Close2Play) {
					CHANGEPlay (false);
					CHANGEClose (true);
                    Highlight(1);
                    //PlayAn.SetActive(false);
                    //CloseMesh.SetActive(true);
                    if (Input.GetKeyDown (KeyCode.Mouse0)) {
                        
                        
						GameObject CradGP = ADDONS.transform.Find ("CradleNew_GP").gameObject;
						CradGP.SetActive (false);
						//Close2Play = true;




					}

				} else { // play is true
					//GameObject CM = CameraGP.transform.FindChild("Main Camera").gameObject;
                    
					//Animator anim = CM.GetComponent<Animator>();
					CHANGEPlay (true);
					CHANGEClose (false);
                    Highlight(2);

                    //bool CPstate = CM.GetComponent<CamPublic>().enabled;





                    if (Input.GetKeyDown (KeyCode.Mouse0)) {
                        //   CM.GetComponent<CamPublic>().JumpIn = true;
                        // CradleSub.GetComponent<GUIZoom2>().POSinit = true;

                       // Close2Play = false;
                        CradleSub.GetComponent<GUIZoom2> ().POSinit = true;
						CM.GetComponent<CamPublic> ().JumpIn = true;
						CradleAnimCanv.SetActive (true);
						anim.enabled = true;
                        anim.runtimeAnimatorController = animINST as RuntimeAnimatorController;
                        SSAN = 1;
						GameObject CradleSILONLY = transform.Find ("CradleSilheuteeOnly").gameObject;
						CradleSILONLY.GetComponent<Renderer> ().enabled = false;

						
                        
                        CrGUI.SetActive(false);



                    }
                   

                    
					//CM.GetComponent<CamPublic>().enabled = CPstate;

					//Close2Play = false;

				}

               
			}
                

                // CradleGP.SetActive(false);
            
            else if (hit.transform.name == "Info") {
				ZeroingCloseAndPlay ();

				ZeroingMeshOnly ();
                Highlight(3);

                if (TogCanvas == false) {

					CHANGEShowInfo (true);
					CHANGECloseInfo (false);
					//ShowInfo.SetActive(true);
					//CloseInfo.SetActive(false);
					//Zeroing (true);

					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						// transform.position = INITpos;
						//SendMessage("POSinit");
						//gameObject.GetComponent<GUIZoom2>().POSinit = true;
						TogCanvas = true;
						Canvas.SetActive (true);
						//CameraGP.transform.position = new Vector3 (-143.9f, 26.3f, -54f);

						CHANGEShowInfo (false);
						//ShowInfo.SetActive(false);

					}
				} else {
					CHANGEShowInfo (false);
					CHANGECloseInfo (true);

					//ShowInfo.SetActive(false);
					//CloseInfo.SetActive(true);
					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						TogCanvas = false;
						Canvas.SetActive (false);
						//CameraGP.transform.position = new Vector3 (-43.9f, 26.3f, -74f);
						CHANGECloseInfo (false);
						// CloseInfo.SetActive(false);


					}
				}
			} else if (hit.transform.name == "Only") {
				ZeroingCloseAndPlay ();
				ZeroingInfo ();
                Highlight(4);


                if (!Only) {
					CHANGEMeshOnly (true);
					CHANGENOTMeshOnly (false);
					//OnlyMesh.SetActive(true);
					//NOTOnlyMesh.SetActive(false);
					if (Input.GetKeyDown (KeyCode.Mouse0)) {

						//transform.position = Vector3.Lerp(pos, INITpos, 1f);
						//SendMessage("POSinit");
						// gameObject.GetComponent<GUIZoom2>().POSinit = true;
						Close2Play = true;
						GameObject childGO = ADDONS.transform.Find ("GradCap_GP").gameObject;
						childGO.SetActive (false);
						GameObject childGO2 = ADDONS.transform.Find ("SilCase_GP").gameObject;
						childGO2.SetActive (false);
						GameObject CradleSIL = transform.Find ("CradleSilheutee").gameObject;
						CradleSIL.GetComponent<Renderer> ().enabled = false;
						//foreach (GameObject ResetScript in ResetScript) {
						//	ResetScript.SetActive (false);
							// ResetScript.SetActive(true);

						//}

						GameObject MSGUI = MS_GP.transform.Find ("MS_INFO_GUI").gameObject;
						MSGUI.GetComponent<OBJOnCam> ().enabled = false;

						GameObject MSSUBGP = MSGUI.transform.Find ("MS_GUI_SubGP").gameObject;

						MSSUBGP.GetComponent<GuiZoom> ().enabled = false;

						GameObject REND_Line = MSSUBGP.transform.Find ("MS_All_locator/REND_LINE_GP").gameObject;

						REND_Line.GetComponent<Line_render> ().enabled = false;

						MS_GP.SetActive (false);


						Vector3 CamPos = CameraGP.transform.position;
						CamPos.z = 30;
						CameraGP.transform.position = CamPos;

						GameObject CloseChild = CradleSub.transform.Find ("CloseSignLoc").gameObject;
						GameObject CloseChildSprite = CloseChild.transform.Find ("CloseCradle").gameObject;
						GameObject PlayChild = CloseChild.transform.Find ("CradlePlay").gameObject;
						CloseChildSprite.SetActive (false);
						PlayChild.SetActive (true);
						//GameObject INFOChild = transform.FindChild("Info").gameObject;
						//INFOChild.transform.localPosition = new Vector3(0f, -7.0f, 0f);
						//GameObject ONLYChild = transform.FindChild("Only").gameObject;
						//ONLYChild.transform.localPosition = new Vector3(0f, 7.0f, 0f);


						if (Cfadein == true && Cfadeout == true && Cfadeend == false) {
							Cfade = true;
							Cfadein = false;
							Cfadeend = true;
							MoveCradle ();
							Only = true;
						} else {
							Cfade = false;
							Only = true;

						}
					}
				} else {
					CHANGEMeshOnly (false);
					CHANGENOTMeshOnly (true);
					//OnlyMesh.SetActive(false);
					//NOTOnlyMesh.SetActive(true);
					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						Input.ResetInputAxes ();
						Close2Play = false;
						GameObject CradleSIL = transform.Find ("CradleSilheutee").gameObject;
						CradleSIL.GetComponent<Renderer> ().enabled = true;
						//CM.BroadcastMessage("InitJump", true);
						//bool CMJI = Camera.main.GetComponent<CamPublic>().JumpIn;
						// CMJI = true;

						// transform.position = Vector3.Lerp(pos, INITpos, 1f);
						//SendMessage("POSinit");



						//foreach (GameObject ResetScript in ResetScript) {
                           
						//	ResetScript.SetActive (true);
						//}
						//GameObject CradleSub = transform.FindChild("CradleGUI/CradleSubGP").gameObject;
						CradleSub.GetComponent<GUIZoom2> ().POSinit = true;
						//GameObject CM = CameraGP.transform.FindChild("Main Camera").gameObject;
						CM.GetComponent<CamPublic> ().JumpIn = true;

						// Input.ResetInputAxes();
						Vector3 CamPos = CameraGP.transform.position;
						CamPos.z = -74;
						CameraGP.transform.position = CamPos;

						GameObject CloseChild = CradleSub.transform.Find ("CloseSignLoc").gameObject;
						GameObject CloseChildSprite = CloseChild.transform.Find ("CloseCradle").gameObject;
						GameObject PlayChild = CloseChild.transform.Find ("CradlePlay").gameObject;
						CloseChildSprite.SetActive (true);
						PlayChild.SetActive (false);

						GameObject childGO = ADDONS.transform.Find ("GradCap_GP").gameObject;
						childGO.SetActive (true);
						GameObject childGO2 = ADDONS.transform.Find ("SilCase_GP").gameObject;
						childGO2.SetActive (true);

						MS_GP.SetActive (true);
						GameObject MSGUI = MS_GP.transform.Find ("MS_INFO_GUI").gameObject;
						GameObject MSSUBGP = MS_GP.transform.Find ("MS_INFO_GUI/MS_GUI_SubGP").gameObject;
						MSGUI.GetComponent<OBJOnCam> ().enabled = true;
						MSSUBGP.GetComponent<GuiZoom> ().enabled = true;


						GameObject REND_Line = MSSUBGP.transform.Find ("MS_All_locator/REND_LINE_GP").gameObject;

						REND_Line.GetComponent<Line_render> ().enabled = true;



						Only = false;
					}
				}
			} else if (hit.transform.name == "CradleNew_GP_COL") {
                Highlight(0);

                if (!Only) {
					ZeroingCloseAndPlay ();
					ZeroingInfo ();
					ZeroingMeshOnly ();
					//Zeroing();
					SilFalse ();
					GameObject childGO3 = ADDONS.transform.Find ("CradleNew_GP").gameObject;
					GameObject childGO4 = childGO3.transform.Find ("CradleSilheutee").gameObject;
					childGO4.SetActive (true);
                   
					//

					if (Input.GetKeyDown (KeyCode.Mouse0)) {
                       
						// GetComponent<GUIZoom2>().POSinit = true;
						// CameraGP.BroadcastMessage("Raycatch", true);

						Cfade = true;
						// transform.position = Vector3.Lerp(pos, INITpos, 1f);
						//SendMessage("POSinit");



						if (Cfadein == true && Cfadeout == true && Cfadeend == false) {
							Cfadein = false;
							Cfadeend = true;
						} else {

							Cfadein = false;
							Cfadeout = false;
							Cfadeend = false;
						}


					}
				} else {
					ZeroingCloseAndPlay ();
					ZeroingInfo ();
					ZeroingMeshOnly ();
					//Zeroing();
					SilFalse ();
					GameObject childGO3 = ADDONS.transform.Find ("CradleNew_GP").gameObject;
					GameObject CradleSil2 = childGO3.transform.Find ("CradleSilheuteeOnly").gameObject;
					CradleSil2.SetActive (true); // подсветка второго слуэта когда Only

					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						// print("INITpos");
						//transform.position = INITpos;
						// GameObject CradleSub = transform.FindChild("CradleGUI/CradleSubGP").gameObject;
						CradleSub.GetComponent<GUIZoom2> ().POSinit = true;
						//gameObject.GetComponent<GUIZoom2>().POSinit = true;
						//GameObject CM = CameraGP.transform.FindChild("Main Camera").gameObject;
						// CM.BroadcastMessage("InitJump", true);
						//GameObject CM = CameraGP.transform.FindChild("Main Camera").gameObject;
						CM.GetComponent<CamPublic> ().JumpIn = true;


					}

				}
			} else if (hit.transform.name == "CollideMS" && Input.GetKeyDown (KeyCode.Mouse0)) {
				//transform.position = INITpos;
				//SendMessage("POSinit");
				//GameObject CradleSub = transform.FindChild("CradleGUI/CradleSubGP").gameObject;
				CradleSub.GetComponent<GUIZoom2> ().POSinit = true;
                //gameObject.GetComponent<GUIZoom2>().POSinit = true;
                Highlight(0);
                ZeroingCloseAndPlay ();
				ZeroingInfo ();
				ZeroingMeshOnly ();
				//CameraGP.BroadcastMessage("Raycatch", true);
				CameraGP.GetComponent<Move> ().RayCatchOn = true;
			} else if (hit.transform.tag != "NFOsigns") {
                Highlight(0);
                ZeroingCloseAndPlay ();
				ZeroingInfo ();
				ZeroingMeshOnly ();
				CHANGEClose (false);
				CHANGEPlay (false);
				CHANGEShowInfo (false);
				CHANGECloseInfo (false);
				CHANGEMeshOnly (false);
				CHANGENOTMeshOnly (false);

			}
		}
        //else if (hit.transform.name != "CradleNew_GP" || hit.transform.name != "Only" || hit.transform.name != "Info" || hit.transform.name != "CloseSignLoc")
        //{

        // }
        
        else {
			// Zeroing();
			ZeroingCloseAndPlay ();
			ZeroingInfo ();
			ZeroingMeshOnly ();
			CHANGEClose (false);
			CHANGEPlay (false);
			CHANGEShowInfo (false);
			CHANGECloseInfo (false);
			CHANGEMeshOnly (false);
			CHANGENOTMeshOnly (false);

			GameObject childGO3 = ADDONS.transform.Find ("CradleNew_GP").gameObject;
			GameObject CradleSil2 = childGO3.transform.Find ("CradleSilheuteeOnly").gameObject;
			CradleSil2.SetActive (false);
            Highlight(0);
            //ShowInfo.SetActive(false);
            //CloseInfo.SetActive(false);
            //CloseMesh.SetActive(false);
            //OnlyMesh.SetActive(false);
            //NOTOnlyMesh.SetActive(false);
            // Canvas.SetActive(false);

        }
        
	}

	void MoveCradle ()
	{
		if (Cfade) { // условие запуска
			Color tmp = Cradlefade [1].GetComponent<Renderer> ().material.color;
			GameObject CradleGP = ADDONS.transform.Find ("CradleNew_GP").gameObject;

			Input.ResetInputAxes ();

			if (Cfadein == false && Cfadeout == false && Cfadeend == false) {
				if (tmp.a >= 0.01f) { 

					tmp.a -= Time.deltaTime * 2.7f;
                  
				} else {
					tmp.a = 0f;
					Cfadein = true;
					GameObject CradleTrans = CradleGP.transform.Find ("CradleLP_trans").gameObject;
					CradleTrans.SetActive (true);
					GameObject CradleSub = CradleGP.transform.Find ("CradleSubGP").gameObject;
					CradleSub.transform.localPosition = new Vector3 (0f, 0f, -1.121f);

					GameObject CradleSil = CradleGP.transform.Find ("CradleSilheutee").gameObject;
					CradleSil.transform.localPosition = new Vector3 (0f, 0f, 0.319f);
				}
			} else if (Cfadein == true && Cfadeout == false && Cfadeend == false) {
				if (tmp.a < 1.0f) {
					tmp.a += Time.deltaTime * 2.7f;
				} else {
					tmp.a = 1.0f;
					Cfadeout = true;
					Cfade = false;
				}
			} else if (Cfadein == false && Cfadeout == true && Cfadeend == true) {
				if (tmp.a >= 0.01f) { // Sfade to 0 alpha 

					tmp.a -= Time.deltaTime * 2.7f;
				} else {
					tmp.a = 0f;
					Cfadein = true;
					Cfadeout = false;
                
					GameObject CradleTrans = CradleGP.transform.Find ("CradleLP_trans").gameObject;
					CradleTrans.SetActive (false);
					GameObject CradleSub = CradleGP.transform.Find ("CradleSubGP").gameObject;
					CradleSub.transform.localPosition = new Vector3 (0f, 0f, 0f);

					GameObject CradleSil = CradleGP.transform.Find ("CradleSilheutee").gameObject;
					CradleSil.transform.localPosition = new Vector3 (0f, 0f, -0.787372f);


				}
			} else if (Cfadein == true && Cfadeout == false && Cfadeend == true) {
				if (tmp.a < 1.0) {
					tmp.a += Time.deltaTime * 2.7f;
				} else {
					tmp.a = 1.0f;
					Cfadeout = false;
					Cfade = false;
					Cfadeend = false;
					Cfade = false;
				}
			}

			CradleGP.BroadcastMessage ("TranSET", tmp.a);

           

		}
	}

	void SilFalse ()
	{
		foreach (GameObject Sil in Sils) {
			Sil.SetActive (false);
		}
	}

	void ZeroingInfo ()
	{
		foreach (GameObject ShowInfo in ShowInfo) {
			ShowInfo.transform.localPosition = new Vector3 (0, 0, 0);
			ShowInfo.SendMessage ("Zeroing", true);
		}
		foreach (GameObject CloseInfo in CloseInfo) {
			CloseInfo.transform.localPosition = new Vector3 (0, 0, 0);
			CloseInfo.SendMessage ("Zeroing", true);
		}
	}

	void ZeroingCloseAndPlay ()
	{ 
            foreach (GameObject CloseMesh in CloseMesh)
            {
                CloseMesh.transform.localPosition = new Vector3(0, 0, 0);
                CloseMesh.SendMessage("Zeroing", true);
            }
       
        
            foreach (GameObject PlayAn in PlayAn)
            {
                PlayAn.transform.localPosition = new Vector3(0, 0, 0);
                PlayAn.SendMessage("Zeroing", true);
            }
        
	}

	void ZeroingMeshOnly ()
	{
		foreach (GameObject OnlyMesh in OnlyMesh) {
			OnlyMesh.transform.localPosition = new Vector3 (0, 0, 0);
			OnlyMesh.SendMessage ("Zeroing", true);
		}
		foreach (GameObject NOTOnlyMesh in NOTOnlyMesh) {
			NOTOnlyMesh.transform.localPosition = new Vector3 (0, 0, 0);
			NOTOnlyMesh.SendMessage ("Zeroing", true);
		}
	}

	void CHANGEShowInfo (bool state)
	{
		foreach (GameObject ShowInfo in ShowInfo) {
			ShowInfo.transform.GetComponent<Renderer> ().enabled = state;
		}
	}

	void CHANGECloseInfo (bool state)
	{ 
		foreach (GameObject CloseInfo in CloseInfo) {
			CloseInfo.transform.GetComponent<Renderer> ().enabled = state;
		}
	}

	void CHANGEClose (bool state)
	{
		foreach (GameObject CloseMesh in CloseMesh) {
			CloseMesh.transform.GetComponent<Renderer> ().enabled = state;
		}
	}

	void CHANGEPlay (bool state)
	{ 
		foreach (GameObject PlayAn in PlayAn) {
			PlayAn.transform.GetComponent<Renderer> ().enabled = state;
		}
	}

	void CHANGEMeshOnly (bool state)
	{
		foreach (GameObject OnlyMesh in OnlyMesh) {
			OnlyMesh.transform.GetComponent<Renderer> ().enabled = state;
		}
	}

	void CHANGENOTMeshOnly (bool state)
	{ 
		foreach (GameObject NOTOnlyMesh in NOTOnlyMesh) {
			NOTOnlyMesh.transform.GetComponent<Renderer> ().enabled = state;
		}
	}

	public void SetSSANUP (int state)
	{
        //StopCoroutine (WaitingTime (2));
        //StopCoroutine (WaitingTime (3));
        //StopCoroutine (WaitingTime (4));

        CameraGP.GetComponent<Move>().enabled = true;
        CameraGP.GetComponent<Move>().RayCatchOn = true;
       
        SSAN = SSAN + state;
        AnimCurtime = AnimFulltime;
        
            FWD = true;
		BCW = false;
		//BCW = false;
		//SSANrestrict ();
		if (SSAN > 4) { 
			SSAN = 2; 
		}
	}

	public void SetSSANDOWN (int state)
	{
		//StopCoroutine(WaitingTime(5f));

		//StopCoroutine (WaitingTime (2));
		//StopCoroutine (WaitingTime (3));
		//StopCoroutine (WaitingTime (4));
		
        //if (SSAN < -1) { SSAN = 3; }
        CameraGP.GetComponent<Move>().enabled = true;
        CameraGP.GetComponent<Move>().RayCatchOn = true;
        
        //CM.GetComponent<CamPublic>().JumpIn = true;
        
        SSAN = SSAN - state;
        AnimCurtime = AnimFulltime;
		BCW=true;
		FWD = false;
		//BCW = true;
		//SSANrestrict ();

		if (SSAN == 0) { 
			
			SSAN = 3;

		} else if (SSAN == -1) { 
			SSAN = 3; 
		}
	}

	public void SetSSAN (int state)
	{
        //StopCoroutine (WaitingTime (2));
        //StopCoroutine (WaitingTime (3));
        //StopCoroutine (WaitingTime (4));
        //StopCoroutine(WaitingTime(5f));
        CameraGP.GetComponent<Move>().enabled = true;
        CameraGP.GetComponent<Move>().RayCatchOn = true;
       
        SSAN = state;
		FWD = false;
		BCW = false;
		AnimCurtime = AnimFulltime;
		//SSANrestrict ();
	}
    public void SetSSANUP2STOP (int state)
    {
        //StopCoroutine (WaitingTime (2));
        //StopCoroutine (WaitingTime (3));
        //StopCoroutine (WaitingTime (4));
        
        CM.GetComponent<CamPublic>().JumpIn = true;
        CameraGP.GetComponent<Move>().enabled = true;
        AnimCurtime = AnimFulltime;
        SSAN = SSAN + state;
        FWD = false;
        BCW = false;
        //BCW = false;
        //SSANrestrict ();
        if (SSAN > 4)
        {
            SSAN = 2;
        }
    }

    public void SetSSANDOWN2STOP (int state)
    {
        //StopCoroutine(WaitingTime(5f));

        //StopCoroutine (WaitingTime (2));
        //StopCoroutine (WaitingTime (3));
        //StopCoroutine (WaitingTime (4));
        CM.GetComponent<CamPublic>().JumpIn = true;
        CameraGP.GetComponent<Move>().enabled = true;
        SSAN = SSAN - state;
        //if (SSAN < -1) { SSAN = 3; }

        AnimCurtime = AnimFulltime;
        BCW = false;
        FWD = false;
        //BCW = true;
        //SSANrestrict ();
        
        if (SSAN == 0)
        {

            SSAN = 3;

        }
        else if (SSAN == -1)
        {
            SSAN = 3;
        }
    }
    void Highlight(int num) // подсветка кнопок
    {
        GameObject CloseCr = transform.Find("CradleGUI/CradleSubGP/CloseSignLoc/CloseCradle").gameObject;
        GameObject ClosePl = transform.Find("CradleGUI/CradleSubGP/CloseSignLoc/CradlePlay").gameObject;
        GameObject CloseInf = transform.Find("CradleGUI/CradleSubGP/Info/i-sign_Cradle 1").gameObject;
        GameObject OnlySig = transform.Find("CradleGUI/CradleSubGP/Only/i-sign_Cradle 2").gameObject;
        Color tmp1 = CloseCr.GetComponent<SpriteRenderer>().material.color;
        Color tmp2 = ClosePl.GetComponent<SpriteRenderer>().material.color;
        Color tmp3 = CloseInf.GetComponent<SpriteRenderer>().material.color;
        Color tmp4 = OnlySig.GetComponent<SpriteRenderer>().material.color;

        if (num == 1)
        {
           tmp1.a = 1.0f;
            tmp2.a = 0.3f;
            if (TogCanvas)
            {


                tmp3.a = 1.0f;

            }
            else
            {

                tmp3.a = 0.3f;

            }

            tmp4.a = 0.3f;
        }
        else if (num == 2)
        {
            tmp2.a = 1.0f;
            tmp1.a = 0.3f;
            if (TogCanvas)
            {


                tmp3.a = 1.0f;

            }
            else
            {

                tmp3.a = 0.3f;

            }
            tmp4.a = 0.3f;
        }
        else if (num == 3)
        {
            tmp3.a = 1.0f;
            tmp1.a = 0.3f;
            tmp2.a = 0.3f;
            tmp4.a = 0.3f;
        }
        else if (num == 4)
        {
            tmp4.a = 1.0f;
            tmp2.a = 0.3f;
            if (TogCanvas)
            {


                tmp3.a = 1.0f;

            }
            else
            {

                tmp3.a = 0.3f;

            }
            tmp1.a = 0.3f;
        }
        else
        {
            tmp1.a = 0.3f;
            tmp2.a = 0.3f;
            tmp4.a = 0.3f;

            if (TogCanvas) {
                
                
                tmp3.a = 1.0f;
                
            }
            else { 
          
            tmp3.a = 0.3f;
            
            }
        }

            CloseCr.GetComponent<SpriteRenderer>().material.color = tmp1;
            ClosePl.GetComponent<SpriteRenderer>().material.color = tmp2;
            CloseInf.GetComponent<SpriteRenderer>().material.color = tmp3;
            OnlySig.GetComponent<SpriteRenderer>().material.color = tmp4;
    }

    //void SSANrestrict ()
    //{


    //}
    // public void Raycatch (bool state)
    //{
    //print("Raycatch");
    //   SSAN  = state;
    //print (RayCatchOn);

    //}
    // void Zeroing()
    //{
    //    TXT_NFO = GameObject.FindGameObjectsWithTag("TXTCradle");

    //     foreach (GameObject TXT_NFO in TXT_NFO)
    //     {
    //         TXT_NFO.transform.GetComponent<Renderer>().enabled = true;
    //         TXT_NFO.transform.GetComponent<ShowClose>().enabled = true;
    //         TXT_NFO.transform.localPosition = new Vector3(0, 0, 0);
    //         TXT_NFO.SendMessage("Zeroing", true);

    //     }

    // }
    //IEnumerator WaitingTime(int State)
    //{

    //CameraGP.GetComponent<Move>().enabled = false;


    //CameraGP.GetComponent<Move>().enabled = false;
    //yield return new WaitForSeconds(time);
    //SetSSAN(2);
    //yield return new WaitForSeconds(5f);
    //SetSSAN(3);
    //SSAN = State;
    //yield return new WaitForSeconds(time);
    //SetSSAN(4);
    //SSAN=4;
    //SSANrestrict ();
    //yield return new WaitForSeconds(time);
    //SetSSAN(2);
    //SSAN =2;
    // }
}