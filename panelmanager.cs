using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class panelmanager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject menuPanel1;
    public GameObject menuPanel2;
    public Button infoButton;
    public Button nextButton1;
    public Button nextButton2;
    public GameObject imagePanel;
    public Button viewImageButton;

    private bool isInfoOpen = false;

    void Start()
    {
        // Sembunyikan panel-panel kecuali "Menu Panel" pada awalnya
        menuPanel.SetActive(false);
        menuPanel1.SetActive(false);
        menuPanel2.SetActive(false);

        // Menambahkan fungsi onClick ke tombol "Lihat Gambar"
        viewImageButton.onClick.AddListener(ShowImagePanel);
    }

    public void ToggleInfoPanel()
    {
        if (!isInfoOpen)
        {
            // Tampilkan "Menu Panel" dan non-interactablekan tombol Info
            menuPanel.SetActive(true);
            infoButton.interactable = false;
            viewImageButton.interactable = false; // Menonaktifkan tombol Lihat Gambar
            isInfoOpen = true;
        }
        else
        {
            // Tutup "Menu Panel" dan membuat tombol Info interaktif lagi
            menuPanel.SetActive(false);
            infoButton.interactable = true;
            viewImageButton.interactable = true; // Mengaktifkan kembali tombol Lihat Gambar
            isInfoOpen = false;
        }
    }


    public void OpenNextPanel1()
    {
        // Tutup panel saat ini dan buka panel kedua
        menuPanel.SetActive(false);
        menuPanel1.SetActive(true);

        // Non-interactablekan tombol Info di panel kedua
        infoButton.interactable = false;
    }

   public void OpenNextPanel2()
{
    // Tutup panel kedua dan buka panel ketiga
    menuPanel1.SetActive(false);
    menuPanel2.SetActive(true);

    // Membuat tombol Info interaktif lagi
    infoButton.interactable = true;
    // Mengubah teks tombol Info menjadi "Tutup"
    infoButton.GetComponentInChildren<Text>().text = "Tutup";

    // Menonaktifkan tombol Lihat Gambar
    viewImageButton.interactable = false;
}


    public void ClosePanel2()
    {
        // Tutup panel ketiga
        menuPanel2.SetActive(false);

        // Mengubah teks tombol Info menjadi "Info"
        infoButton.GetComponentInChildren<Text>().text = "Info";
    }
    public void ShowImagePanel()
    {
        // Toggle tampilan panel gambar
        imagePanel.SetActive(!imagePanel.activeSelf);

        // Update teks tombol "Lihat Gambar" sesuai dengan status panel gambar
        if (imagePanel.activeSelf)
        {
            // Panel gambar terbuka, ubah teks tombol menjadi "Tutup Gambar"
            viewImageButton.GetComponentInChildren<Text>().text = "Tutup Gambar";
        }
        else
        {
            // Panel gambar ditutup, ubah teks tombol menjadi "Lihat Gambar"
            viewImageButton.GetComponentInChildren<Text>().text = "Lihat Gambar";
        }
    }



}