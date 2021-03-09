using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapShotCamera : MonoBehaviour
{
    public Camera cam;


    int resWidth = 256;
    int resHeight = 256;



    // Start is called before the first frame update
    void Start()
    {
        if (cam.targetTexture != null)
        {
            resWidth = cam.targetTexture.width;
            resHeight = cam.targetTexture.height;
        }

        cam.gameObject.SetActive(false);
          

    }

    public void CallTakeSnapShot()
    {
        cam.gameObject.SetActive(true);
    }

    private void LateUpdate()
    {

        if (cam.gameObject.activeInHierarchy)
        {

            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
            cam.Render();

            RenderTexture.active = cam.activeTexture;

            snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);

            byte[] bytes = snapshot.EncodeToPNG();

            string fileName = SnapShotName();

            System.IO.File.WriteAllBytes(fileName, bytes);

            Debug.Log("SnapShot Taken");

            cam.gameObject.SetActive(false);

        }
        
    }

    private string SnapShotName()
    {
        return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.png",
            Application.dataPath,
            resWidth,
            resHeight,
            System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
    }
}
