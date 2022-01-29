using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainmenucontroller : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene("game");
    }  
    public void restartlevels()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
