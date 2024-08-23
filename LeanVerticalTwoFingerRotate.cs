using UnityEngine;
using Lean.Touch;

public class LeanTranslateOrRotate : MonoBehaviour
{
    public float translationSpeed = 0.1f;
    public float rotationSpeed = 10f;

    void Update()
    {
        // Periksa apakah tepat satu jari menyentuh layar
        if (LeanTouch.Fingers.Count == 1)
        {
            // Lakukan Lean Drag Translate
            var finger = LeanTouch.Fingers[0];
            if (finger.IsOverGui == false)
            {
                // Ubah delta layar menjadi ruang dunia dan tambahkan ke posisi saat ini
                transform.position += Camera.main.transform.TransformDirection(finger.ScreenDelta.x, finger.ScreenDelta.y, 0) * translationSpeed;
            }
        }
        // Periksa apakah tepat dua jari menyentuh layar
        else if (LeanTouch.Fingers.Count == 2)
        {
            var finger1 = LeanTouch.Fingers[0];
            var finger2 = LeanTouch.Fingers[1];

            // Pastikan kedua jari bergerak
            if (finger1.Down == false && finger2.Down == false)
            {
                // Hitung gerakan horizontal kedua jari
                float horizontalMovement = (finger1.ScreenDelta.x + finger2.ScreenDelta.x) / 2.0f;

                // Memutar objek berdasarkan gerakan horizontal
                float horizontalRotation = horizontalMovement * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, horizontalRotation);

                // Hitung gerakan vertikal kedua jari
                float verticalMovement = (finger1.ScreenDelta.y + finger2.ScreenDelta.y) / 2.0f;

                // Memutar objek berdasarkan gerakan vertikal
                float verticalRotation = verticalMovement * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.right, verticalRotation);
            }
        }
    }
}