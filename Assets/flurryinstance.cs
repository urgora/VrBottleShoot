using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlurrySDK;
using FlurrySDKInternal;
public class flurryinstance : MonoBehaviour
{
    private string FLURRY_API_KEY = "2BH6MT4HZ5YTPRNMBPTG";
    public static flurryinstance instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }




    void Start()
    {
        // Initialize Flurry.
        new Flurry.Builder()
                  .WithCrashReporting(true)
                  .WithLogEnabled(true)
                  .WithLogLevel(Flurry.LogLevel.VERBOSE)
                  .WithMessaging(true)
                  .Build(FLURRY_API_KEY);


        // Flurry.EventRecordStatus LogEvent("open",paramsDict.ToString()); 

        // Flurry.EventRecordStatus LogEvent(string eventId);
        levelstatus("App_open");
    }
    public void onclick()
    {
        Flurry.EventRecordStatus status;
        Flurry.LogEvent("working");
        status = Flurry.LogEvent(Flurry.Event.LEVEL_STARTED);

        PlayerPrefs.SetInt("x", (PlayerPrefs.GetInt("x") + 1));
        string x = "" + PlayerPrefs.GetInt("x");
        Dictionary<string, string> paramsDict = new Dictionary<string, string>() { { "preessed", "x" } };
        status = Flurry.LogEvent("Unity Event", paramsDict);
    }
    public void levelstatus(string levelstatustext)
    {
        Flurry.EventRecordStatus status;
        status = Flurry.LogEvent(levelstatustext);
    }

    public void openlevelapppurchase()
    {
        Flurry.EventRecordStatus status;
        status = Flurry.LogEvent("In_App_purchase__opened");
    }
  
    public void Levelsucess(int levelnumber,int star)
    {
        Flurry.EventRecordStatus status;
        IDictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("Level number", levelnumber.ToString());
        parameters.Add("Star Count", star.ToString());
        status = Flurry.LogEvent("Level Sucess", parameters);
    }
    public void Levelfail(int levelnumber)
    {
        Flurry.EventRecordStatus status;
        IDictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("Level number", levelnumber.ToString());
        status = Flurry.LogEvent("Level Failed", parameters);
    }



}
