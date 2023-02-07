using System.Collections;
using System.Collections.Generic;
using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;
public class Gamemanager : MonoBehaviour
{
    [Header("App price")]
    public string fullvertionprice;
    [Header("Unlocked level")]
    public int unlockedlevels;

    public GameObject levelfinished,levelfaled,star,gameoverpanel,objectivepanel,pausepanel,bottleandtimerindicator,nextbutton,shopbutton,shoppanel;
    public GameObject  normal, xrrig,player;
    public GameObject[] levels,playerposition;
    public static float timer, bulletcount,currentbottlecount;
    public TextMeshProUGUI timertext, bulletcountertext,bullettextobjectivepanel,bottletextobjectivepanel,timetextobjectivepanel,gunbulletcounter,levelwinorloss,levelnumber,timerdisplay,bottlecount,delayedtimer,apppriceindicator;
    public bool testing,gamestarted,canshoot;
    public int currentlevel;
    public float percentage,totalbt,totalbottle;

    public Text test;
    public static bool firstopen = false;
    public GameObject checkentitlement;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("demo"))
        {
            PlayerPrefs.SetString("demo", "demo");
        }
        fullvertionprice = "4.99";
        unlockedlevels = 15;
        //  PlayerPrefs.SetInt("level", 16);
        StartCoroutine(gettournament());
        normal.SetActive(false);
        xrrig.SetActive(true);
        objectivepanel.SetActive(true);
        bottleandtimerindicator.SetActive(false);
        canshoot = true;
        //PlayerPrefs.DeleteAll();
        if (!testing)
        {
            currentlevel = PlayerPrefs.GetInt("level",0);
        }

        for (int i = 0; i < levels.Length; i++)
        {
            if (i == currentlevel)
            {
                levels[i].SetActive(true);
                player.transform.position = playerposition[i].transform.position;
                player.transform.rotation = playerposition[i].transform.rotation;
            }
            else
            {
                levels[i].SetActive(false);
            }
        }
        levelnumber.text = " LEVEL " + (currentlevel + 1) ;

        StartCoroutine(delayedplaygame());

        string eventname = "level_" + (currentlevel + 1) + "_Open";
        flurryinstance.instance.levelstatus(eventname);
        if (!firstopen)
            firstopenfunction();

        apppriceindicator.text = "Buy Full Version              ($ " + fullvertionprice + " )";
        //Instantiate(levels[currentlevel], transform.position, transform.rotation);
    }
    public void firstopenfunction()
    {
        checkentitlement.GetComponent<appentilementcheck>().cancheck();
       flurryinstance.instance.levelstatus("App_open");
      //  GetPurchase();
        firstopen = true;
    }
    


    IEnumerator delayedplaygame()
    {
        int timer = 5;
        while(timer>0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            delayedtimer.text = "Game Starts in " + timer + " sec";
        }
        startgame();
       
    }
    public void startobjectivepanelupdate(string bullet,string bottle, string time)
    {
        bullettextobjectivepanel.text = bullet;
        bottletextobjectivepanel.text = bottle;
        timetextobjectivepanel.text = time;

    }
    public void pause()
    {
        Invoke("pausegame", .2f);

    }
    public void pausegame()
    {
        normal.SetActive(false);
        xrrig.SetActive(true);
        gamestarted = false;
        pausepanel.SetActive(true);
        bottleandtimerindicator.SetActive(false);
    }

    private void Update()
    {
        // timertext.text =""+(int)timer;
        //  bulletcountertext.text=""+(int)bulletcount;
        gunbulletcounter.text = "" + bulletcount + "/" + totalbt;
        int minutes=(int)(timer/60);
        int seconds =(int) timer % 60;
        timerdisplay.text = "" + minutes + ":" + seconds;
        bottlecount.text = "" + currentbottlecount + "/" + totalbottle;

        test.text =" " +PlayerPrefs.GetInt("level")+"    " + PlayerPrefs.GetString("demo");
    }
    public void startgame()
    {
        normal.SetActive(true);
        xrrig.SetActive(false);
        objectivepanel.SetActive(false);
        gamestarted = true;
        pausepanel.SetActive(false);
        bottleandtimerindicator.SetActive(true);

    }

    public void Gamedone()
    {
            normal.SetActive(false);
            xrrig.SetActive(true);
            gameoverpanel.SetActive(true);
            levelfinished.SetActive(true);
            gamestarted = false;
            bottleandtimerindicator.SetActive(false);
            levelwinorloss.text = "LEVEL " + (currentlevel + 1) + " COMPLETED";

        if(bulletcount>=((totalbt-totalbottle)))
        {
            if (PlayerPrefs.GetInt("levelstar" + (currentlevel + 1)) < 3)
            {
                PlayerPrefs.SetInt("levelstar" + (currentlevel + 1), 3);
            }
            star.GetComponent<starFxController>().ea = 3;
            star.SetActive(true);
        }
        else if(bulletcount>=((totalbt-totalbottle)-2))
        {
            if (PlayerPrefs.GetInt("levelstar" + (currentlevel + 1)) < 2)
            {
                PlayerPrefs.SetInt("levelstar" + (currentlevel + 1), 2);
            }
            star.GetComponent<starFxController>().ea = 2;
            star.SetActive(true);
        }
        else
        {
            if (PlayerPrefs.GetInt("levelstar" + (currentlevel + 1)) < 1)
            {
                PlayerPrefs.SetInt("levelstar" + (currentlevel + 1), 1);
            }
            star.GetComponent<starFxController>().ea = 1;
            star.SetActive(true);
        }

        //percentage = ((bulletcount /totalbt) * 100);
        //if (percentage >= 50f)
        //{
        //    if (PlayerPrefs.GetInt("levelstar" + (currentlevel + 1)) < 3)
        //    {
        //        PlayerPrefs.SetInt("levelstar" + (currentlevel + 1), 3);
        //    }
        //   star.GetComponent<starFxController>().ea = 3;
        //   star.SetActive(true);
        //}
        //else if (percentage > 25f)
        //{
        //    if (PlayerPrefs.GetInt("levelstar" + (currentlevel + 1)) < 2)
        //    {
        //        PlayerPrefs.SetInt("levelstar" + (currentlevel + 1), 2);
        //    }
        //    star.GetComponent<starFxController>().ea = 2;
        //    star.SetActive(true);
        //}
        //else if (percentage <= 25f)
        //{
        //    if (PlayerPrefs.GetInt("levelstar" + (currentlevel + 1)) < 1)
        //    {
        //        PlayerPrefs.SetInt("levelstar" + (currentlevel + 1), 1);
        //    }
        //    star.GetComponent<starFxController>().ea = 1;
        //    star.SetActive(true);
        //}


        Debug.Log("executed");
        if (PlayerPrefs.GetString("demo") == "lvlunlocked")
        {
            print("full");
            increaselevel();
            shopbutton.SetActive(false);
        }
     //   if(PlayerPrefs.GetString("demo")== "lvlunlocked")
        else
        {
            print("demo");
            if ((currentlevel + 1) < unlockedlevels)
            {
                increaselevel();
                print("demo");
            }
            else
            {
                print("demo");
                shopbutton.SetActive(true);
                nextbutton.SetActive(false);
            }
      
        }
        //string eventname = "level" + (currentlevel + 1);
        //Firebase.Analytics.FirebaseAnalytics.LogEvent(eventname, "Success", PlayerPrefs.GetInt("levelstar" + (currentlevel + 1)));
        //Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelEnd);
        //Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelUp);


      //  flurryinstance.instance.Levelsucess((currentlevel + 1), star.GetComponent<starFxController>().ea);
        string x = "Level_" + (currentlevel + 1) + "_Sucess";
        flurryinstance.instance.levelstatus(x);
    }
    public void gamefalied()
    {
        normal.SetActive(false);
        xrrig.SetActive(true);
        gameoverpanel.SetActive(true);
        levelfaled.SetActive(true);
        gamestarted = false;
        levelwinorloss.text = "LEVEL " + (currentlevel + 1) + " FAILED";

        //string eventname = "level" + (currentlevel + 1);
        //Firebase.Analytics.FirebaseAnalytics.LogEvent(eventname, "Failed",0);
        //Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelEnd);

        string x = "Level_" + (currentlevel + 1) + "_Failed";
        flurryinstance.instance.levelstatus(x);
        bottleandtimerindicator.SetActive(false);
    }
    //public void reload()
    //{
    //    int i= PlayerPrefs.GetInt("level");
    //    if(i<levels.Length-1)
    //    {
    //        PlayerPrefs.SetInt("level", i + 1);
    //    }

    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}
    void GetPurchase()
    {
        IAP.GetViewerPurchases().OnComplete(GetPurchasesCallback);
    }

    private void GetPurchasesCallback(Message<PurchaseList> msg)
    {
        if (msg.IsError) return;
        foreach (var purch in msg.GetPurchaseList())
        {
            //purchaseditem.text += $"{ purch.Sku}-{purch.GrantTime}\n";
            if (purch.Sku == "Fullvertion")
            {
                string purchasedetail = "lvlunlocked";
                PlayerPrefs.SetString("demo", purchasedetail);


            }

            //string purchasedetail = purch.Sku;
            //PlayerPrefs.SetString("demo", purchasedetail);
            //buypanel.SetActive(false);
        }
    }
    public void iapopen(bool opened)
    {
        if(opened)
        {
            flurryinstance.instance.levelstatus("IAP__Open");
        }
        else
        {
            flurryinstance.instance.levelstatus("IAP__Close");
        }
    }
    public void Buycube()
    {
        flurryinstance.instance.levelstatus("IAP__Attempt");
        //#if UNITY_EDITOR
        //        PlayerPrefs.SetString("demo", "lvlunlocked");
        //        buypanel.SetActive(false);
        //        FindObjectOfType<levelselection>().deletechilds();
        //        FindObjectOfType<levelselection>().levelshow();
        //#endif
        IAP.LaunchCheckoutFlow(sku: "Fullvertion").OnComplete(BuyCubeCallback);
      //  Invoke("buysucess", 4);
    }
    void BuyCubeCallback(Message<Purchase> msg)
    {
        if (msg.IsError) return;
        foreach (var purch in msg.GetPurchaseList())
        {
            // purchaseditem.text += $"{ purch.Sku}-{purch.GrantTime}\n";
            if (purch.Sku == "Fullvertion")
            {
                string purchasedetail = "lvlunlocked";
                PlayerPrefs.SetString("demo", purchasedetail);
              //  nextbutton.SetActive(true);
               // shopbutton.SetActive(false);
              //  shoppanel.SetActive(false);
                //if (PlayerPrefs.GetInt("levelcompleted") == 4)
                //{
                //    PlayerPrefs.SetInt("levelcompleted", 5);
                //}
                increaselevel();

                flurryinstance.instance.levelstatus("IAP__sucess");
                reload();
                //if (PlayerPrefs.GetInt("levelcompleted") == 4)
                //{
                //    PlayerPrefs.SetInt("levelcompleted", 5);
                //}
                //  sucess.SetActive(true);
            }
         
        }


    }
    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void replay()
    {
        if (PlayerPrefs.GetString("demo") == "demo")
        {
            if((currentlevel+1)<5)
            {
                int i = PlayerPrefs.GetInt("level") - 1;
                PlayerPrefs.SetInt("level", i);
                reload();
            }
            else if( (currentlevel + 1) == 5)
            reload();
        }
        else if (PlayerPrefs.GetString("demo") == "lvlunlocked")
        {
            int i = PlayerPrefs.GetInt("level") - 1;
            PlayerPrefs.SetInt("level", i);
            reload();
        }
     
    }
    public void home()
    {
        SceneManager.LoadScene("main");
    }
    public void increaselevel()
    {
        int i = PlayerPrefs.GetInt("level") + 1;
      //  print(i);
      //  PlayerPrefs.SetString("demo", "lvlunlocked");
        PlayerPrefs.SetInt("level", i);
        if (PlayerPrefs.GetInt("level") < levels.Length - 1)
        {
           
            if (PlayerPrefs.GetInt("level") >= PlayerPrefs.GetInt("levelcompleted"))
            {
                PlayerPrefs.SetInt("levelcompleted", i);
            }
        }
    }
    public void quit()
    {
        flurryinstance.instance.levelstatus("App_Close");
        UnityEngine.Application.Quit();
    }
    IEnumerator gettournament()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://puzzle-games-d9a78.firebaseapp.com/FootballConfig.json"))
        {

            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
              //  loader.SetActive(false);
                Debug.Log(": Error: " + webRequest.error);
              //  tournament_get();
            }
            else
            {


                JSONNode jsonNode = SimpleJSON.JSON.Parse(webRequest.downloadHandler.text);
                fullvertionprice = jsonNode["IAP_Price"].Value;
                string lvl = jsonNode["IAP_Level"].Value;
                unlockedlevels = int.Parse( lvl);

                print(jsonNode["IAP_Level"].Value);
                print(jsonNode["IAP_Price"].Value);

                apppriceindicator.text = "Buy Full Version              ($ " + fullvertionprice + " )";
            }
        }
    }

}
