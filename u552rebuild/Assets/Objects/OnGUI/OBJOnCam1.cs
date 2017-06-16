using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJOnCam1 : MonoBehaviour {

    private float speed = 2.5f;
    private Vector3 cameraAngles;
    //public Transform target;
    //private float InitCamDist;
   // private float CamDist;
    //private float MagPixMax;
    //public SpriteRenderer checksize;
    //public GameObject CheckSizeObj;
    //private float InitSizeX;

    private void Start()
    {

       
        //InitCamDist = (Camera.main.transform.position - target.position).magnitude;
       // var scale = transform.localScale;
       // SpriteRenderer checksize = CheckSizeObj.GetComponent<SpriteRenderer>();
        
      //  Vector3 SpriteSizeINIT = Camera.main.WorldToScreenPoint(GetComponent<Collider>().bounds.size);
      //  Vector3 SpriteposINIT = Camera.main.WorldToScreenPoint(transform.position);
      //  InitSizeX = (SpriteSizeINIT.x)/ Screen.width;
        //Vector2 sprite_size = CheckSizeObj.GetComponent<SpriteRenderer>().sprite.rect.size;
        //Vector2 local_sprite_size = sprite_size / CheckSizeObj.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        //Vector3 world_size = local_sprite_size;
        //world_size.x *= transform.lossyScale.x;
        //world_size.y *= transform.lossyScale.y;
        //Vector3 screen_size = 0.5f * world_size / Camera.main.orthographicSize;
        //screen_size.y *= Camera.main.aspect;

        //size in pixels
        //Vector3 in_pixels = new Vector3(screen_size.x * Camera.main.pixelWidth, screen_size.y * Camera.main.pixelHeight, 0) * 0.5f;
        // MagPixMax = in_pixels.magnitude;

    }

    // Update is called once per frame
    void LateUpdate () {

        //SpriteRenderer checksize = CheckSizeObj.GetComponent<SpriteRenderer>();
        //CamDist = (Camera.main.transform.position - target.position).magnitude;
        //var scale = transform.localScale;
        //float currentSizeX = checksize.bounds.size.x;
        //Vector2 sprite_size = CheckSizeObj.GetComponent<SpriteRenderer>().sprite.rect.size;
        //Vector2 local_sprite_size = sprite_size / CheckSizeObj.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        //Vector3 world_size = local_sprite_size;
        //world_size.x *= transform.lossyScale.x;
        //world_size.y *= transform.lossyScale.y;
        //Vector3 screen_size = 0.5f * world_size / Camera.main.orthographicSize;
        //screen_size.y *= Camera.main.aspect;
        
        //size in pixels
        //Vector3 in_pixels = new Vector3(screen_size.x * Camera.main.pixelWidth, screen_size.y * Camera.main.pixelHeight, 0) * 0.5f;
        //float MagPix = in_pixels.magnitude;
        
        //Debug.Log(string.Format("World size: {0}, Screen size: {1}, Pixel size: {2}", world_size, screen_size, in_pixels));
        //float currentSizeX = CheckSizeObj.transform.localScale.x / CheckSizeObj.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        //float currentSizeY = checksize.bounds.size.y;
        //Vector3 screenPos = camera.WorldToScreenPoint ()
        //float CurrentSizeX = checksize.bounds.size.x;
        //Vector3 SpriteSizeCur = Camera.main.WorldToScreenPoint(CheckSizeObj.GetComponent<SpriteRenderer>().sprite.bounds.size);
       // Vector3 SpriteposCur = Camera.main.WorldToScreenPoint(transform.position);
       // float CurrentSizeX = SpriteSizeCur.x/ Screen.width;
       // float CurrentSizeY = SpriteSizeCur.y / Screen.height;
       // float CurrentSize = CurrentSizeX * CurrentSizeY;
       // print (CurrentSize);

        //if (CamDist / InitCamDist >= 1)
            // {
            //scale = Vector3.Lerp(scale, scale * CamDist / InitCamDist, 0.01f);
            //scale = Vector3.Lerp (scale (0.01F, 0.01F, 0.01F) * CamDist / InitCamDist;
         //   if (InitSizeX / CurrentSizeX < 1.02f ) {  } 
           // else
           // {
           //     scale = scale;
           // }
       // }
            // else if (CamDist / InitCamDist < currentSizeX / InitSizeX - 0.02)
           //  {
           // scale = Vector3.Lerp(scale, scale * CamDist/ InitCamDist, 0.1f);
           // transform.localScale = scale;

            //scale = scale - new Vector3(0.01F, 0.01F, 0.01F) * CamDist / InitCamDist;
            //    scale = Vector3.Lerp(scale, scale * CamDist / InitCamDist, 1f);
            //     transform.localScale = scale;
            // }
        // else
        // {
        //     scale = new Vector3(1F, 1F, 1F);
        //     transform.localScale = scale;
        //  }
        //transform.localScale = scale;
        //float CtI = currentSizeX;
       // print(CtI);

        cameraAngles = Camera.main.transform.eulerAngles;
         var newRot = Quaternion.Euler(cameraAngles); // get the equivalent quaternion
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 15*Time.deltaTime);
    }
}
