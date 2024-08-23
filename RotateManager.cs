using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RotateManager : MonoBehaviour
{
    public float rotationSpeed = 30f; // Kecepatan putaran objek dalam derajat per detik
    private bool isRotating = true; // Untuk mengetahui apakah objek sedang berputar
    private Quaternion initialRotation; // Rotasi awal objek

    public Text buttonText; // Referensi teks tombol

    void Start()
    {
        // Menyimpan rotasi awal objek saat permainan dimulai
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        // Mengecek apakah objek sedang berputar
        if (isRotating)
        {
            // Menghitung jumlah rotasi berdasarkan waktu yang berlalu (dalam detik)
            float rotationAmount = rotationSpeed * Time.deltaTime;

            // Mendapatkan rotasi lokal objek
            Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotationAmount);

            // Menggabungkan rotasi dengan rotasi lokal sebelumnya
            transform.localRotation *= deltaRotation;
        }
    }

    public void ToggleRotation()
    {
        // Mengubah status putaran objek (berhenti atau putar)
        isRotating = !isRotating;

        // Mengubah teks tombol sesuai dengan status putaran
        if (isRotating)
        {
            buttonText.text = "Berhenti";
        }
        else
        {
            // Tombol "Putar" diklik, kembalikan rotasi objek ke rotasi awalnya
            transform.localRotation = initialRotation;
            buttonText.text = "Putar";
        }
    }
}