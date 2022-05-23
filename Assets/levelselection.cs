using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelselection : MonoBehaviour
{
    public GameObject content, levelbutton; 
    void Start()
    {
        for(int i=0;i<60;i++)
        {
            GameObject lvlbutton=  Instantiate(levelbutton, content.transform);
            lvlbutton.GetComponent<LevelButtonPick>().levelno = i ;
            lvlbutton.GetComponent<LevelButtonPick>().UIUpdate();
            int j = PlayerPrefs.GetInt("levelcompleted");
          
          //  if(i<=j)
            lvlbutton.GetComponent<LevelButtonPick>().Lock.gameObject.SetActive(false);

            if (i == 0)
            {
                lvlbutton.GetComponent<LevelButtonPick>().Lock.gameObject.SetActive(false);
            }

        }


    }

}
