using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public Button button;
    public AudioSource audioSource;
    public float fadeDuration = 1f; // Durasi fading dari 50% ke 100%

    void Start()
    {
        // Membuat tombol non-interaktif saat awal
        button.interactable = false;

        

        // Memulai pemutaran audio dan mengaktifkan tombol setelah audio selesai
        StartCoroutine(EnableButtonAfterAudio());
    }

    IEnumerator EnableButtonAfterAudio()
    {
        // Memainkan audio
        audioSource.Play();

  

        // Menunggu hingga audio selesai
        yield return new WaitForSeconds(audioSource.clip.length);

        // Mengembalikan opacity dan mengaktifkan tombol setelah audio selesai
        
        button.interactable = true;
    }
}