using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
//using System.Collections;


public class MainMenu : MonoBehaviour
{
    private int numOfSkys = 3; // total num of skys used
    private int currentSky = 1;
    private int skyCounter = 1;
    // public Light sunLight;

    public Material skyBox01; // HDR image for skybox that does the lighting
    public Cubemap skyRef01; // HDR image for reflections
                             //public float skyExposure01 = 1.0f; // Exposure of HDR used for lighting and GI
                             // public Vector3 sunDirection01 = Vector3.zero; // Directional Light (Sun) angle
                             // public float sunIntensity01 = 1.0f; // Intensity of directional light (sun)
                             //public Color sunColor01 = Color.white; // Color of directional light (sun)

    public Material skyBox02;
    public Cubemap skyRef02;
    public Material skyBox03;
    public Cubemap skyRef03;

    Ray ray;
    RaycastHit hit;
    public Color EmissiveCol;
    private GameObject MenuCube;
    private GameObject InfoCube;
    private GameObject HelpSprite;
    private GameObject HDRIsprite;
    private GameObject SoundSprite;
    private GameObject AboutSprite;
    private GameObject HelpGp;
    private GameObject HDRIGp;
    private GameObject AboutGp;
    private bool NFOmeshCHK;
    private GameObject[] NFOSprites;
    // Use this for initialization
    void Start ()
    {
        MenuCube = transform.Find("MenuCube").gameObject;
        InfoCube = transform.Find("InfoCube/InfoMesh").gameObject;
        NFOmeshCHK = false;
        NFOSprites = GameObject.FindGameObjectsWithTag("MainNFOSprites");
        HelpSprite = MenuCube.transform.Find("HelpTextPic").gameObject;
        HDRIsprite = MenuCube.transform.Find("HDRITextPic").gameObject;
        SoundSprite = MenuCube.transform.Find("SoundSetTextPic").gameObject;
        AboutSprite = MenuCube.transform.Find("AboutTextPic").gameObject;

        HelpGp = MenuCube.transform.Find("Help_GP").gameObject;
        HDRIGp = MenuCube.transform.Find("HDRI_GP").gameObject;
        AboutGp = MenuCube.transform.Find("About_GP").gameObject;

        RenderSettings.skybox = skyBox01;
        RenderSettings.customReflection = skyRef01;
        //RenderSettings.skybox.SetFloat("_Exposure", skyExposure01);
        // sunLight.transform.eulerAngles = sunDirection01;
        // sunLight.intensity = sunIntensity01;
        //sunLight.color = sunColor01;
        skyCounter = 1;
        currentSky = skyCounter;
        DynamicGI.UpdateEnvironment();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            skyCounter += 1;
            if (skyCounter > numOfSkys) { skyCounter = 1; }
            //DynamicGI.UpdateEnvironment();
        }
        if (skyCounter != currentSky)
        {
            switch (skyCounter)
            {
                case 1:
                    RenderSettings.skybox = skyBox01;
                    RenderSettings.customReflection = skyRef01;
                    // RenderSettings.skybox.SetFloat("_Exposure", skyExposure01);
                    //sunLight.transform.eulerAngles = sunDirection01;
                    // sunLight.intensity = sunIntensity01;
                    // sunLight.color = sunColor01;
                    DynamicGI.UpdateEnvironment();
                    break;

                case 2:
                    RenderSettings.skybox = skyBox02;
                    RenderSettings.customReflection = skyRef02;
                    //RenderSettings.skybox.SetFloat("_Exposure", skyExposure02);
                    //sunLight.transform.eulerAngles = sunDirection02;
                    // sunLight.intensity = sunIntensity02;
                    // sunLight.color = sunColor02;
                    DynamicGI.UpdateEnvironment();
                    break;
                case 3:
                    RenderSettings.skybox = skyBox03;
                    RenderSettings.customReflection = skyRef03;
                    //RenderSettings.skybox.SetFloat("_Exposure", skyExposure02);
                    //sunLight.transform.eulerAngles = sunDirection02;
                    // sunLight.intensity = sunIntensity02;
                    // sunLight.color = sunColor02;
                    DynamicGI.UpdateEnvironment();
                    break;

                default:
                    RenderSettings.skybox = skyBox01;
                    RenderSettings.customReflection = skyRef01;

                    // RenderSettings.skybox.SetFloat("_Exposure", skyExposure01);
                    //sunLight.transform.eulerAngles = sunDirection01;
                    // sunLight.intensity = sunIntensity01;
                    // sunLight.color = sunColor01;
                    DynamicGI.UpdateEnvironment();
                    break;
            }
            currentSky = skyCounter;
        }
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
                if (hit.transform.name == "InfoMesh" )
                {
                Color tmp = InfoCube.GetComponent<Renderer>().material.color;
                tmp.a = 1f;
                InfoCube.GetComponent<Renderer>().material.color = tmp;
                NFOSpritesChangeAlpha(1f);

                

                if (Input.GetKeyDown(KeyCode.Mouse0))
                    { 
                              
                    NFOmeshCHK = !NFOmeshCHK;
                    //print(NFOmeshCHK);
                    if (NFOmeshCHK)
                         { 
                    MenuCube.SetActive(true);
                         }
                    else
                        {
                    MenuCube.SetActive(false);
                        }
                    }
                }
                else if (hit.transform.name == "HelpQuad" && Input.GetKeyDown(KeyCode.Mouse0))
            {
                MenuFlops(HelpSprite);
                HelpGp.SetActive(true);
                HDRIGp.SetActive(false);
                AboutGp.SetActive(false);
            }
            else if (hit.transform.name == "HDRIQuad" && Input.GetKeyDown(KeyCode.Mouse0))
            {
                MenuFlops(HDRIsprite);
                HelpGp.SetActive(false);
                HDRIGp.SetActive(true);
                AboutGp.SetActive(false);
            }
            else if (hit.transform.name == "SoundQuad" && Input.GetKeyDown(KeyCode.Mouse0))
            {
                MenuFlops(SoundSprite);
                HelpGp.SetActive(false);
                HDRIGp.SetActive(false);
                AboutGp.SetActive(false);
            }
            else if (hit.transform.name == "AboutQuad" && Input.GetKeyDown(KeyCode.Mouse0))
            {
                MenuFlops(AboutSprite);
                HelpGp.SetActive(false);
                HDRIGp.SetActive(false);
                AboutGp.SetActive(true);
            }
        }
        else
        {
            Color tmp = InfoCube.GetComponent<Renderer>().material.color;
            tmp.a = 0.3f;
            InfoCube.GetComponent<Renderer>().material.color = tmp;
            NFOSpritesChangeAlpha(0.3f);
   

        }



    }
    void NFOSpritesChangeAlpha(float alpha)
    {
        foreach (GameObject NFOSprites in NFOSprites)
        {
            Color tmp = NFOSprites.GetComponent<SpriteRenderer>().material.color; 
            tmp.a = alpha;
            NFOSprites.GetComponent<SpriteRenderer>().material.color = tmp;
        }
     
    }
    void MenuFlops(GameObject Emmit)
    {
        Material mat = Emmit.GetComponent<SpriteRenderer>().material;
        Color alphatemp = mat.color;
        alphatemp.a = 1f;
        mat.SetColor("_EmissionColor", EmissiveCol);
        mat.SetColor("_Color", alphatemp);
        GameObject[] NotEmm;
        NotEmm = new GameObject[3];
       // print(Emmit.name);

        if (Emmit.name == "HelpTextPic")
        {
            NotEmm[0] = HDRIsprite;
            NotEmm[1] = SoundSprite;
            NotEmm[2] = AboutSprite;



        }
        else if (Emmit.name == "HDRITextPic")
        {

            NotEmm[0] = HelpSprite;
            NotEmm[1] = SoundSprite;
            NotEmm[2] = AboutSprite;

        }
        else if (Emmit.name == "SoundSetTextPic")
        {

            NotEmm[0] = HelpSprite;
            NotEmm[1] = HDRIsprite;
            NotEmm[2] = AboutSprite;

        }
        else if (Emmit.name == "AboutTextPic")
        {

            NotEmm[0] = HelpSprite;
            NotEmm[1] = HDRIsprite;
            NotEmm[2] = SoundSprite;
        }

        foreach (GameObject NotEmms in NotEmm)
        {
            Material matNE = NotEmms.GetComponent<SpriteRenderer>().material;
            Color alphatempNE = matNE.color;
            Color emcolorNE = new Color(0.249f, 0.249f, 0.249f, 1f);
            alphatempNE.a = 0.575f;
            matNE.SetColor("_EmissionColor", emcolorNE);
            matNE.SetColor("_Color", alphatempNE);
            
        }
    }
    public void HDRIchange (int skynum)
    {
        skyCounter = skynum;
    }
    public void SendMailIGM()
    {
        Process.Start("mailto:sergeevsv@igm.spb.ru");
    }
    public void SendMailAuthor()
    {
        Process.Start("mailto:stas.v.sergeev@gmail.com");
    }
}
