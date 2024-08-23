using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
   public void LinkSupportedDevice()
    {
        Application.OpenURL("https://developers.google.com/ar/devices?hl=id#google_play");
    }
}
