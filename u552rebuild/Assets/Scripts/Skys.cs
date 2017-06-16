using UnityEngine;
using System.Collections;

public class Skys : MonoBehaviour
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
    //public float skyExposure02 = 1.0f;
    //public Vector3 sunDirection02 = Vector3.zero;
    //ublic float sunIntensity02 = 1.0f;
    //public Color sunColor02 = Color.white;

    // make sure skybox 1 is used at startup
    void Start()
    {
        RenderSettings.skybox = skyBox01;
        RenderSettings.customReflection = skyRef01;
        //RenderSettings.skybox.SetFloat("_Exposure", skyExposure01);
       // sunLight.transform.eulerAngles = sunDirection01;
       // sunLight.intensity = sunIntensity01;
        //sunLight.color = sunColor01;
        skyCounter = 1;
        currentSky = skyCounter;
        DynamicGI.UpdateEnvironment();
        //Lightmapping.Bake();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            skyCounter += 1;
            if (skyCounter > numOfSkys) { skyCounter = 1; }
            //DynamicGI.UpdateEnvironment();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            skyCounter -= 1;
            if (skyCounter < 1) { skyCounter = numOfSkys; }
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
    }
    
            
}

