using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{
    public void OnBackButtonClicked()
    {
        // Simpan state di PlayerPrefs bahwa tombol kembali telah diklik
        PlayerPrefs.SetInt("MiniGamesUnlocked", 1);
        PlayerPrefs.Save();

        // Log untuk memastikan PlayerPrefs disimpan
        Debug.Log("MiniGamesUnlocked set to 1");

        // Pindah ke scene Main Menu
        SceneManager.LoadScene("Pilih AR");
    }
}
