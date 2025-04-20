using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCameraBinder : MonoBehaviour
{
  void Start()
    {
        var canvas = GetComponent<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera == null)
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                canvas.worldCamera = mainCam;
            }
        }
    }
}
