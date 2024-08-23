using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusikControl : MonoBehaviour
{
    public static MusikControl Instance { get; set; }

    public AudioClip[] clipMusik;
    public AudioSource audioMusik;

    private float volume = 1.0f; // Default volume

    public float Volume
    {
        get { return volume; }
        set
        {
            volume = Mathf.Clamp01(value); // Ensure volume is between 0 and 1
            audioMusik.volume = volume;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMusik(int indexMusik)
    {
        if (audioMusik.clip != clipMusik[indexMusik])
        {
            audioMusik.Stop(); //stop musik
            audioMusik.clip = clipMusik[indexMusik]; //ganti clip audio
            audioMusik.Play(); //mainkan musik
        }
    }
}
