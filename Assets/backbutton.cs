using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backbutton : MonoBehaviour
{
    public Button yourButton;
    public mainmenucontroller mc;

    void Start()
    {
        Button btn =GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        mc = FindObjectOfType<mainmenucontroller>();
    }

    void TaskOnClick()
    {
       if(mc.exitpanel.activeSelf)
        {
            mc.exitpanel.SetActive(false);
            mc.mainscreen.SetActive(true);
        }
        if (mc.optionpanel.activeSelf)
        {
            mc.optionpanel.SetActive(false);
            mc.mainscreen.SetActive(true);
        }
        if (mc.levelpanel.activeSelf)
        {
            mc.levelpanel.SetActive(false);
            mc.mainscreen.SetActive(true);
        }
    }
}
