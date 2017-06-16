using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradCapGUI : MonoBehaviour
{


	private Vector3 INITCAMpos;// исходная позиция камеры

	public GameObject CameraGP;// группа камеры на которой весит скрипт
    public GameObject MS_GP;// основная камера
	public GameObject ADDONS; // все доп. опции
    public bool Gfade = false;
    public bool Gfadein = false;
    public bool Gfadeout = false;
    public bool Gfadeend = false;
	public GameObject Sil; // включаемый силуэт
    public GameObject SilVirt;// силуэт в режиме only
    public GameObject[] GCfade; // все сабмэши с тэгом Fade
	private bool Only;
	public GameObject[] ShowInfo; // подсказка на открыть инфу 
	public GameObject[] CloseInfo;	// подсказка на закрыть инфу
	public GameObject[] CloseMesh; // подсказка на закрыть мэш
    public GameObject[] OnlyMesh; // подсказка только мэш
    public GameObject[] NOTOnlyMesh; // подсказка показать все
    public GameObject[] PlayAn; // подсказка воспроизвести анимацию
    public RuntimeAnimatorController animINST;
    //private GameObject[] GCAnDescText; // объекты текста с описанием в канвасе при анимации
    //private GameObject[] GCAnDescPic; // объекты картинки с описанием в канвасе при анимации
    public GameObject Canvas; // канвас с исходным описанием
	public GameObject GCSub;// GUI подгруппа
	private GameObject CM; // camera main

	private Animator anim; 
	private GameObject GCAnimCanv; // канвас для анимации

    private GameObject GCGUI;
	private GameObject GCDesc1; // группа описаний при анимации 1
	private GameObject GCDesc2;// группа описаний при анимации 2
    private GameObject GCDesc3;// группа описаний при анимации 3
    private bool TogCanvas; // канвас вкл
	
	private Vector3 INITpos; // позиция при включении
	public int SSAN;	//вкл-выкл анимации при старте
	Ray ray;
	RaycastHit hit;
	//private GameObject[] TXT_NFO;
	//private GameObject[] TXT_NFO2;
	private bool FWD; // анимация вперед
	private bool BCW; // анимация назад
	private float AnimFulltime = 3f;
	private float AnimCurtime;

	public bool Close2Play; 	// change close-play icon cather

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
		Sil = transform.Find ("GC_SubGp/GrapCapSilheutee").gameObject;
        GCfade = GameObject.FindGameObjectsWithTag ("GSFade");
		//ResetScript = GameObject.FindGameObjectsWithTag ("ScriptsONOFF");
		GCSub = transform.Find ("GradCapGUI/GCGUISubGp").gameObject;
		Canvas = GCSub.transform.Find ("GCCaseCanvas").gameObject;


		ShowInfo = GameObject.FindGameObjectsWithTag ("GCShowINF");
		CloseInfo = GameObject.FindGameObjectsWithTag ("GCCloseINF");
		CloseMesh = GameObject.FindGameObjectsWithTag ("GCClose");
		PlayAn = GameObject.FindGameObjectsWithTag ("GCPlayAn");
		OnlyMesh = GameObject.FindGameObjectsWithTag ("GCOnlyMesh");
		NOTOnlyMesh = GameObject.FindGameObjectsWithTag ("GCNotOnlyMesh");
		GCGUI = transform.Find ("GradCapGUI").gameObject;
		//GCAnDescPic = GameObject.FindGameObjectsWithTag ("GSAnimText");// переназначить
        //GCAnDescText = GameObject.FindGameObjectsWithTag ("GSAnimPic");// переназначить

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
       // anim.enabled = true;
        //anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animation/GradCap/GradCap.controller", typeof(RuntimeAnimatorController));
       
        // anim.enabled = false;
        GCAnimCanv = transform.Find ("GCAnimINFO").gameObject; // переназначить
       
		GCDesc1 = GCAnimCanv.transform.Find ("Canvas/DESC1").gameObject;
		GCDesc2 = GCAnimCanv.transform.Find ("Canvas/DESC2").gameObject;
		GCDesc3 = GCAnimCanv.transform.Find ("Canvas/DESC3").gameObject;
		GCDesc1.SetActive (false);
		GCDesc2.SetActive (false);
		GCDesc3.SetActive (false);
        GCAnimCanv.SetActive(false);
    }

	// Update is called once per frame
	void Update ()
	{
        

		if (SSAN == -1 && anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {

			// print("Jump INIT");
			//if (SSAN == -1)
			//{
            
			GCAnimCanv.SetActive (false);

			//print("Empty");
			CM.GetComponent<CamPublic> ().enabled = true;

			// CM.GetComponent<CamPublic>().enabled = true;
			CameraGP.GetComponent<Move> ().enabled = true;
			GCSub.GetComponent<GUIZoom2> ().POSinit = true;
			CM.GetComponent<CamPublic> ().JumpIn = true;
            
			anim.enabled = false;
			GCGUI.SetActive (true);
			//GameObject CradleSILONLY = transform.Find ("CradleSilheuteeOnly").gameObject;
			Sil.GetComponent<Renderer> ().enabled = true;
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
				GCDesc1.SetActive (true);
                GCDesc2.SetActive (false);
                GCDesc3.SetActive (false);
				//StartCoroutine (WaitingTime (2));



			} else if (SSAN == 2) {
                GCDesc1.SetActive (false);
                GCDesc2.SetActive (true);
                GCDesc3.SetActive (false);
				//StartCoroutine (WaitingTime (3));

			} else if (SSAN == 3) {
                GCDesc1.SetActive (false);
                GCDesc2.SetActive (false);
                GCDesc3.SetActive (true);
				//StartCoroutine (WaitingTime (4));

			}


		} else {
			CameraGP.GetComponent<Move> ().enabled = true;
		}

		anim.SetInteger ("CP", SSAN);

        MoveGrad();
        //GameObject CradleGUI = transform.FindChild("CradleGUI").gameObject;

        if (GCGUI.activeSelf == true) {
			RayGUI ();
		}
	}

	void RayGUI ()
	{ 
		ray = Camera.main.ScreenPointToRay (Input.mousePosition); //рэйкаст от мыши

        

		if (Physics.Raycast (ray, out hit)) {

           //print(Close2Play);
			if (hit.transform.name == "GC_Close") {

				ZeroingInfo ();
				ZeroingMeshOnly ();

				if (!Close2Play) {
					CHANGEPlay (false);
					CHANGEClose (true);
                    Highlight(1);
                    //PlayAn.SetActive(false);
                    //CloseMesh.SetActive(true);
                    if (Input.GetKeyDown (KeyCode.Mouse0)) {
                        
                        
						GameObject GC_GP = ADDONS.transform.Find ("GradCap_GP").gameObject;
                        GC_GP.SetActive (false);
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
                        GCSub.GetComponent<GUIZoom2> ().POSinit = true;
						CM.GetComponent<CamPublic> ().JumpIn = true;
                        GCAnimCanv.SetActive (true);
						anim.enabled = true;
                        SilVirt.GetComponent<Renderer>().enabled = false;
                        anim.runtimeAnimatorController = animINST as RuntimeAnimatorController;
                        SSAN = 1;
						//GameObject CradleSILONLY = transform.Find ("CradleSilheuteeOnly").gameObject;
						//CradleSILONLY.GetComponent<Renderer> ().enabled = false;



                        GCGUI.SetActive(false);



                    }
                   

                    
					//CM.GetComponent<CamPublic>().enabled = CPstate;

					//Close2Play = false;

				}

               
			}
                

                // CradleGP.SetActive(false);
            
            else if (hit.transform.name == "GC_Info") {
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
			} else if (hit.transform.name == "GC_Only") {
				ZeroingCloseAndPlay ();
				ZeroingInfo ();
                Highlight(4);


                if (!Only) {
					CHANGEMeshOnly (true);
					CHANGENOTMeshOnly (false);
					//OnlyMesh.SetActive(true);
					//NOTOnlyMesh.SetActive(false);
					if (Input.GetKeyDown (KeyCode.Mouse0)) {
                        SilVirt.SetActive(true);
						//transform.position = Vector3.Lerp(pos, INITpos, 1f);
						//SendMessage("POSinit");
						// gameObject.GetComponent<GUIZoom2>().POSinit = true;
						Close2Play = true;
						GameObject childGO = ADDONS.transform.Find ("CradleNew_GP").gameObject;
						childGO.SetActive (false);
						GameObject childGO2 = ADDONS.transform.Find ("SilCase_GP").gameObject;
						childGO2.SetActive (false);
						//GameObject CradleSIL = transform.Find ("CradleSilheutee").gameObject;
						Sil.GetComponent<Renderer> ().enabled = false;
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
						CamPos.z = -185;
						CameraGP.transform.position = CamPos;

						//GameObject CloseChild = GCSub.transform.Find ("CloseSignLoc").gameObject;
						GameObject CloseChildSprite = GCSub.transform.Find ("GC_Close/CloseGC").gameObject;
						GameObject PlayChild = GCSub.transform.Find ("GC_Close/PlayGC").gameObject;
						CloseChildSprite.SetActive (false);
						PlayChild.SetActive (true);
						//GameObject INFOChild = transform.FindChild("Info").gameObject;
						//INFOChild.transform.localPosition = new Vector3(0f, -7.0f, 0f);
						//GameObject ONLYChild = transform.FindChild("Only").gameObject;
						//ONLYChild.transform.localPosition = new Vector3(0f, 7.0f, 0f);


						if (Gfadein == true && Gfadeout == true && Gfadeend == false) {
							Gfade = true;
							Gfadein = false;
							Gfadeend = true;
                        

                            Only = true;
						} else {
							Gfade = false;
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
						//GameObject CradleSIL = transform.Find ("CradleSilheutee").gameObject;
						Sil.GetComponent<Renderer> ().enabled = true;
                        SilVirt.SetActive(false);
                        
                        //CM.BroadcastMessage("InitJump", true);
                        //bool CMJI = Camera.main.GetComponent<CamPublic>().JumpIn;
                        // CMJI = true;

                        // transform.position = Vector3.Lerp(pos, INITpos, 1f);
                        //SendMessage("POSinit");



                        //foreach (GameObject ResetScript in ResetScript) {

                        //	ResetScript.SetActive (true);
                        //}
                        //GameObject CradleSub = transform.FindChild("CradleGUI/CradleSubGP").gameObject;
                        GCSub.GetComponent<GUIZoom2> ().POSinit = true;
						//GameObject CM = CameraGP.transform.FindChild("Main Camera").gameObject;
						CM.GetComponent<CamPublic> ().JumpIn = true;

						// Input.ResetInputAxes();
						Vector3 CamPos = CameraGP.transform.position;
						CamPos.z = -74;
						CameraGP.transform.position = CamPos;

                        GameObject CloseChildSprite = GCSub.transform.Find("GC_Close/CloseGC").gameObject;
                        GameObject PlayChild = GCSub.transform.Find("GC_Close/PlayGC").gameObject;
                        CloseChildSprite.SetActive (true);
						PlayChild.SetActive (false);

                        GameObject childGO = ADDONS.transform.Find("CradleNew_GP").gameObject;
                        childGO.SetActive(true);
                        GameObject childGO2 = ADDONS.transform.Find("SilCase_GP").gameObject;
                        childGO2.SetActive(true);

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
			} else if (hit.transform.name == "GradCap_Colider") {
                Highlight(0);

                if (!Only) {
					ZeroingCloseAndPlay ();
					ZeroingInfo ();
					ZeroingMeshOnly ();
                    //Zeroing();
                    Sil.SetActive(true);
                    
                   
					//

					if (Input.GetKeyDown (KeyCode.Mouse0)) {
                       
						// GetComponent<GUIZoom2>().POSinit = true;
						// CameraGP.BroadcastMessage("Raycatch", true);

						Gfade = true;
						// transform.position = Vector3.Lerp(pos, INITpos, 1f);
						//SendMessage("POSinit");



						if (Gfadein == true && Gfadeout == true && Gfadeend == false) {
							Gfadein = false;
							Gfadeend = true;
						}
                        else
                        {

							Gfadein = false;
							Gfadeout = false;
							Gfadeend = false;
						}


					}
				}
                else {
					ZeroingCloseAndPlay ();
					ZeroingInfo ();
					ZeroingMeshOnly ();
                    //Zeroing();
                    Sil.SetActive(false);
                    SilVirt.SetActive(true);
                    // подсветка второго слуэта когда Only
                    SilVirt.GetComponent<Renderer>().enabled = true;
                    if (Input.GetKeyDown (KeyCode.Mouse0)) {
                        // print("INITpos");
                        //transform.position = INITpos;
                        // GameObject CradleSub = transform.FindChild("CradleGUI/CradleSubGP").gameObject;
                        GCSub.GetComponent<GUIZoom2> ().POSinit = true;
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
                GCSub.GetComponent<GUIZoom2> ().POSinit = true;
                //gameObject.GetComponent<GUIZoom2>().POSinit = true;
                //Highlight(0);
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
                SilVirt.GetComponent<Renderer>().enabled = false;
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
			//GameObject CradleSil2 = childGO3.transform.Find ("CradleSilheuteeOnly").gameObject;
            
            SilVirt.GetComponent<Renderer>().enabled = false;
            SilVirt.SetActive(false);
            Highlight(0);
            //ShowInfo.SetActive(false);
            //CloseInfo.SetActive(false);
            //CloseMesh.SetActive(false);
            //OnlyMesh.SetActive(false);
            //NOTOnlyMesh.SetActive(false);
            // Canvas.SetActive(false);

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
    void MoveGrad()
    {
        GameObject GradSub = transform.Find("GC_SubGp/V1_20UVGrad_cap").gameObject;
        GameObject GCTrans = transform.Find("GC_SubGp/GradCapLP_trans").gameObject;
        GameObject SilCaseGP = ADDONS.transform.Find("SilCase_GP").gameObject;
        GameObject SilTrans = SilCaseGP.transform.Find("SilCaseSubGp/SilCaseLP_trans").gameObject;
        if (Gfade) // условие запуска
        {
            Input.ResetInputAxes();
            Color tmp = GCfade[1].GetComponent<Renderer>().material.color;

            Input.ResetInputAxes();

            if (Gfadein == false && Gfadeout == false && Gfadeend == false)
            {
                if (tmp.a >= 0.01f)
                {

                    tmp.a -= Time.deltaTime * 2.7f;
                    //print(tmp.a);
                }
                else
                {
                    tmp.a = 0f;
                    Gfadein = true;
                    
                    GCTrans.SetActive(true);
                    //SilCaseGP.SendMessage("SFade", true);
                    if (SilTrans.activeSelf)
                    {
                        SilCaseGP.GetComponent<SilCaseGUI>().Sfade = true;
                        SilCaseGP.GetComponent<SilCaseGUI>().Sfadein = false;
                        SilCaseGP.GetComponent<SilCaseGUI>().Sfadeout = true;
                        SilCaseGP.GetComponent<SilCaseGUI>().Sfadeend = true;
                    }
                   // CameraOBJ.SendMessage.RayCatch(false);
                   //GameObject GradSub = transform.Find("GC_SubGp/V1_20UVGrad_cap").gameObject;
                    GradSub.transform.localPosition = new Vector3(-0.7f, 6.9f, 68.4f);
                    //GameObject GradSil = transform.Find("GC_SubGp/GrapCapSilheutee").gameObject;
                    Sil.transform.localPosition = new Vector3(-0.7f, 6.9f, 2.8f);
                }
            }
            else if (Gfadein == true && Gfadeout == false && Gfadeend == false)
            {
                if (tmp.a < 1.0f)
                {
                    tmp.a += Time.deltaTime * 2.7f;
                }
                else
                {
                    tmp.a = 1.0f;
                    Gfadeout = true;
                    Gfade = false;
                }
            }
            else if (Gfadein == false && Gfadeout == true && Gfadeend == true)
            {
                if (tmp.a >= 0.01f)
                { // Sfade to 0 alpha 

                    tmp.a -= Time.deltaTime * 2.7f;
                }
                else
                {
                    tmp.a = 0f;
                    Gfadein = true;
                    Gfadeout = false;
                    
                    GradSub.transform.localPosition = new Vector3(-0.7f, 6.9f, 2.8f);
                    
                    Sil.transform.localPosition = new Vector3(-0.7f, 6.9f, 68.4f);
                    GCTrans.SetActive(false); 

                }
            }
            else if (Gfadein == true && Gfadeout == false && Gfadeend == true)
            {
                if (tmp.a < 1.0)
                {
                    tmp.a += Time.deltaTime * 2.7f;
                }
                else
                {
                    tmp.a = 1.0f;
                    Gfadeout = false;
                    Gfade = false;
                    Gfadeend = false;
                    Gfade = false;
                }
            }

            GradSub.BroadcastMessage("TranSET", tmp.a);



        }

    }
    void Highlight(int num) // подсветка кнопок
    {
        GameObject Close = transform.Find("GradCapGUI/GCGUISubGp/GC_Close/CloseGC").gameObject;
        GameObject ClosePl = transform.Find("GradCapGUI/GCGUISubGp/GC_Close/PlayGC").gameObject;
        GameObject CloseInf = transform.Find("GradCapGUI/GCGUISubGp/GC_Info/i-sign_GC").gameObject;
        GameObject OnlySig = transform.Find("GradCapGUI/GCGUISubGp/GC_Only/i-sign_GC2").gameObject;
        Color tmp1 = Close.GetComponent<SpriteRenderer>().material.color;
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

            if (TogCanvas)
            {


                tmp3.a = 1.0f;

            }
            else
            {

                tmp3.a = 0.3f;

            }
        }

        Close.GetComponent<SpriteRenderer>().material.color = tmp1;
        ClosePl.GetComponent<SpriteRenderer>().material.color = tmp2;
        CloseInf.GetComponent<SpriteRenderer>().material.color = tmp3;
        OnlySig.GetComponent<SpriteRenderer>().material.color = tmp4;
    }

}