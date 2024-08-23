using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class TextAnimation : MonoBehaviour
{
    public Text textComponent;
    public Text continueText; // Tambahkan Text untuk "Tekan untuk Lanjut"
    public float typingSpeed = 0.1f;

    private string fullText;
    private string currentText = "";

    void Start()
    {
        fullText = textComponent.text;
        textComponent.text = "";
        continueText.gameObject.SetActive(false); // Sembunyikan teks "Tekan untuk Lanjut" saat mulai
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Tampilkan teks "Tekan untuk Lanjut" setelah teks selesai ditampilkan
        continueText.gameObject.SetActive(true);
    }

    public void FinishTextAnimation()
    {
        // Menyelesaikan animasi teks
        StopAllCoroutines();
        textComponent.text = fullText;
        continueText.gameObject.SetActive(true); // Tampilkan teks "Tekan untuk Lanjut" ketika animasi selesai
    }
}
