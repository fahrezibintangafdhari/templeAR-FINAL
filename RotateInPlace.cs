using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class RotateYWithTwoFingers : MonoBehaviour
{
    // Sensitivitas rotasi
    public float rotationSensitivity = 1.0f;

    // LeanFingerUpdate event handler untuk rotasi
    private void OnEnable()
    {
        LeanTouch.OnFingerUpdate += HandleFingerUpdate;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerUpdate -= HandleFingerUpdate;
    }

    private void HandleFingerUpdate(LeanFinger finger)
    {
        // Hanya lakukan rotasi jika ada dua jari yang menyentuh layar
        if (LeanTouch.Fingers.Count == 2)
        {
            // Hitung delta rotasi
            Vector2 delta = finger.SwipeScreenDelta;
            float rotationY = delta.x * rotationSensitivity;

            // Rotasi objek pada sumbu Y saja
            transform.Rotate(Vector3.up, -rotationY, Space.World);
        }
    }
}
