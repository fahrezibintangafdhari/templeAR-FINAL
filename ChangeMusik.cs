using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusik : MonoBehaviour
{

    public int indexMusik;
    // Start dipanggil sebelum pembaruan bingkai pertama
    void Start()
    {
        if (GameObject.Find("Musik") != null)
        {
            MusikControl.Instance.ChangeMusik(indexMusik);

        }
    }

    // Pembaruan dipanggil sekali per frame
    void Update()
    {
        
    }
}
