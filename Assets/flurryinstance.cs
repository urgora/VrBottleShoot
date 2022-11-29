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
  
    public void levelstatus(string levelstatustext)
    {
        Flurry.EventRecordStatus status;
        status = Flurry.LogEvent(levelstatustext);
    }

    public void openlevelapppurchase()
    {
        levelstatus("IAP_open");
    }
    public void IAPclose()
    {
        levelstatus("IAP_Close");
    }




}
