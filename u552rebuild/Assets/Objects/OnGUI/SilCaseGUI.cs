using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilCaseGUI : MonoBehaviour
{

    //private Vector3 INITscale;
    //private float INITDISTScreen;
    //private float CurDISTScreen;
    private Vector3 INITCAMpos;
    //public Transform target;
    // public GameObject CradleGP;
    public GameObject CameraGP;
    public GameObject MS_GP;
    public GameObject ADDONS;
    public bool Sfade = false;
    public bool Sfadein = false;
    public bool Sfadeout = false;
    public bool Sfadeend = false;
    public GameObject Sil;
    public GameObject Sil2;
    // все силуэты с тэгом Silhouettes
    public GameObject[] SCfade;
    private bool Only;
    public GameObject[] ShowInfo;
    // подсказка на открыть инфу про крэдл
    public GameObject[] CloseInfo;
    // подсказка на закрыть инфу про крэдл
    public GameObject[] CloseMesh;
    public GameObject[] OnlyMesh;
    public GameObject[] NOTOnlyMesh;
    public GameObject[] SCchangecolor;
    //public GameObject[] PlayAn;
    //public GameObject[] ResetScript;
    private GameObject[] SCAnDescText;
    private GameObject[] SCAnDescPic;
    private GameObject SCSub;
    private GameObject CM;
    private GameObject CloseCS;
    private GameObject SC_CC; //change color
    private GameObject Canvas;
    private int CC;
    private GameObject SilCaseLP;
    private GameObject SilCaseINT;// внутренность для полупрозрачного материала
    //private bool Close2CC;
    // основная камера, потому что их две
    // only camera main
    //private Animator anim;
    //private GameObject SCAnimCanv;
    // for on-off canvas-anim
    //private GameObject SCGUI;
    //private GameObject SCDesc1;
    //private GameObject SCDesc2;
    //private GameObject SCDesc3;
    private bool TogCanvas;
    // канвас вкл
    //private float speed = 2.5f;
    private Vector3 INITpos;
    //public int SSAN;
    //вкл-выкл анимации при старте
    Ray ray;
    RaycastHit hit;
    private GameObject[] TXT_NFO;
    private GameObject[] TXT_NFO2;
    private float alpha;
    public GameObject[] COL_NFO;

    void Start()
    {
        COL_NFO = GameObject.FindGameObjectsWithTag("SC_ChangeColNFO");
        CC = 1;
        Only = false;
        //Close2CC = false;
        //FWD = false;
        //BCW = false;
        //AnimCurtime = AnimFulltime;

        // INITscale = transform.localScale;
        //INITposScreen = Camera.main.WorldToScreenPoint(INITpos);
        //INITDISTScreen = (Camera.main.WorldToScreenPoint(INITpos) - Camera.main.WorldToScreenPoint(target.position)).magnitude;

        //INITCAMpos = Camera.main.transform.position;
        //Sil = GameObject.Find ("SilCaseSubGp/SilCaseLP/SilCaseSilheutee");
        SCfade = GameObject.FindGameObjectsWithTag("SCFade"); //если содержит кучу подобъектов
                                                             
        SCSub = transform.Find("SilCaseGUI/SilCaseGUISubGp").gameObject;
        Canvas = SCSub.transform.Find("SilCaseCanvas").gameObject;
        SilCaseLP = transform.Find("SilCaseSubGp/SilCaseLP").gameObject;
        SilCaseINT = SilCaseLP.transform.Find("SIL_CASE_RET1_20UV/default").gameObject;

        ShowInfo = GameObject.FindGameObjectsWithTag("SCShowINF");
        CloseInfo = GameObject.FindGameObjectsWithTag("SCCloseINF");
        CloseMesh = GameObject.FindGameObjectsWithTag("SCClose");
        //PlayAn = GameObject.FindGameObjectsWithTag ("PlayAn");
        OnlyMesh = GameObject.FindGameObjectsWithTag("SCOnlyMesh");
        NOTOnlyMesh = GameObject.FindGameObjectsWithTag("SCNotOnlyMesh");

        SCchangecolor = GameObject.FindGameObjectsWithTag("SCCC");
        // print(SCchangecolor.Length);
        CloseCS = SCSub.transform.Find("SC_Close/CloseCS").gameObject;
        SC_CC = SCSub.transform.Find("SC_Close/SC_CC").gameObject;
        CloseCS.SetActive(true);
        SC_CC.SetActive(false);
        alpha = 0.717f;
        //SCGUI = transform.Find ("CradleGUI").gameObject;
        //CrAnDescPic = GameObject.FindGameObjectsWithTag ("CrAnimText");
        //CrAnDescText = GameObject.FindGameObjectsWithTag ("CrAnimPic");

        //ShowInfo.SetActive(false);
        //CloseInfo.SetActive(false);
        //CHANGEPlay(false);
        CHANGEClose(true);
        Highlight(0);
        CHANGEShowInfo(false);
        CHANGECloseInfo(false);
        CHANGEMeshOnly(false);
        CHANGENOTMeshOnly(false);
        //Canvas.SetActive (false);
        TogCanvas = false;
        //Zeroing();
        ZeroingClose();
        ZeroingInfo();
        ZeroingMeshOnly();
        ZeroingCOl();
        //

        //Close2Play = false;

        //var inpos = GetComponent.<  > ().POSinit;
        CM = CameraGP.transform.Find("Main Camera").gameObject;


    }

    // Update is called once per frame
    void Update()
    {
        MoveSilcase();

        ray = Camera.main.ScreenPointToRay(Input.mousePosition); //рэйкаст от мыши




        if (Physics.Raycast(ray, out hit))
        {

            //print(Close2Play);
            if (hit.transform.name == "SC_Close")
            {

                ZeroingInfo();
                ZeroingMeshOnly();

                if (!Only)
                {
                    CHANGEClose(true);
                    Highlight(1);
                }
                else
                {
                    Highlight(2);
                    foreach (GameObject COL_NFO in COL_NFO)
                    {
                        COL_NFO.transform.GetComponent<SpriteRenderer>().enabled = true;

                    }
                }
                //PlayAn.SetActive(false);
                //CloseMesh.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (!Only)
                    {
                        this.gameObject.SetActive(false);

                    }
                    else
                    {


                        if (CC >= 3)
                        { CC = 0; }
                        CC++;
                        ChangeCol(CC);

                    }
                }



            }




            else if (hit.transform.name == "SC_Info")
            {
                ZeroingClose();

                ZeroingMeshOnly();
                Highlight(3);
                ZeroingCOl();

                if (TogCanvas == false)
                {

                    CHANGEShowInfo(true);
                    CHANGECloseInfo(false);
                    //ShowInfo.SetActive(true);
                    //CloseInfo.SetActive(false);
                    //Zeroing (true);

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        // transform.position = INITpos;
                        //SendMessage("POSinit");
                        //gameObject.GetComponent<GUIZoom2>().POSinit = true;
                        TogCanvas = true;
                        Canvas.SetActive(true);
                        //CameraGP.transform.position = new Vector3 (-143.9f, 26.3f, -54f);

                        CHANGEShowInfo(false);
                        //ShowInfo.SetActive(false);

                    }
                }
                else
                {
                    CHANGEShowInfo(false);
                    CHANGECloseInfo(true);

                    //ShowInfo.SetActive(false);
                    //CloseInfo.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        TogCanvas = false;
                        Canvas.SetActive(false);
                        //CameraGP.transform.position = new Vector3 (-43.9f, 26.3f, -74f);
                        CHANGECloseInfo(false);
                        // CloseInfo.SetActive(false);


                    }
                }
            }
            else if (hit.transform.name == "SC_Only")
            {
                ZeroingClose();
                ZeroingInfo();
                Highlight(4);
                ZeroingCOl();

                if (!Only)
                {
                    CHANGEMeshOnly(true);
                    CHANGENOTMeshOnly(false);
                    //OnlyMesh.SetActive(true);
                    //NOTOnlyMesh.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {

                        CloseCS.SetActive(false);
                        SC_CC.SetActive(true);
                        //transform.position = Vector3.Lerp(pos, INITpos, 1f);
                        //SendMessage("POSinit");
                        // gameObject.GetComponent<GUIZoom2>().POSinit = true;
                        //Close2Play = true;
                        GameObject childGO = ADDONS.transform.Find("GradCap_GP").gameObject;
                        childGO.SetActive(false);
                        GameObject childGO2 = ADDONS.transform.Find("CradleNew_GP").gameObject;
                        childGO2.SetActive(false);
                        //GameObject SCSIL = transform.Find ("SilCaseSubGp/SilCaseLP/SilCaseSilheutee").gameObject;

                        ChangeCol(CC);
                        //Sil.GetComponent<Renderer> ().enabled = false;

                        //foreach (GameObject ResetScript in ResetScript) {
                        //	ResetScript.SetActive (false);
                        // ResetScript.SetActive(true);

                        //}

                        GameObject MSGUI = MS_GP.transform.Find("MS_INFO_GUI").gameObject;
                        MSGUI.GetComponent<OBJOnCam>().enabled = false;

                        GameObject MSSUBGP = MSGUI.transform.Find("MS_GUI_SubGP").gameObject;

                        MSSUBGP.GetComponent<GuiZoom>().enabled = false;

                        GameObject REND_Line = MSSUBGP.transform.Find("MS_All_locator/REND_LINE_GP").gameObject;

                        REND_Line.GetComponent<Line_render>().enabled = false;

                        MS_GP.SetActive(false);


                        Vector3 CamPos = CameraGP.transform.position;
                        CamPos.x = 45;
                        CameraGP.transform.position = CamPos;

                        //GameObject CloseChild = SCSub.transform.Find ("CloseSignLoc").gameObject;
                        //GameObject CloseChildSprite = CloseChild.transform.Find ("CloseCradle").gameObject;
                        //GameObject PlayChild = CloseChild.transform.Find ("CradlePlay").gameObject;
                        //CloseChildSprite.SetActive (false);
                        //PlayChild.SetActive (true);



                        //GameObject INFOChild = transform.FindChild("Info").gameObject;
                        //INFOChild.transform.localPosition = new Vector3(0f, -7.0f, 0f);
                        //GameObject ONLYChild = transform.FindChild("Only").gameObject;
                        //ONLYChild.transform.localPosition = new Vector3(0f, 7.0f, 0f);


                        if (Sfadein == true && Sfadeout == true && Sfadeend == false)
                        {
                            Sfade = true;
                            Sfadein = false;
                            Sfadeend = true;

                            Only = true;
                        }
                        else
                        {
                            Sfade = false;
                            Only = true;

                        }

                        //Sil.SetActive(false);
                        //Sfade = true;
                    }
                }
                else
                {
                    CHANGEMeshOnly(false);
                    CHANGENOTMeshOnly(true);
                    //OnlyMesh.SetActive(false);
                    //NOTOnlyMesh.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Input.ResetInputAxes();
                        //Close2Play = false;
                        //GameObject CradleSIL = transform.Find ("CradleSilheutee").gameObject;
                        //Sil.GetComponent<Renderer> ().enabled = true;
                        //CM.BroadcastMessage("InitJump", true);
                        //bool CMJI = Camera.main.GetComponent<CamPublic>().JumpIn;
                        // CMJI = true;

                        // transform.position = Vector3.Lerp(pos, INITpos, 1f);
                        Sil.transform.localPosition = new Vector3(-92.09999f, -6.361397f, 72.70006f);
                        //SendMessage("POSinit");

                        SC_CC.SetActive(false);
                        CloseCS.SetActive(true);


                        //foreach (GameObject ResetScript in ResetScript) {

                        //	ResetScript.SetActive (true);
                        //}
                        //GameObject CradleSub = transform.FindChild("CradleGUI/CradleSubGP").gameObject;
                        SCSub.GetComponent<GUIZoom2>().POSinit = true;
                        //GameObject CM = CameraGP.transform.FindChild("Main Camera").gameObject;
                        CM.GetComponent<CamPublic>().JumpIn = true;

                        // Input.ResetInputAxes();
                        Vector3 CamPos = CameraGP.transform.position;
                        CamPos.x = -43.9f;
                        CameraGP.transform.position = CamPos;

                        //GameObject CloseChild = SCSub.transform.Find ("CloseSignLoc").gameObject;
                        //GameObject CloseChildSprite = CloseChild.transform.Find ("CloseCradle").gameObject;
                        //GameObject PlayChild = CloseChild.transform.Find ("CradlePlay").gameObject;
                        //CloseChildSprite.SetActive (true);
                        //PlayChild.SetActive (false);

                        GameObject childGO = ADDONS.transform.Find("GradCap_GP").gameObject;
                        childGO.SetActive(true);
                        GameObject childGO2 = ADDONS.transform.Find("CradleNew_GP").gameObject;
                        childGO2.SetActive(true);

                        MS_GP.SetActive(true);
                        GameObject MSGUI = MS_GP.transform.Find("MS_INFO_GUI").gameObject;
                        GameObject MSSUBGP = MS_GP.transform.Find("MS_INFO_GUI/MS_GUI_SubGP").gameObject;
                        MSGUI.GetComponent<OBJOnCam>().enabled = true;
                        MSSUBGP.GetComponent<GuiZoom>().enabled = true;


                        GameObject REND_Line = MSSUBGP.transform.Find("MS_All_locator/REND_LINE_GP").gameObject;

                        REND_Line.GetComponent<Line_render>().enabled = true;



                        Only = false;
                    }
                }
            }
            else if (hit.transform.name == "CollideSilCase")
            {
                Highlight(0);

                if (!Only)
                {
                    ZeroingClose();
                    ZeroingInfo();
                    ZeroingMeshOnly();
                    Sil.SetActive(true);
                    ZeroingCOl();
                    //Zeroing();
                    //SilFalse ();
                    //Sil.SetActive(false);
                    //GameObject childGO3 = ADDONS.transform.Find ("CradleNew_GP").gameObject;
                    //GameObject childGO4 = childGO3.transform.Find ("CradleSilheutee").gameObject;


                    //

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {

                        // GetComponent<GUIZoom2>().POSinit = true;
                        // CameraGP.BroadcastMessage("Raycatch", true);

                        Sfade = true;
                        // transform.position = Vector3.Lerp(pos, INITpos, 1f);
                        //SendMessage("POSinit");



                        if (Sfadein == true && Sfadeout == true && Sfadeend == false)
                        {
                            Sfadein = false;
                            Sfadeend = true;
                        }
                        else
                        {

                            Sfadein = false;
                            Sfadeout = false;
                            Sfadeend = false;
                        }


                    }
                }
                else
                {
                    Sil2.SetActive(true);
                    ZeroingClose();
                    ZeroingInfo();
                    ZeroingMeshOnly();
                    ZeroingCOl();
                    //Zeroing();
                    //SilFalse ();
                    //Sil.SetActive(false);
                    //GameObject childGO3 = ADDONS.transform.Find ("CradleNew_GP").gameObject;
                    //GameObject CradleSil2 = childGO3.transform.Find ("CradleSilheuteeOnly").gameObject;
                    //CradleSil2.SetActive (true); // подсветка второго слуэта когда Only

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        // print("INITpos");
                        //transform.position = INITpos;
                        // GameObject CradleSub = transform.FindChild("CradleGUI/CradleSubGP").gameObject;
                        SCSub.GetComponent<GUIZoom2>().POSinit = true;
                        //gameObject.GetComponent<GUIZoom2>().POSinit = true;
                        //GameObject CM = CameraGP.transform.FindChild("Main Camera").gameObject;
                        // CM.BroadcastMessage("InitJump", true);
                        //GameObject CM = CameraGP.transform.FindChild("Main Camera").gameObject;
                        CM.GetComponent<CamPublic>().JumpIn = true;


                    }

                }
            }
            else if (hit.transform.name == "CollideMS" && Input.GetKeyDown(KeyCode.Mouse0))
            {
                //transform.position = INITpos;
                //SendMessage("POSinit");
                //GameObject CradleSub = transform.FindChild("CradleGUI/CradleSubGP").gameObject;
                SCSub.GetComponent<GUIZoom2>().POSinit = true;
                //gameObject.GetComponent<GUIZoom2>().POSinit = true;
                Highlight(0);
                ZeroingClose();
                ZeroingInfo();
                ZeroingMeshOnly();
                ZeroingCOl();
                //CameraGP.BroadcastMessage("Raycatch", true);
                CameraGP.GetComponent<Move>().RayCatchOn = true;
            }
            else if (hit.transform.tag != "NFOsigns")
            {
                Highlight(0);
                ZeroingClose();
                ZeroingInfo();
                ZeroingMeshOnly();
                CHANGEClose(false);
                //CHANGEPlay (false);
                CHANGEShowInfo(false);
                CHANGECloseInfo(false);
                CHANGEMeshOnly(false);
                CHANGENOTMeshOnly(false);
                Sil.SetActive(false);
                Sil2.SetActive(false);
                ZeroingCOl();
            }
        }
        //else if (hit.transform.name != "CradleNew_GP" || hit.transform.name != "Only" || hit.transform.name != "Info" || hit.transform.name != "CloseSignLoc")
        //{

        // }

        else
        {
            // Zeroing();
            ZeroingClose();
            ZeroingInfo();
            ZeroingMeshOnly();
            CHANGEClose(false);
            ZeroingCOl();
            //CHANGEPlay (false);
            CHANGEShowInfo(false);
            CHANGECloseInfo(false);
            CHANGEMeshOnly(false);
            CHANGENOTMeshOnly(false);
            Sil.SetActive(false);
            Sil2.SetActive(false);
            GameObject childGO3 = ADDONS.transform.Find("CradleNew_GP").gameObject;
            GameObject CradleSil2 = childGO3.transform.Find("CradleSilheuteeOnly").gameObject;
            CradleSil2.SetActive(false);
            Highlight(0);
            //ShowInfo.SetActive(false);
            //CloseInfo.SetActive(false);
            //CloseMesh.SetActive(false);
            //OnlyMesh.SetActive(false);
            //NOTOnlyMesh.SetActive(false);
            // Canvas.SetActive(false);

        }

    }



    //void SilFalse ()
    //{
    void MoveSilcase()
    {
        GameObject GCCaseGP = ADDONS.transform.Find("GradCap_GP").gameObject;
        GameObject GClTrans = GCCaseGP.transform.Find("GC_SubGp/GradCapLP_trans").gameObject;
        if (Sfade) // условие запуска
        {
            Input.ResetInputAxes();
            Color tmp = SCfade[1].GetComponent<Renderer>().material.color;
            //if (tmp.a ==0f) {scrun = true;}
            // scrun - int count of SSfade 
            // scrun =0 on start (SSfade up)
            // scrun = 1 after SSfade down
            // scrun = 2 after SSfade up
            // scrun = 3 after SSfade down again



            //} else if (tmp.a == 0.717f) {
            //	if (scrun == 1) {
            //		scrun = 2;
            //	} else if (scrun == 3) {
            //		scrun = 0;
            //	}
            //


            //else if (tmp.a =0f && scrun ==2) {scrun=3;}
            //else if (tmp.a =0.717f && scrun ==3) {scrun=0;}

            if (Sfadein == false && Sfadeout == false && Sfadeend == false)
            {
                if (tmp.a >= 0.01f)
                { // SSfade to 0 alpha 

                    tmp.a -= Time.deltaTime * 2.7f;
                }
                else
                {
                    tmp.a = 0f;
                    Sfadein = true;
                    GameObject SClowPoly = transform.Find("SilCaseSubGp/SilCaseLP").gameObject;

                    SClowPoly.transform.localPosition = new Vector3(-92.09999f, -6.361397f, 72.70006f);
                    GameObject SISILVIRT = transform.Find("SilCaseSubGp/SilCaseLP_trans").gameObject;
                    SISILVIRT.SetActive(true);
                    //SISIL.transform.localPosition = new Vector3 (-26.6f, 0f, 0f);
                    // GameObject childGO = SilCaseGP.transform.FindChild("SilCaseSilheutee").gameObject;

                    Sil.transform.localPosition = new Vector3(1.199997f, -6.361397f, 72.70003f);
                    if (GClTrans.activeSelf)
                    {
                        GCCaseGP.GetComponent<GradCapGUI>().Gfade = true;
                        GCCaseGP.GetComponent<GradCapGUI>().Gfadein = false;
                        GCCaseGP.GetComponent<GradCapGUI>().Gfadeout = true;
                        GCCaseGP.GetComponent<GradCapGUI>().Gfadeend = true;
                    }

                }
            }
            else if (Sfadein == true && Sfadeout == false && Sfadeend == false)
            {
                if (tmp.a < alpha)
                {
                    tmp.a += Time.deltaTime * 2.7f;
                }
                else
                {
                    tmp.a = alpha;
                    Sfadeout = true;
                    Sfade = false;
                }
            }
            else if (Sfadein == false && Sfadeout == true && Sfadeend == true)
            {
                if (tmp.a >= 0.01f)
                { // SSfade to 0 alpha 

                    tmp.a -= Time.deltaTime * 2.7f;
                }
                else
                {
                    tmp.a = 0f;
                    Sfadein = true;
                    Sfadeout = false;
                    //Sfadein = true;
                    GameObject SISILVIRT = transform.Find("SilCaseSubGp/SilCaseLP_trans").gameObject;
                    SISILVIRT.SetActive(false);
                    //SISIL.transform.localPosition = new Vector3 (-119.9f, 0f, 0f);
                    //GameObject childGO = SilCaseGP.transform.FindChild("SilCaseSilheutee").gameObject;

                    Sil.transform.localPosition = new Vector3(-92.09999f, -6.361397f, 72.70006f);


                    GameObject SClowPoly = transform.Find("SilCaseSubGp/SilCaseLP").gameObject;
                    SClowPoly.transform.localPosition = new Vector3(1.199997f, -6.361397f, 72.70003f);

                }
            }
            else if (Sfadein == true && Sfadeout == false && Sfadeend == true)
            {
                if (tmp.a < alpha)
                {
                    tmp.a += Time.deltaTime * 2.7f;
                }
                else
                {
                    tmp.a = alpha;
                    Sfadeout = false;
                    Sfade = false;
                    Sfadeend = false;
                    Sfade = false;
                }
            }

            foreach (GameObject SCfade in SCfade)
            {
                SCfade.GetComponent<Renderer>().material.color = tmp;
            }



        }
    }


    //}
    void ChangeCol(int CC)
    {

        Color SCcolor = SilCaseLP.GetComponent<Renderer>().material.color;
        //print(CC);
        if (CC == 1)
        {

            SCchangecolor[0].SetActive(true);
            SCchangecolor[1].SetActive(false);
            SCchangecolor[2].SetActive(false);
            SilCaseINT.SetActive(true);
            alpha = 0.717f;
            SCcolor = new Color(1f, 1f, 0.957f, alpha);

        }
        else if (CC == 2)
        {
            SCchangecolor[0].SetActive(false);
            SCchangecolor[1].SetActive(true);
            SCchangecolor[2].SetActive(false);
            SilCaseINT.SetActive(false);
            alpha = 1f;
            SCcolor = new Color(0.1f, 0.1f, 0.1f, alpha);
            // SCcolor.a = 1;

        }
        else if (CC == 3)
        {
            alpha = 1f;
            SCchangecolor[0].SetActive(false);
            SCchangecolor[1].SetActive(false);
            SCchangecolor[2].SetActive(true);
            SilCaseINT.SetActive(false);
            SCcolor = new Color(1f, 0.5f, 0.165f, alpha);
        }


        SilCaseLP.GetComponent<Renderer>().material.color = SCcolor;
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

    void ZeroingClose()
    {
        foreach (GameObject CloseMesh in CloseMesh)
        {
            CloseMesh.transform.localPosition = new Vector3(0, 0, 0);
            CloseMesh.SendMessage("Zeroing", true);
        }

    }

    void ZeroingMeshOnly()
    {
        foreach (GameObject OnlyMesh in OnlyMesh)
        {
            OnlyMesh.transform.localPosition = new Vector3(0, 0, 0);
            OnlyMesh.SendMessage("Zeroing", true);
        }
        foreach (GameObject NOTOnlyMesh in NOTOnlyMesh)
        {
            NOTOnlyMesh.transform.localPosition = new Vector3(0, 0, 0);
            NOTOnlyMesh.SendMessage("Zeroing", true);
        }
    }

    void CHANGEShowInfo(bool state)
    {
        foreach (GameObject ShowInfo in ShowInfo)
        {
            ShowInfo.transform.GetComponent<Renderer>().enabled = state;
        }
    }

    void CHANGECloseInfo(bool state)
    {
        foreach (GameObject CloseInfo in CloseInfo)
        {
            CloseInfo.transform.GetComponent<Renderer>().enabled = state;
        }
    }

    void CHANGEClose(bool state)
    {
        foreach (GameObject CloseMesh in CloseMesh)
        {
            CloseMesh.transform.GetComponent<Renderer>().enabled = state;
        }
    }



    void CHANGEMeshOnly(bool state)
    {
        foreach (GameObject OnlyMesh in OnlyMesh)
        {
            OnlyMesh.transform.GetComponent<Renderer>().enabled = state;
        }
    }

    void CHANGENOTMeshOnly(bool state)
    {
        foreach (GameObject NOTOnlyMesh in NOTOnlyMesh)
        {
            NOTOnlyMesh.transform.GetComponent<Renderer>().enabled = state;
        }
    }


    void Highlight(int num) // подсветка кнопок
    {
        // GameObject CloseCS = transform.Find("CradleGUI/CradleSubGP/CloseSignLoc/CloseCradle").gameObject;
        // GameObject ClosePl = transform.Find("CradleGUI/CradleSubGP/CloseSignLoc/CradlePlay").gameObject;


        Color tmp1 = CloseCS.GetComponent<SpriteRenderer>().material.color;


        Color tmp2 = SCchangecolor[0].GetComponent<SpriteRenderer>().material.color;




        GameObject CloseInf = transform.Find("SilCaseGUI/SilCaseGUISubGp/SC_Info/i-sign_SilCase 1").gameObject;
        GameObject OnlySig = transform.Find("SilCaseGUI/SilCaseGUISubGp/SC_Only/i-sign_SilCase 2").gameObject;
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

        CloseCS.GetComponent<SpriteRenderer>().material.color = tmp1;
        if (SCchangecolor[0].activeSelf)
        {
            SCchangecolor[0].GetComponent<SpriteRenderer>().material.color = tmp2;
        }
        else if (SCchangecolor[1].activeSelf)
        {
            SCchangecolor[1].GetComponent<SpriteRenderer>().material.color = tmp2;
        }
        else if (SCchangecolor[2].activeSelf)
        {
            SCchangecolor[2].GetComponent<SpriteRenderer>().material.color = tmp2;
        }


        CloseInf.GetComponent<SpriteRenderer>().material.color = tmp3;
        OnlySig.GetComponent<SpriteRenderer>().material.color = tmp4;
    }
    void ZeroingCOl()
    {
        
        //TXT_NFO = GameObject.FindGameObjectsWithTag ("TXTchild");

        foreach (GameObject COL_NFO in COL_NFO)
        {
            //if (COL_NFO.gameObject.GetComponent<SpriteRenderer>() != null) {
            COL_NFO.transform.GetComponent<SpriteRenderer>().enabled = false;
            COL_NFO.transform.localPosition = new Vector3(0, 0, 0);
            COL_NFO.transform.GetComponent<ChangeColor2>().enabled = true;
            COL_NFO.transform.GetComponent<ChangeColor2>().togA = true;
            COL_NFO.transform.GetComponent<ChangeColor2>().alpha = 0f;
            COL_NFO.transform.GetComponent<ChangeColor2>().Twotimes = 0;
            //}
            //else if (COL_NFO.gameObject.GetComponent<Renderer>() != null) {

            //}
        }


    }
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
