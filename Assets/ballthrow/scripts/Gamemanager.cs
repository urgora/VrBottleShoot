using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
   
    public GameObject gameover,levelfaled;
    public GameObject  normal, xrrig;
    public GameObject[] levels;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        int currentlevel = PlayerPrefs.GetInt("level",0);
        Instantiate(levels[currentlevel], transform.position, transform.rotation);
    }
   
    public void Gamedone()
    {
            normal.SetActive(false);
            xrrig.SetActive(true);
            gameover.SetActive(true);
    }
    public void gamefalied()
    {
        normal.SetActive(false);
        xrrig.SetActive(true);
        levelfaled.SetActive(true);
    }
    public void reload()
    {
        int i= PlayerPrefs.GetInt("level");
        if(i<levels.Length-1)
        {
            PlayerPrefs.SetInt("level", i + 1);
        }
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void failedreload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void home()
    {
        SceneManager.LoadScene("main");
    }
}
