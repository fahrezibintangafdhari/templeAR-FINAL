using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class prolog_script : MonoBehaviour
{
    public Text teksnya;
    public string[] dialog;
    private float textspeed = 0.06f;
    private int indexDialog;
    void Start()
    {
        StartDialog();
    }

    void StartDialog()
    {

        indexDialog = 0;
        StartCoroutine(Tulisan());
    }

    IEnumerator Tulisan()
    {
        foreach(char c in dialog[indexDialog].ToCharArray())
        {
            teksnya.text += c;
            yield return new WaitForSeconds(textspeed);
        }

    }

     void dialogSelanjutnya()
    {
        if (indexDialog < dialog.Length-1)
        {
            indexDialog++;
            teksnya.text = string.Empty;
            StartCoroutine(Tulisan());
        }
        else
        {
            SceneManager.LoadScene("Menu Level");
        }
    }
    public void LanjutkanButton()
    {
        if (teksnya.text == dialog[indexDialog])
        {
            dialogSelanjutnya();
           
        }
        else
        {
            StopAllCoroutines();
            teksnya.text = dialog[indexDialog];
        }
    }
}
