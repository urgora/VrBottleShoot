using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class mainmenucontroller : MonoBehaviour
{


    public GameObject mainscreen, exitpanel, levelpanel, optionpanel,backbutton,demolevelindicator;
    public TextMeshProUGUI currentpanel,playerprefscheck;
    public void play()
    {
        SceneManager.LoadScene("game");
    }  
    public void restartlevels()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void quit()
    {
        flurryinstance.instance.levelstatus("App_Close");
        Application.Quit();
    }
    private void Update()
    {
        if(mainscreen.activeSelf)
        {
            currentpanel.text = "PLAYER";
            backbutton.SetActive(false);
        }
           
        if (exitpanel.activeSelf)
        {
            currentpanel.text = "Exit ";
            backbutton.SetActive(true);
        }

        if (levelpanel.activeSelf)
        {
            currentpanel.text = "LEVEL sELECTION ";
            backbutton.SetActive(true);
        }
        if (optionpanel.activeSelf)
        {
            currentpanel.text = "OPTIONS ";
            backbutton.SetActive(true);
        }


        playerprefscheck.text = PlayerPrefs.GetString("demo");
    }

    public void demolevelunlockinfo()
    {
        demolevelindicator.SetActive(true);
    }
    public void showbuypanel()
    {
      
        demolevelindicator.SetActive(true);
    }

    
}
