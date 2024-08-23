using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVolumeController : MonoBehaviour
{
    public float targetVolume = 1.0f; // Target volume
    public float transitionSpeed = 0.5f; // Kecepatan transisi

    private bool isTransitioning = false; // Apakah sedang dalam proses transisi

    void Start()
    {
        // Cek apakah ada instance dari MusikControl
        if (MusikControl.Instance != null)
        {
            // Cek apakah perlu melakukan transisi
            if (!Mathf.Approximately(MusikControl.Instance.Volume, targetVolume))
            {
                StartCoroutine(TransitionVolume()); // Mulai transisi volume
            }
        }
    }

    IEnumerator TransitionVolume()
    {
        isTransitioning = true;

        float startVolume = MusikControl.Instance.Volume; // Volume saat ini
        float timeElapsed = 0f;

        while (timeElapsed < transitionSpeed)
        {
            float t = timeElapsed / transitionSpeed;
            MusikControl.Instance.Volume = Mathf.Lerp(startVolume, targetVolume, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        MusikControl.Instance.Volume = targetVolume; // Pastikan volume mencapai target
        isTransitioning = false;
    }
}
