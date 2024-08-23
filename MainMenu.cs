using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{

    public GameObject panelLevelSelect;
    
    

    

    [Header("Lock Level")]
    public GameObject[] levels;
    public static int currentLevel;
    public static int levelSekarang;
    void Start()
    {
        CheckLevels();

        for (int i = 0; i < levels.Length; i++) 
        {
            InsertLevelSekarang(i);
        } 
    }

 
    void Update()
    {
        
    }

    public void ButtonLoadLevel(int indexLevel)
    {
        SceneManager.LoadScene(indexLevel);
    }



    void InsertLevelSekarang(int index)
    {
        levels[index].GetComponent<Button>().onClick.AddListener(() => LevelSekarang(index));
    }
    public void LevelSekarang(int indexLevel)
    {
        levelSekarang = indexLevel;

        Debug.Log(levelSekarang);
    }
    void CheckLevels()
    {

        currentLevel = PlayerPrefs.GetInt("Level", 0);
        for (int i = 0; i < currentLevel + 1; i++) 
        {
            //unlock level //child 1 itu image (sesuaikan dengan arraynya)
            
            levels[i].GetComponent<Button>().interactable = true; //mengaktifkan fungsi button  
        }
    }

    public void ButtonPlay()
    {
        if (panelLevelSelect.activeInHierarchy == false)
        { 
            panelLevelSelect.SetActive(true);

           
        }
        else
        {
            panelLevelSelect.SetActive(false);
        }
        
    }
}
