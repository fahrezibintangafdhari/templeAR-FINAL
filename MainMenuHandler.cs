using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    public Button miniGamesButton; // Referensi ke tombol mini games

    void Start()
    {
        // Periksa apakah mini games telah di-unlock
        if (PlayerPrefs.GetInt("MiniGamesUnlocked", 0) == 1)
        {
            // Aktifkan tombol mini games
            miniGamesButton.interactable = true;
        }
        else
        {
            // Nonaktifkan tombol mini games
            miniGamesButton.interactable = false;
        }
    }
}