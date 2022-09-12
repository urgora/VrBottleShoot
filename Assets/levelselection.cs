using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelselection : MonoBehaviour
{
    public GameObject content, levelbutton,buybutton; 
    void Start()
    {
        deletechilds();
        levelshow();
    }


    public void deletechilds()
    {
        foreach (Transform child in content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void levelshow()
    {
        if (PlayerPrefs.GetString("demo") == "demo")
        {
            buybutton.SetActive(true);
            //for (int i = 0; i < 5; i++)
            //{
            //    GameObject lvlbutton = Instantiate(levelbutton, content.transform);
            //    lvlbutton.GetComponent<LevelButtonPick>().levelno = i;
            //    lvlbutton.GetComponent<LevelButtonPick>().UIUpdate();
            //    int j = PlayerPrefs.GetInt("levelcompleted");


            //    if (i <= j && i < 5)
            //    {
            //        lvlbutton.GetComponent<LevelButtonPick>().Lock.gameObject.SetActive(false);
            //    }

            //    if (i == 0)
            //    {
            //        lvlbutton.GetComponent<LevelButtonPick>().Lock.gameObject.SetActive(false);
            //    }

            //}
            for (int i = 0; i < 60; i++)
            {
                GameObject lvlbutton = Instantiate(levelbutton, content.transform);
                lvlbutton.GetComponent<LevelButtonPick>().levelno = i;
                lvlbutton.GetComponent<LevelButtonPick>().UIUpdate();
                int j = PlayerPrefs.GetInt("levelcompleted");

                if (i <= j)
                    lvlbutton.GetComponent<LevelButtonPick>().Lock.gameObject.SetActive(false);

                if (i == 0)
                {
                    lvlbutton.GetComponent<LevelButtonPick>().Lock.gameObject.SetActive(false);

                }

            }
        }
        else if (PlayerPrefs.GetString("demo") == "lvlunlocked")
        {
            buybutton.SetActive(false);
            for (int i = 0; i < 60; i++)
            {
                GameObject lvlbutton = Instantiate(levelbutton, content.transform);
                lvlbutton.GetComponent<LevelButtonPick>().levelno = i;
                lvlbutton.GetComponent<LevelButtonPick>().UIUpdate();
                int j = PlayerPrefs.GetInt("levelcompleted");

                if (i <= j)
                    lvlbutton.GetComponent<LevelButtonPick>().Lock.gameObject.SetActive(false);

                if (i == 0)
                {
                    lvlbutton.GetComponent<LevelButtonPick>().Lock.gameObject.SetActive(false);

                }
               

            }
        }
    }

}
