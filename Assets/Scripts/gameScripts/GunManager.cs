using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<negun> Guns;
    List<negun> myGuns;
    public int guncount;

    public void Start()
    {
        if(!PlayerPrefs.HasKey("GunSelected")){
            PlayerPrefs.SetInt("GunSelected",0);
        }else{
            PlayerPrefs.SetInt("GunSelected",PlayerPrefs.GetInt("GunSelected",0));
        }
        guncount = PlayerPrefs.GetInt("GunSelected",0);
        myGuns = new List<negun>();
        myGuns = Guns;
        myGuns[guncount].gameObject.SetActive(true);
        GameManager.instance.Gunselected = myGuns[guncount];
    }

    public void canshoot(bool t)
    {
        GameManager.instance.Gunselected.canshoot = t;
    }
    void SwitchGun(int i)
    {
        for(int k = 0; k < myGuns.Count; k++)
        {
            if (i == k)
            {
                myGuns[k].gameObject.SetActive(true);
            }
            else
            {
                myGuns[k].gameObject.SetActive(false);
            }
        }
        PlayerPrefs.SetInt("GunSelected",guncount);
        GameManager.instance.Gunselected = myGuns[guncount];
    }

    public void SwitchBetweenGuns()
    {
        if (guncount < myGuns.Count - 1)
        {
            guncount += 1;
        }
        else if (guncount >= myGuns.Count - 1)
        {
            guncount = 0;
        }
        
        SwitchGun(guncount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchBetweenGuns();
        }
    }
}
