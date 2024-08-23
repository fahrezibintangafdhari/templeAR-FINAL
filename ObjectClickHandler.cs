using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClickHandler : MonoBehaviour
{
    public GameObject panel;  // Referensi ke panel
    public AudioSource audioSource;  // Referensi ke audio source
    private Color originalColor;  // Warna asli objek
    private Renderer objectRenderer;  // Renderer objek
    private BoxCollider[] allBoxColliders;  // Referensi ke semua BoxCollider dalam scene
    private bool isInteractable = true;  // Flag untuk mengecek apakah objek bisa ditekan

    void Start()
    {
        // Simpan referensi ke Renderer objek dan warna asli
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;

        // Simpan referensi ke semua BoxCollider dalam scene
        allBoxColliders = FindObjectsOfType<BoxCollider>();

        // Pastikan panel tersembunyi di awal permainan
        if (panel != null)
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel tidak diset di Inspector");
        }

        // Pastikan audio source telah diatur
        if (audioSource == null)
        {
            Debug.LogError("AudioSource tidak diset di Inspector");
        }
    }

    void Update()
    {
        if (!isInteractable)
            return;

        // Cek input sentuhan
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Cek apakah touch phase adalah Began
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Jika ada objek yang disentuh
                if (Physics.Raycast(ray, out hit))
                {
                    // Cek apakah objek yang disentuh adalah objek ini
                    if (hit.transform == transform)
                    {
                        HandleTouch();
                    }
                }
            }
        }

        // Cek input mouse
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Jika ada objek yang diklik
            if (Physics.Raycast(ray, out hit))
            {
                // Cek apakah objek yang diklik adalah objek ini
                if (hit.transform == transform)
                {
                    HandleTouch();
                }
            }
        }
    }

    void HandleTouch()
    {
        // Cek status panel
        bool isPanelActive = panel.activeSelf;

        // Ubah warna objek dan status panel berdasarkan status saat ini
        if (isPanelActive)
        {
            // Panel sedang ditampilkan, sembunyikan panel dan ubah warna menjadi semula
            panel.SetActive(false);
            objectRenderer.material.color = originalColor;
            isInteractable = true;

            // Aktifkan kembali semua BoxCollider
            foreach (BoxCollider collider in allBoxColliders)
            {
                collider.enabled = true;
            }
        }
        else
        {
            // Panel sedang disembunyikan, tampilkan panel dan ubah warna menjadi merah
            panel.SetActive(true);
            objectRenderer.material.color = Color.red;
            isInteractable = false;

            // Nonaktifkan semua BoxCollider
            foreach (BoxCollider collider in allBoxColliders)
            {
                collider.enabled = false;
            }
        }

        // Mainkan audio jika audio source diset
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    // Fungsi untuk menutup panel yang dipanggil oleh tombol X pada panel
    public void ClosePanel()
    {
        panel.SetActive(false);
        objectRenderer.material.color = originalColor;
        isInteractable = true;

        // Aktifkan kembali semua BoxCollider
        foreach (BoxCollider collider in allBoxColliders)
        {
            collider.enabled = true;
        }
    }
}
