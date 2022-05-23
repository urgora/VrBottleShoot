using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButtonPick : MonoBehaviour
{
    public TextMeshProUGUI LevelnoLabel;
    public GameObject completed;
    public Image Lock;
   // public Animation _lock;
    public GameObject[] starImgs,star; 
    public int worldno,levelno;
  //  public Floatvariable _levelNo;
    public AudioClip btnclick,lockaudio;
    public void UIUpdate()
    {
        LevelnoLabel.text = (levelno+1).ToString();
    }
    private void Start()
    {
        int starnumber = PlayerPrefs.GetInt("levelstar" + (levelno+1));
        for (int i = 0; i < 3; i++)
        {
            if (i < starnumber)
            {
                star[i].SetActive(true);
            }
            else
            {
                star[i].SetActive(false);
            }
        }
    }

    public void OnBtnClick( )
    {
        if (Lock.gameObject.activeInHierarchy)
        {
            SoundManager.PlaySFX(lockaudio, false, 0);
          //  _lock.Play();
        }
        else
        {
           // FindObjectOfType<MainMenuManager>().LoadingScreen.SetActive(true);
            SoundManager.PlaySFX(btnclick, false, 0);
            //scene jump and level no and world no parsing
            PlayerPrefs.SetInt("level", levelno);
           // _levelNo.SetValue(levelno);
            SceneManager.LoadScene("game");
            print("levelopen");
        }
       
    }

}