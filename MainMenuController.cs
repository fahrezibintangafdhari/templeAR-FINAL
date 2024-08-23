using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static string prologSceneName = "Prolog"; // Nama scene prolog
    public static string menuLevelScene = "Pilih AR"; // Nama scene Pilih AR

    void Start()
    {
        Debug.Log("baru pertama kali main");
    }

    // Method baru untuk dipanggil ketika tombol mulai diklik
    public void OnMulaiButtonClick()
    {
        // Membuat instance objek MainMenuController
        MainMenuController mainMenuController = new MainMenuController();

        // Menunda eksekusi fungsi MulaiGame() selama 1 detik
        Invoke("MulaiGame", 1f);
    }

    // Fungsi untuk memulai game
    public void MulaiGame()
    {
        // Cek apakah prolog telah dimainkan sebelumnya
        if (PlayerPrefs.GetInt("PrologPlayed") != 1)
        {
            SceneManager.LoadScene(prologSceneName);
            // Set PrologPlayed ke 1 untuk menandakan prolog telah dimainkan
            PlayerPrefs.SetInt("PrologPlayed", 1);
        }
        else
        {
            SceneManager.LoadScene(menuLevelScene);
        }
    }   
        public Transform masukanLoadingbar; // Deklarasi variabel Transform untuk masukanLoadingbar
        [SerializeField]
        private float nilaiSekarang; // Deklarasi variabel float untuk nilai sekarang (current value)
        [SerializeField]
        private float nilaiKecepatan; // Deklarasi variabel float untuk nilai kecepatan (speed value)
}
