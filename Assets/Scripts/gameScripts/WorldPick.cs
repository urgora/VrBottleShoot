using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldPick : MonoBehaviour
{
    public TextMeshProUGUI WorldTitle;
    public Image _wrldImg;
    public Image Lock;
    public Animation _lock;
    public int worldno;
    public Floatvariable WorldSelected;

    [HideInInspector]
    public List<ScrollRect> thisworldLevels;
    [HideInInspector]
    public GameObject worldselection;
    public AudioClip btnclick, lockaudio;

    public void Start()
    {
        thisworldLevels = FindObjectOfType<LevelManager>().levelspanel;
    }
    public void OnBtnClick()
    {
        if (Lock.gameObject.activeInHierarchy)
        {
            SoundManager.PlaySFX(lockaudio, false, 0);
            _lock.Play();
        }
        else
        {
            SoundManager.PlaySFX(btnclick, false, 0);
            WorldSelected.SetValue(worldno);
            //close world selection and enable specific world levels panel
            worldselection.gameObject.SetActive(false);
            for (int i = 0; i < thisworldLevels.Count; i++)
            {
                if (i == worldno) {
                     thisworldLevels[i].gameObject.SetActive(true);
                }
                else
                {
                    thisworldLevels[i].gameObject.SetActive(false);
                }
            }
            FindObjectOfType<MainMenuManager>().ActivatePanel(2);
        }

    }
}
