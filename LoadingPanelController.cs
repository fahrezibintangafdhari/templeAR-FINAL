using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanelController : MonoBehaviour
{
    public GameObject loadingPanel; // Mendeklarasikan variabel publik loadingPanel bertipe GameObject

    void Start()
    {
        Invoke("CloseLoadingPanel", 2f); // Memanggil fungsi untuk menutup panel loading setelah 2 detik
    }

    void CloseLoadingPanel()
    {
        
        loadingPanel.SetActive(false); // Menonaktifkan panel loading

    }
}